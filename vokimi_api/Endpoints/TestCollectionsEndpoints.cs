using Microsoft.EntityFrameworkCore;
using vokimi_api.Helpers;
using vokimi_api.Src.db_related;
using vokimi_api.Src.db_related.db_entities.users;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.dtos.responses;
using vokimi_api.Src.extension_classes;
using VokimiShared.src.models.db_classes.test.test_types;

namespace vokimi_api.Endpoints
{
    internal class TestCollectionsEndpoints
    {
        public static async Task<IResult> GetCollectionsInfoForTest(
            string testId,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {
            if (!Guid.TryParse(testId, out var testGuid)) {
                return ResultsHelper.BadRequest.ServerError();
            }
            TestId tId = new(testGuid);
            using (var db = await dbFactory.CreateDbContextAsync()) {
                if (!httpContext.TryGetUserId(out var viewerId)) {
                    return ResultsHelper.BadRequest.WithErr("Only logged in users can access collections");
                }
                BaseTest? test = await db.TestsSharedInfo.FindAsync(tId);
                if (test is null) {
                    return ResultsHelper.BadRequest.WithErr("Test not fount");
                }
                AppUser? viewer = await db.AppUsers
                    .Include(u => u.TestCollections)
                        .ThenInclude(tc => tc.Tests)
                    .FirstOrDefaultAsync(u => u.Id == viewerId);
                if (viewer is null) {
                    return ResultsHelper.BadRequest.LogOutLogIn();
                }
                if (!await TestAccessValidator.CheckUserAccessToTest(db,
                    test.CreatorId,
                    test.Settings.Privacy,
                    viewerId)
                ) {
                    return ResultsHelper.BadRequest.NoTestAccess();
                }
                var response = viewer.TestCollections
                    .Select(c => TestCollectionsVmsForCertainTest.FromCollection(c, tId))
                    .ToArray();
                return Results.Ok(response);
            }
        }
        public static async Task<IResult> HandleTestEntriesInCollectionsChanged(
            IDbContextFactory<AppDbContext> dbFactory
        ) {
            return ResultsHelper.BadRequest.WithErr("Not implemented");
        }
        public static async Task<IResult> CreateNewCollection(
            IDbContextFactory<AppDbContext> dbFactory
        ) {
            return ResultsHelper.BadRequest.WithErr("Not implemented");
        }
    }
}
