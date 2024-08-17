using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using vokimi_api.Src.db_related;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src;
using System.Text.Json;

namespace vokimi_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext db;
        public AuthController(AppDbContext appDbContext) {
            db = appDbContext;
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

        [HttpPost]
        [Route("/signup")]
        public IResult Signup() {
            return Results.Ok();
        }

        
        [HttpPost]
        [Route("/login")]
        public async Task<IResult> LoginAsync([FromBody] LoginRequest loginRequest) {

            //check if user exists
            //check password

            string username = "";
            AppUserId userId = new(new Guid());

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
        public record LoginRequest(string Email, string Password);
        [HttpPost]
        [Route("/logout")]
        public async Task<IResult> LogoutAsync() {
            await HttpContext.SignOutAsync();
            return Results.Ok();
        }
    }

}
