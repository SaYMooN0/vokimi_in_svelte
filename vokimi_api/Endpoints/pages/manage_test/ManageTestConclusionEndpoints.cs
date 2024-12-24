using Microsoft.EntityFrameworkCore;
using vokimi_api.Helpers;
using vokimi_api.Src.db_related.db_entities.published_tests.published_tests_shared;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.db_related;
using vokimi_api.Src.extension_classes;
using vokimi_api.Src.dtos.responses.manage_test_page.conclusion;

namespace vokimi_api.Endpoints.pages.manage_test
{
    internal static class ManageTestConclusionEndpoints
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
                    .Include(t => t.GetBaseTestTakings())
                    .Include(t => t.Conclusion)
                    .FirstOrDefaultAsync(t => t.Id == testId);
                if (t is null) {
                    return ResultsHelper.BadRequest.UnknownTest();
                }
                if (!httpContext.IsAuthenticatedUserIsTestCreator(t)) {
                    return ResultsHelper.BadRequest.WithErr("You don't have access to this page");
                }
                return Results.Ok(ManageTestConclusionTabDataResponse.FromTest(t));
            }
        }
        internal static async Task<IResult> EnableTestFeedback(
            string testIdString,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {
            if (!Guid.TryParse(testIdString, out var testGuid)) {
                return ResultsHelper.BadRequest.UnknownTest();
            }
            TestId testId = new(testGuid);
            using (var db = await dbFactory.CreateDbContextAsync()) {
                BaseTest? test = await db.TestsSharedInfo
                    .Include(t => t.Conclusion)
                    .FirstOrDefaultAsync(t => t.Id == testId);
                if (test is null) {
                    return ResultsHelper.BadRequest.UnknownTest();
                }
                if (!httpContext.IsAuthenticatedUserIsTestCreator(test)) {
                    return ResultsHelper.BadRequest.WithErr("You don't have access to this page");
                }
                if (test.Conclusion is null) {
                    return ResultsHelper.BadRequest.WithErr(
                        "You cannot enable test feedback because test does not have any conclusion. " +
                        "Please, create test conclusion first"
                    );
                }
                if (test.Conclusion.AnyFeedback) {
                    return ResultsHelper.BadRequest.WithErr("Feedback is already enabled for this test");
                }
                test.Conclusion.SetAnyFeedback(true);
                await db.SaveChangesAsync();
                return Results.Ok(new { IsFeedbackEnabled = test.Conclusion.AnyFeedback });
            }
        }
        internal static async Task<IResult> DisableTestFeedback(
            string testIdString,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {
            if (!Guid.TryParse(testIdString, out var testGuid)) {
                return ResultsHelper.BadRequest.UnknownTest();
            }
            TestId testId = new(testGuid);
            using (var db = await dbFactory.CreateDbContextAsync()) {
                BaseTest? test = await db.TestsSharedInfo.FindAsync(testId);
                if (test is null) {
                    return ResultsHelper.BadRequest.UnknownTest();
                }
                if (!httpContext.IsAuthenticatedUserIsTestCreator(test)) {
                    return ResultsHelper.BadRequest.WithErr("You don't have access to this page");
                }
                return ResultsHelper.BadRequest.WithErr("Not implemented");
            }
        }
    }
}
