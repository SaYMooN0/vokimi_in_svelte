using Microsoft.EntityFrameworkCore;
using vokimi_api.Helpers;
using vokimi_api.Src.db_related;
using vokimi_api.Src.db_related.db_entities.published_tests.published_tests_shared;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.extension_classes;

namespace vokimi_api.Endpoints.pages.manage_test
{
    internal static class ManageTestOverallEndpoints
    {
        internal static async Task<IResult> CheckUserAccessToPage(
            string testIdString,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {
            if (!Guid.TryParse(testIdString, out var testGuid)) {
                return ResultsHelper.BadRequest.UnknownTest();
            }
            if (!httpContext.TryGetUserId(out var userId)) {
                return ResultsHelper.BadRequest.LogOutLogIn();
            }
            TestId testId = new(testGuid);
            using (var db = await dbFactory.CreateDbContextAsync()) {
                BaseTest? t = await db.TestsSharedInfo.FindAsync(testId);
                if (t is null) {
                    return ResultsHelper.BadRequest.UnknownTest();
                }

                if (t.CreatorId == userId) {
                    return Results.Ok();
                }
                return ResultsHelper.BadRequest.WithErr("You are not creator of this test");

            }
        }
        internal static async Task<IResult> GetBasePageInfo(
            string testIdString,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {
            throw new();
        }

    }
}
