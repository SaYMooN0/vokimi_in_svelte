using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using vokimi_api.Src.db_related;

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
                string? email = user.FindFirstValue(ClaimTypes.Email);
                string? username = user.FindFirstValue(ClaimTypes.Name);
                string? userId = user.FindFirstValue("UserId");

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
                string? userName = currentUser.Identity.Name;
                string? userId = currentUser.FindFirstValue("UserId");

                return Results.Json(new {
                    UserName = userName,
                    ProfilePictureUrl = ""
                });
            } else {
                return Results.Unauthorized();
            }
        }
    }
}
