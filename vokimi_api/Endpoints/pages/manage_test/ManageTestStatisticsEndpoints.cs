using Microsoft.EntityFrameworkCore;
using vokimi_api.Helpers;
using vokimi_api.Src.db_related.db_entities.published_tests.published_tests_shared;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.db_related;
using vokimi_api.Src.dtos.responses.manage_test_page.tags;
using vokimi_api.Src.extension_classes;

namespace vokimi_api.Endpoints.pages.manage_test
{
    internal static class ManageTestStatisticsEndpoints
    {
        internal static async Task<IResult> GetTabData(
            string testIdString,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {
            if (!Guid.TryParse(testIdString, out var testGuid)) {
                return ResultsHelper.BadRequest.UnknownTest();
            }
            TestId testId = new(testGuid);
            using (var db = await dbFactory.CreateDbContextAsync()) {
                BaseTest? t = await db.TestsSharedInfo
                    .Include(t => t.Tags)
                    .Include(t => t.SuggestedTags)
                    .FirstOrDefaultAsync(t => t.Id == testId);
                if (t is null) {
                    return ResultsHelper.BadRequest.UnknownTest();
                }
                if (!httpContext.IsAuthenticatedUserIsTestCreator(t)) {
                    return ResultsHelper.BadRequest.WithErr("You don't have access to this page");
                }
                return ResultsHelper.BadRequest.WithErr("Statistics tab not implemented");
            }
        }
    }
}
