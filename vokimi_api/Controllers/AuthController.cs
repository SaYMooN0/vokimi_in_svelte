using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using vokimi_api.Src.db_related;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src;
using System.Text.Json;
using Org.BouncyCastle.Crypto.Agreement;
using vokimi_api.Src.constants_store_classes;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Text.RegularExpressions;
using vokimi_api.Src.db_related.db_entities.users;
using Org.BouncyCastle.Crypto.Generators;
using static System.Runtime.InteropServices.JavaScript.JSType;
using vokimi_api.Services;

namespace vokimi_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IDbContextFactory<AppDbContext> _dbFactory;
        private readonly EmailService _emailService;
        public AuthController(IDbContextFactory<AppDbContext> dbFactory, EmailService emailService) {
            _dbFactory = dbFactory;
            _emailService = emailService;
        }

        [HttpGet]
        [Route("/pingauth")]
        public IResult PingAuth() {
            var user = this.User;

            if (user.Identity?.IsAuthenticated ?? false) {
                string? email = user.FindFirstValue(PingAuthObj.ClaimKeyEmail);
                string? username = user.FindFirstValue(PingAuthObj.ClaimKeyUsername);
                string? userIdStr = user.FindFirstValue(PingAuthObj.ClaimKeyUserId);

                AppUserId? userId = null;
                if (!string.IsNullOrEmpty(userIdStr)) {
                    userId = new(new Guid(userIdStr));
                }

                PingAuthObj objToReturn = new PingAuthObj(email, username, userId);

                return Results.Json(new { Email = email, Username = username, UserId = userId });
            } else {
                return Results.Unauthorized();
            }
        }


        [HttpGet]
        [Route("/getUsernameWithProfilePicture")]
        public async Task<IResult> GetUsernameWithProfilePicture() {
            var currentUser = this.User;

            if (currentUser.Identity?.IsAuthenticated ?? false) {
                string? userId = currentUser.FindFirstValue(PingAuthObj.ClaimKeyUserId);

                if (Guid.TryParse(userId, out Guid userGuid)) {

                    using (var db = await _dbFactory.CreateDbContextAsync()) {
                        AppUserId appUserId = new(userGuid);

                        AppUser? user = await db.AppUsers.FirstOrDefaultAsync(u => u.Id == appUserId);
                        if (user is null) {
                            return Results.Unauthorized();
                        }

                        return Results.Json(new {
                            Username = user.Username,
                            ProfilePicture = ImgOperationsConsts.ImgUrl(user.ProfilePicturePath)
                        });
                    }
                }


            }
            return Results.Unauthorized();
        }

        public record SignupRequest(string Email, string Password, string Username, string FrontendBaseUrl);
        [HttpPost]
        [Route("/signup")]
        public async Task<IResult> Signup([FromBody] SignupRequest signupRequest) {
            using (var db = _dbFactory.CreateDbContext()) {
                Err formValidatingErr = ValidateSignupRequest(signupRequest, db);
                if (formValidatingErr.NotNone()) {
                    return Results.BadRequest(new { Error = formValidatingErr.Message });
                }
                string confirmationCode = $"{DateTime.Now.GetHashCode()}-{Guid.NewGuid()}";
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(signupRequest.Password);


                UnconfirmedAppUser unconfirmedUser = UnconfirmedAppUser.CreateNew(
                    signupRequest.Username,
                    signupRequest.Email,
                    passwordHash,
                    confirmationCode
                    );
                using (var transaction = await db.Database.BeginTransactionAsync()) {
                    try {
                        var existingUser = await db.UnconfirmedAppUsers.FirstOrDefaultAsync(u => u.Email == signupRequest.Email);
                        if (existingUser != null) {
                            db.UnconfirmedAppUsers.Remove(existingUser);
                        }

                        await db.UnconfirmedAppUsers.AddAsync(unconfirmedUser);
                        await db.SaveChangesAsync();

                        string confirmationLink =
                            $"{signupRequest.FrontendBaseUrl}/confirm-registration/{unconfirmedUser.Id}/{confirmationCode}";

                        Err emailErr = _emailService.SendConfirmationLink(signupRequest.Email, confirmationLink);
                        if (emailErr.NotNone()) {
                            throw new Exception();
                        }

                        await transaction.CommitAsync();
                    } catch {
                        await transaction.RollbackAsync();
                        return Results.BadRequest(new { Error = "Something went wrong. Please try again later" });
                    }
                    return Results.Ok();
                }

            }

        }
        private Err ValidateSignupRequest(SignupRequest signupRequest, AppDbContext db) {
            if (string.IsNullOrEmpty(signupRequest.FrontendBaseUrl)) {
                return new Err("Incorrect data");
            }
            if (db.AppUsers.Any(x => x.LoginInfo.Email == signupRequest.Email)) {
                return new Err("Email already in use");
            }
            int passwordLength = string.IsNullOrEmpty(signupRequest.Password) ? 0 : signupRequest.Password.Length;
            if (passwordLength < AppUsersConsts.MinPasswordLength || passwordLength > AppUsersConsts.MaxPasswordLength) {
                return new Err(
                    $"Password must be between {AppUsersConsts.MinPasswordLength} and {AppUsersConsts.MaxPasswordLength} characters");
            }
            if (!MailAddress.TryCreate(signupRequest.Email, out var _)) { return new Err("Invalid email"); }
            int usernameLength = string.IsNullOrEmpty(signupRequest.Username) ? 0 : signupRequest.Username.Length;
            if (usernameLength < AppUsersConsts.MinUsernameLength ||
                usernameLength > AppUsersConsts.MaxUsernameLength) {
                return new Err(
                    $"Username must be between {AppUsersConsts.MinUsernameLength} and {AppUsersConsts.MaxUsernameLength} characters");
            }
            if (!AppUsersConsts.UsernameRegex.IsMatch(signupRequest.Username)) {
                return new("Invalid username");
            }
            return Err.None;
        }

        public record ConfirmRegistrationRequest(string ConfirmationString, string UserId);
        [HttpPost]
        [Route("/confirmRegistration")]
        public async Task<IResult> ConfirmRegistration([FromBody] ConfirmRegistrationRequest requestData) {

            UnconfirmedAppUserId unconfirmedAppUserId;

            if (Guid.TryParse(requestData.UserId, out Guid userGuid)) {
                unconfirmedAppUserId = new UnconfirmedAppUserId(userGuid);
            } else {
                return Results.BadRequest(new { Error = "Something went wrong. Please click on confirmation link in email again" });
            }

            using (var db = _dbFactory.CreateDbContext()) {
                UnconfirmedAppUser? unconfirmed = await db.UnconfirmedAppUsers
                    .FirstOrDefaultAsync(x => x.Id == unconfirmedAppUserId && x.ConfirmationCode == requestData.ConfirmationString);
                if (unconfirmed is null) {
                    return Results.BadRequest(new { Error = "Either this user has already been confirmed or the link has expired" });
                }
                var loginInfo = LoginInfo.CreateNew(unconfirmed.Email, unconfirmed.PasswordHash);
                var additionalInfo = UserAdditionalInfo.CreateNew(unconfirmed.RegistrationDate);
                var newUser = AppUser.CreateNew(unconfirmed.Username, loginInfo.Id, additionalInfo.Id);

                using (var transaction = await db.Database.BeginTransactionAsync()) {
                    try {

                        db.UserAdditionalInfo.Add(additionalInfo);
                        db.LoginInfo.Add(loginInfo);
                        db.AppUsers.Add(newUser);
                        db.UnconfirmedAppUsers.Remove(unconfirmed);
                        await db.SaveChangesAsync();
                        await transaction.CommitAsync();
                    } catch {
                        await transaction.RollbackAsync();
                        return Results.BadRequest(new { Error = "Something went wrong. Please try again later" });
                    }

                }

                Err signInErr = await SignInUser(new PingAuthObj(loginInfo.Email, newUser.Username, newUser.Id));
                if (signInErr.NotNone()) {
                    return Results.BadRequest(new { Error = "Email has been confirmed but could not be signed in. Please log in by yourself" });
                }
                return Results.Ok();
            }
        }





        public record LoginRequest(string Email, string Password);
        [HttpPost]
        [Route("/login")]
        public async Task<IResult> LoginAsync([FromBody] LoginRequest loginRequest) {

            //check if user exists
            //check password

            string username = "basicUsername";
            AppUserId userId = new(Guid.NewGuid());



            return Results.Ok("User logged in");
        }
        [HttpPost]
        [Route("/logout")]
        public async Task<IResult> LogoutAsync() {
            await HttpContext.SignOutAsync();
            return Results.Ok();
        }

        private async Task<Err> SignInUser(PingAuthObj data) {
            try {
                if (data.AnyFieldEmpty) {
                    return new Err("Incorrect data");
                }
                List<Claim> claims = [
                    new(PingAuthObj.ClaimKeyEmail, data.Email),
                    new(PingAuthObj.ClaimKeyUsername, data.Username),
                    new(PingAuthObj.ClaimKeyUserId, data.UserId.ToString())
                ];
                ClaimsPrincipal claimsPrincipal = new(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
                AuthenticationProperties authProperties = new();

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authProperties);

                return Err.None;
            } catch {
                return new Err("Something went wrong.");
            }
        }
    }

}
