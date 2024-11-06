using System.Security.Claims;
using vokimi_api.Src.db_related.db_entities.draft_tests.draft_tests_shared;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.dtos.responses;
using VokimiShared.src.models.db_classes.test.test_types;

namespace vokimi_api.Src.extension_classes
{
    public static class HttpContextExtensions
    {
        public static bool TryGetUserId(this HttpContext httpContext, out AppUserId userId) {
            userId = default;

            if (!(httpContext.User.Identity?.IsAuthenticated ?? false)) {
                return false;
            }

            string? userIdStr = httpContext.User.FindFirstValue(PingAuthResponse.ClaimKeyUserId);
            if (string.IsNullOrEmpty(userIdStr) || !Guid.TryParse(userIdStr, out Guid userGuid)) {
                return false;
            }

            userId = new AppUserId(userGuid);
            return true;
        }
        public static bool IfAuthenticatedUserIdEquals(this HttpContext httpContext, AppUserId? userId) {
            if (userId is null) {
                return false;
            }
            return httpContext.TryGetUserId(out AppUserId authenticatedUserId) && authenticatedUserId == userId.Value;
        }
        public static bool IfAuthenticatedUserIdEqualsStr(this HttpContext httpContext, string userIdStr) {
            if (!Guid.TryParse(userIdStr, out Guid userGuid)) {
                return false;
            }

            AppUserId userId = new(userGuid);
            return httpContext.IfAuthenticatedUserIdEquals(userId);
        }
        public static bool IsAuthenticatedUserIsTestCreator(this HttpContext httpContext, BaseDraftTest test) =>
            httpContext.IfAuthenticatedUserIdEquals(test.CreatorId);
        public static bool IsAuthenticatedUserIsTestCreator(this HttpContext httpContext, BaseTest test) =>
           httpContext.IfAuthenticatedUserIdEquals(test.CreatorId);

    }

}
