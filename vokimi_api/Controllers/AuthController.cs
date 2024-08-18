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

namespace vokimi_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IDbContextFactory<AppDbContext> _dbFactory;
        public AuthController(IDbContextFactory<AppDbContext> dbFactory) {
            _dbFactory = dbFactory;
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
        public IResult GetUsernameWithProfilePicture() {
            var currentUser = this.User;

            if (currentUser.Identity?.IsAuthenticated ?? false) {
                string? userName = currentUser.FindFirstValue(PingAuthObj.ClaimKeyUsername);
                string? userId = currentUser.FindFirstValue(PingAuthObj.ClaimKeyUserId);

                //get from db profile picture

                return Results.Json(new {
                    UserName = userName,
                    ProfilePictureUrl = ""
                });
            } else {
                return Results.Unauthorized();
            }
        }

        public record SignupRequest(string Email, string Password, string Username);
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

        public record LoginRequest(string Email, string Password);
        [HttpPost]
        [Route("/login")]
        public async Task<IResult> LoginAsync([FromBody] LoginRequest loginRequest) {

            //check if user exists
            //check password

            string username = "basicUsername";
            AppUserId userId = new(Guid.NewGuid());

            List<Claim> claims = [
                new(PingAuthObj.ClaimKeyEmail, loginRequest.Email),
                new(PingAuthObj.ClaimKeyUsername, username),
                new(PingAuthObj.ClaimKeyUserId, userId.ToString())
            ];

            ClaimsPrincipal claimsPrincipal = new(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
            AuthenticationProperties authProperties = new();

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authProperties);

            return Results.Ok("User logged in");
        }
        [HttpPost]
        [Route("/logout")]
        public async Task<IResult> LogoutAsync() {
            await HttpContext.SignOutAsync();
            return Results.Ok();
        }
    }

}
