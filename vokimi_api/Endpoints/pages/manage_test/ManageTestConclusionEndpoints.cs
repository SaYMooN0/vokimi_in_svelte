using Microsoft.EntityFrameworkCore;
using vokimi_api.Helpers;
using vokimi_api.Src.db_related.db_entities.published_tests.published_tests_shared;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.db_related;
using vokimi_api.Src.extension_classes;
using vokimi_api.Src.dtos.responses.manage_test_page.conclusion;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.Json.Serialization.Metadata;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using vokimi_api.Src.db_related.db_entities.draft_published_tests_shared;
using Microsoft.AspNetCore.Mvc;
using vokimi_api.Src.dtos.requests.manage_test;
using vokimi_api.Src;
using vokimi_api.Src.dtos.shared;
using static System.Net.Mime.MediaTypeNames;
using vokimi_api.Src.db_related.db_entities.tests_related;

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
                    .Include(t => t.FeedbackRecords)
                        .ThenInclude(fr => fr.AppUser)
                    .Include(t => t.Conclusion)
                    .FirstOrDefaultAsync(t => t.Id == testId);
                if (t is null) {
                    return ResultsHelper.BadRequest.UnknownTest();
                }
                if (!httpContext.IsAuthenticatedUserIsTestCreator(t)) {
                    return ResultsHelper.BadRequest.WithErr("You don't have access to this page");
                }
                if (t.Conclusion is null) { return Results.Ok(new { TestHasConclusion = false }); }


                var response = new {
                    TestHasConclusion = true,
                    ConclusionData = ManageTestConclusionTabDataResponse.FromTest(t)
                };
                return Results.Ok(response);
            }
        }
        internal static async Task<IResult> DeleteConclusion(
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
                    return ResultsHelper.BadRequest.WithErr("This test didn't have conclusion");
                }
                db.Remove(test.Conclusion);
                test.SetConclusion(null);
                await db.SaveChangesAsync();

                return Results.Ok();
            }
        }
        internal static async Task<IResult> CreateConclusionForTest(
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
                if (test.Conclusion is not null) {
                    return ResultsHelper.BadRequest.WithErr("This test already has conclusion");
                }
                TestConclusion conclusion = TestConclusion.CreateNew();
                test.SetConclusion(conclusion);
                await db.SaveChangesAsync();
                return Results.Ok();
            }
        }
        internal static async Task<IResult> UpdateConclusionForTest(
            string testIdString,
            [FromBody] TestConclusionData request,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {
            if (!Guid.TryParse(testIdString, out var testGuid)) {
                return ResultsHelper.BadRequest.WithErr("Error in data transfer. Please refresh the page and try again.");
            }
            TestId testId = new(testGuid);

            Err requestErr = request.CheckForErr();
            if (requestErr.NotNone()) {
                return ResultsHelper.BadRequest.WithErr(requestErr);
            }
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
                    test.SetConclusion(TestConclusion.CreateNew());
                }
                test.Conclusion.Update(
                    request.Text,
                    request.AdditionalImage,
                    request.AnyFeedback,
                    request.FeedbackText,
                    request.MaxFeedbackLength
                );
                await db.SaveChangesAsync();
                return Results.Ok();
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
        internal static async Task<IResult> GetTestFeedbackRecords(
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
                    .Include(t => t.FeedbackRecords)
                        .ThenInclude(fr => fr.AppUser)
                    .Include(t => t.Conclusion)
                    .FirstOrDefaultAsync(t => t.Id == testId);
                if (test is null) {
                    return ResultsHelper.BadRequest.UnknownTest();
                }
                if (!httpContext.IsAuthenticatedUserIsTestCreator(test)) {
                    return ResultsHelper.BadRequest.WithErr("You don't have access to this page");
                }
                if (test.Conclusion is null) {
                    return Results.BadRequest("This test doesn't have conclusion so it doesn't have feedback");
                }

                var response = test.FeedbackRecords.Select(FeedbackRecordData.FromTestFeedbackRecord).ToArray();
                return Results.Ok(response);
            }
        }
        internal static async Task<IResult> GetTestFeedbackFilteredRecords(
            string testIdString,
            [FromBody] FeedbackRecordsFilter filter,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {
            if (!Guid.TryParse(testIdString, out var testGuid)) {
                return ResultsHelper.BadRequest.UnknownTest();
            }

            var filterErr = filter.CheckForErr();
            if (filterErr.NotNone()) {
                return ResultsHelper.BadRequest.WithErr(filterErr);
            }

            var testId = new TestId(testGuid);

            using (var db = await dbFactory.CreateDbContextAsync()) {
                var test = await db.TestsSharedInfo
                    .Include(t => t.FeedbackRecords)
                        .ThenInclude(fr => fr.AppUser)
                    .Include(t => t.Conclusion)
                    .FirstOrDefaultAsync(t => t.Id == testId);

                if (test == null || !httpContext.IsAuthenticatedUserIsTestCreator(test) || test.Conclusion == null) {
                    return ResultsHelper.BadRequest.UnknownTest();
                }

                var query = FormFilteredFeedbackQuery(db, filter, testId);

                var filteredFeedbackRecords = query
                    .Select(FeedbackRecordData.FromTestFeedbackRecord)
                    .ToArray();

                return Results.Ok(filteredFeedbackRecords);
            }
        }

        private static IQueryable<TestFeedbackRecord> FormFilteredFeedbackQuery(
            AppDbContext db,
            FeedbackRecordsFilter filter,
            TestId testId
        ) {
            var query = db.TestFeedbackRecords.AsQueryable();

            if (filter.MinLength.HasValue) {
                query = query.Where(fr => fr.Text.Length >= filter.MinLength);
            }
            if (filter.MaxLength.HasValue) {
                query = query.Where(fr => fr.Text.Length <= filter.MaxLength);
            }
            if (filter.FeedbackType != FeedbackRecordsFilter.FeedbackType_All) {
                query = filter.FeedbackType switch {
                    FeedbackRecordsFilter.FeedbackType_Anonymous => query.Where(fr => fr.AppUser == null),
                    FeedbackRecordsFilter.FeedbackType_WithAuthor => query.Where(fr => fr.AppUser != null),
                    _ => query
                };
            }
            if (filter.DateFrom.HasValue) {
                query = query.Where(fr => fr.CreatedAt >= filter.DateFrom.Value);
            }
            if (filter.DateTo.HasValue) {
                query = query.Where(fr => fr.CreatedAt <= filter.DateTo.Value);
            }

            return query.Where(fr => fr.TestId == testId);
        }
    }
}
