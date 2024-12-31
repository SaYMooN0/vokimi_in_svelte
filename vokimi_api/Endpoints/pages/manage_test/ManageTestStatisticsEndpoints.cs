using Microsoft.EntityFrameworkCore;
using vokimi_api.Helpers;
using vokimi_api.Src.db_related.db_entities.published_tests.published_tests_shared;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.db_related;
using vokimi_api.Src.dtos.responses.manage_test_page.tags;
using vokimi_api.Src.extension_classes;
using vokimi_api.Src.enums;
using vokimi_api.Src.db_related.db_entities.published_tests.general_test_related;
using vokimi_api.Src.dtos.responses.manage_test_page.statistics;

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
                BaseTest? t = await db.TestsSharedInfo.FirstAsync(t => t.Id == testId);
                if (t is null) {
                    return ResultsHelper.BadRequest.UnknownTest();
                }
                if (!httpContext.IsAuthenticatedUserIsTestCreator(t)) {
                    return ResultsHelper.BadRequest.WithErr("You don't have access to this page");
                }
                return t.Template switch {
                    TestTemplate.General => await GetStatisticsTabDataForGeneralTest(db, testId),
                    _ => throw new Exception("Not implemented")
                };
            }
        }
        private static async Task<IResult> GetStatisticsTabDataForGeneralTest(AppDbContext db, TestId testId) {
            TestGeneralTemplate? test = await db.TestsGeneralTemplate
                .Include(t => t.Creator)
                    .ThenInclude(c => c.Followers)
                    .ThenInclude(c => c.Friends)
                .Include(t => t.Ratings)
                .Include(t => t.DiscussionsComments)
                .Include(t => t.TestTakings)
                .FirstOrDefaultAsync(t => t.Id == testId);
            if (test is null) {
                return ResultsHelper.BadRequest.UnknownTest();
            }
            return Results.Ok(TestStatisticsData.ForGeneralTest(test));
        }
    }
}
