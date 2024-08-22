using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using vokimi_api.Src;
using vokimi_api.Src.db_related;
using vokimi_api.Src.db_related.db_entities.users;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.dtos.responses;

namespace vokimi_api.Endpoints
{
    public class UserTestsEndpoints
    {
        public static async Task<IResult> GetUsersDraftTestsVms(
            HttpContext httpContext, IDbContextFactory<AppDbContext> dbFactory) {
            var cntxUser = httpContext.User;

            if (cntxUser.Identity?.IsAuthenticated ?? false) {
                string? userIdStr = cntxUser.FindFirstValue(PingAuthResponse.ClaimKeyUserId);

                AppUserId? userId = null;
                if (!string.IsNullOrEmpty(userIdStr)) {
                    userId = new(new Guid(userIdStr));
                } else {
                    return Results.Unauthorized();
                }
                using (var db = dbFactory.CreateDbContext()) {
                    AppUser? user = await db.AppUsers
                        .Include(u => u.DraftTests)
                        .FirstOrDefaultAsync(u => u.Id == userId);
                    if (user is null) {
                        return Results.BadRequest(new { Err = "Unknown user" });
                    }
                    return Results.Ok(user.DraftTests.Select(UsersTestsVm.FromDraftTest));
                }
            } else {
                return Results.BadRequest();
            }
        }
        public static Results<Ok<UsersTestsVm[]>, BadRequest> GetUsersЗPublishedTestsVms(HttpContext httpContext) {
            var user = httpContext.User;

            if (user.Identity?.IsAuthenticated ?? false) {
                string? userIdStr = user.FindFirstValue(PingAuthResponse.ClaimKeyUserId);

                AppUserId? userId = null;
                if (!string.IsNullOrEmpty(userIdStr)) {
                    userId = new(new Guid(userIdStr));
                }


                return TypedResults.Ok(new UsersTestsVm[] { });
            } else {
                return TypedResults.BadRequest();
            }
        }
    }
}
