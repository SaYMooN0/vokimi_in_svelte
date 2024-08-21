using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Claims;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.dtos.responses;

namespace vokimi_api.Endpoints
{
    public class UserTestsEndpoints
    {
        public static Results<Ok<UsersDraftTestsVm[]>, BadRequest> GetUsersDraftTestsVms(HttpContext httpContext) {
            var user = httpContext.User;

            if (user.Identity?.IsAuthenticated ?? false) {
                string? userIdStr = user.FindFirstValue(PingAuthResponse.ClaimKeyUserId);

                AppUserId? userId = null;
                if (!string.IsNullOrEmpty(userIdStr)) {
                    userId = new(new Guid(userIdStr));
                }


                return TypedResults.Ok(new UsersDraftTestsVm[] { });
            } else {
                return TypedResults.BadRequest();
            }
        }

    }
}
