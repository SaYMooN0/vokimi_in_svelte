using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using vokimi_api.Src.db_related;

namespace vokimi_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController
    {
        private readonly AppDbContext db;
        public AuthController(AppDbContext appDbContext) {
            db = appDbContext;
        }

        [HttpGet]
        [Route("/pingauth")]
        [Authorize]
        public IResult PingAuth(ClaimsPrincipal user) {
            string? email = user.FindFirstValue(ClaimTypes.Email);
            string? username = user.FindFirstValue(ClaimTypes.Name);
            string? userId = user.FindFirstValue("UserId");

            return Results.Json(new { Email = email, Username = username, UserId = userId });
        }
        public record PingAuthObj(bool isAuthenticated, string Username, string Email);

        [HttpGet]
        [Route("/getUsernameWithProfilePicture")]
        public IResult GetUsernameWithProfilePicture() {
            return Results.Json(new { Username = "test", ProfileImage = "test" });
        }
    }
}
