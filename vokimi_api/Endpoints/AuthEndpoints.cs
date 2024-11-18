using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Security.Claims;
using vokimi_api.Services;
using vokimi_api.Src.constants_store_classes;
using vokimi_api.Src.db_related.db_entities.users;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.db_related;
using vokimi_api.Src;
using Microsoft.AspNetCore.Http.HttpResults;
using vokimi_api.Src.dtos.responses;
using vokimi_api.Src.dtos.requests.auth;
using vokimi_api.Src.extension_classes;
using vokimi_api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace vokimi_api.Endpoints
{
    public class AuthEndpoints
    {
        public static async Task<Results<Ok<PingAuthResponse>, UnauthorizedHttpResult>> PingAuth(
            HttpContext httpContext,
            IDbContextFactory<AppDbContext> dbFactory
        ) {

            if (!httpContext.TryGetUserId(out var userId)) {
                return TypedResults.Unauthorized();
            }

            using (var db = await dbFactory.CreateDbContextAsync()) {
                AppUser? user = await db.AppUsers
                    .Include(u => u.LoginInfo)
                    .FirstOrDefaultAsync(x => x.Id == userId);
                if (user is null) {
                    await httpContext.SignOutAsync();
                    return TypedResults.Unauthorized();
                }

                PingAuthResponse objToReturn = new(user.LoginInfo.Email, user.Username, user.Id);

                return TypedResults.Ok(objToReturn);
            }
        }


        public static async Task<IResult> GetUsernameWithProfilePicture(
            HttpContext httpContext,
            IDbContextFactory<AppDbContext> dbFactory
        ) {
            if (!httpContext.TryGetUserId(out var appUserId)) {
                return Results.Unauthorized();

            }
            using (var db = await dbFactory.CreateDbContextAsync()) {
                AppUser? user = await db.AppUsers.FirstOrDefaultAsync(u => u.Id == appUserId);
                if (user is null) {
                    await httpContext.SignOutAsync();
                    return Results.Unauthorized();
                }

                return Results.Json(new {
                    Username = user.Username,
                    ProfilePicture = user.ProfilePicturePath
                });
            }

        }

        public static async Task<IResult> Signup(
            [FromBody] SignupRequest signupRequest,
            IDbContextFactory<AppDbContext> dbFactory,
            EmailService emailService
        ) {
            using var db = await dbFactory.CreateDbContextAsync();
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

            using var transaction = await db.Database.BeginTransactionAsync();
            try {
                var existingUser = await
                    db.UnconfirmedAppUsers.FirstOrDefaultAsync(u => u.Email == signupRequest.Email);
                if (existingUser is not null) {
                    db.UnconfirmedAppUsers.Remove(existingUser);
                }

                await db.UnconfirmedAppUsers.AddAsync(unconfirmedUser);
                await db.SaveChangesAsync();

                string confirmationLink =
                    $"{signupRequest.FrontendBaseUrl}/confirm-registration/{unconfirmedUser.Id}/{confirmationCode}";

                Err emailErr = await emailService.SendConfirmationLink(signupRequest.Email, confirmationLink);
                if (emailErr.NotNone()) {
                    throw new Exception();
                }

                await transaction.CommitAsync();
            } catch {
                await transaction.RollbackAsync();
                return ResultsHelper.BadRequest.WithErr("Something went wrong. Please try again later");
            }

            return Results.Ok();
        }

        public static async Task<IResult> ConfirmRegistration(
            [FromBody] ConfirmRegistrationRequest requestData,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {
            if (!Guid.TryParse(requestData.UserId, out Guid userGuid)) {
                return ResultsHelper.BadRequest
                    .WithErr("Something went wrong. Please click on confirmation link in email again");
            }

            UnconfirmedAppUserId unconfirmedAppUserId = new(userGuid);

            using var db = await dbFactory.CreateDbContextAsync();
            UnconfirmedAppUser? unconfirmed = await db.UnconfirmedAppUsers.FirstOrDefaultAsync(
                x => x.Id == unconfirmedAppUserId
                && x.ConfirmationCode == requestData.ConfirmationString
            );

            if (unconfirmed is null) {
                return ResultsHelper.BadRequest
                    .WithErr("Either this user has already been confirmed or the link has expired");
            }

            var loginInfo = LoginInfo.CreateNew(unconfirmed.Email, unconfirmed.PasswordHash);
            var additionalInfo = UserAdditionalInfo.CreateNew(DateOnly.FromDateTime(unconfirmed.RegistrationDate));
            var pageSettings = UserPageSettings.CreateNew();

            var newUser = AppUser.CreateNew(unconfirmed.Username, loginInfo.Id, additionalInfo.Id, pageSettings.Id);

            using var transaction = await db.Database.BeginTransactionAsync();
            try {
                db.UserAdditionalInfo.Add(additionalInfo);
                db.LoginInfo.Add(loginInfo);
                db.AppUsers.Add(newUser);
                db.UnconfirmedAppUsers.Remove(unconfirmed);
                await db.SaveChangesAsync();
                await transaction.CommitAsync();
            } catch {
                await transaction.RollbackAsync();
                return ResultsHelper.BadRequest.WithErr("Something went wrong. Please try again later");
            }

            Err signInErr = await SignInUser(new PingAuthResponse(loginInfo.Email, newUser.Username, newUser.Id), httpContext);
            if (signInErr.NotNone()) {
                return ResultsHelper.BadRequest
                    .WithErr("Email has been confirmed but could not be signed in. Please log in by yourself");
            }

            return Results.Ok();
        }

        public static async Task<IResult> Login(
            [FromBody] LoginRequest loginRequest,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {
            if (string.IsNullOrEmpty(loginRequest.Email) || string.IsNullOrEmpty(loginRequest.Password)) {
                return ResultsHelper.BadRequest.WithErr("Please fill all fields");
            }

            using (var db = await dbFactory.CreateDbContextAsync()) {
                AppUser? user = await db.AppUsers
                    .Include(x => x.LoginInfo)
                    .FirstOrDefaultAsync(u => u.LoginInfo.Email == loginRequest.Email);

                if (user is null) {
                    return ResultsHelper.BadRequest.WithErr("There is no user with this email");
                }

                if (!BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.LoginInfo.PasswordHash)) {
                    return ResultsHelper.BadRequest.WithErr("Incorrect password");
                }

                var signInData = new PingAuthResponse(user.LoginInfo.Email, user.Username, user.Id);
                Err signInErr = await SignInUser(signInData, httpContext);
                if (signInErr.NotNone()) {
                    return ResultsHelper.BadRequest.WithErr("Something went wrong. Please try again later");
                }

                return Results.Ok();
            }
        }

        public static async Task<IResult> Logout(HttpContext httpContext) {
            await httpContext.SignOutAsync();
            return Results.Ok();
        }

        private static Err ValidateSignupRequest(SignupRequest signupRequest, AppDbContext db) {
            if (string.IsNullOrEmpty(signupRequest.FrontendBaseUrl)) {
                return new("Incorrect data");
            }
            if (db.AppUsers.Any(x => x.LoginInfo.Email == signupRequest.Email)) {
                return new("Email already in use");
            }

            int passwordLength = string.IsNullOrEmpty(signupRequest.Password) ? 0 : signupRequest.Password.Length;
            if (passwordLength < AppUsersConsts.MinPasswordLength || passwordLength > AppUsersConsts.MaxPasswordLength) {
                return new(
                    $"Password must be between {AppUsersConsts.MinPasswordLength} and {AppUsersConsts.MaxPasswordLength} characters");
            }

            if (!MailAddress.TryCreate(signupRequest.Email, out var _)) {
                return new("Invalid email");
            }

            int usernameLength = string.IsNullOrEmpty(signupRequest.Username) ? 0 : signupRequest.Username.Length;
            if (usernameLength < AppUsersConsts.MinUsernameLength || usernameLength > AppUsersConsts.MaxUsernameLength) {
                return new(
                    $"Username must be between {AppUsersConsts.MinUsernameLength} and {AppUsersConsts.MaxUsernameLength} characters");
            }

            if (!AppUsersConsts.UsernameRegex.IsMatch(signupRequest.Username)) {
                return new("Invalid username");
            }

            return Err.None;
        }

        private static async Task<Err> SignInUser(PingAuthResponse data, HttpContext httpContext) {
            try {
                if (data.AnyFieldEmpty) {
                    return new Err("Incorrect data");
                }

                List<Claim> claims = new()
                {
                    new(PingAuthResponse.ClaimKeyEmail, data.Email),
                    new(PingAuthResponse.ClaimKeyUsername, data.Username),
                    new(PingAuthResponse.ClaimKeyUserId, data.UserId.ToString())
                };

                ClaimsPrincipal claimsPrincipal = new(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
                AuthenticationProperties authProperties = new();

                await httpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    claimsPrincipal,
                    authProperties
                );

                return Err.None;
            } catch {
                return new Err("Something went wrong.");
            }
        }
    }
}
