using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using vokimi_api.Helpers;
using vokimi_api.Src;
using vokimi_api.Src.db_related;
using vokimi_api.Src.db_related.db_entities.published_tests.published_tests_shared;
using vokimi_api.Src.db_related.db_entities.test_collections;
using vokimi_api.Src.db_related.db_entities.users;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.dtos.requests.collections_interactions;
using vokimi_api.Src.dtos.responses;
using vokimi_api.Src.extension_classes;

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
            string testId,
            [FromBody] string[] collectionIds,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {
            if (!Guid.TryParse(testId, out var tesdGuid)) {
                return ResultsHelper.BadRequest.ServerError();
            }
            if (!httpContext.TryGetUserId(out AppUserId userId)) {
                return ResultsHelper.BadRequest.LogOutLogIn();
            }
            TestId tId = new(tesdGuid);
            using (var db = await dbFactory.CreateDbContextAsync()) {
                BaseTest? test = await db.TestsSharedInfo
                    .Include(t => t.CollectionTestIn)
                    .FirstOrDefaultAsync(t => t.Id == tId);
                if (test is null) {
                    return ResultsHelper.BadRequest.UnknownTest();
                }
                HashSet<TestCollectionId> testCollectionIds = collectionIds
                    .Where(i => Guid.TryParse(i, out var _))
                    .Select(i => new TestCollectionId(new Guid(i)))
                    .ToHashSet();
                test.CollectionTestIn.Clear();
                if (testCollectionIds.Count == 0) {
                    await db.SaveChangesAsync();
                    return Results.Ok(new string[] { });
                }
                var newCollections = db.TestCollections.Where(tc => testCollectionIds.Contains(tc.Id));
                foreach (var collection in newCollections) {
                    test.CollectionTestIn.Add(collection);
                }
                await db.SaveChangesAsync();
                var response = test.CollectionTestIn
                    .Select(tc => tc.Id.Value);
                return Results.Ok(response);
            }
        }
        public static async Task<IResult> CreateNewCollection(
            [FromBody] CollectionCreationRequest request,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {
            Err requestErr = request.CheckForErr();
            if (requestErr.NotNone()) {
                return ResultsHelper.BadRequest.WithErr(requestErr);
            }
            if (!httpContext.TryGetUserId(out var userId)) {
                return ResultsHelper.BadRequest.WithErr("You need to be logged in to create new collections");
            }
            using (var db = await dbFactory.CreateDbContextAsync()) {
                TestCollection tCollection = TestCollection.CreateNew(
                    request.CollectionName,
                    userId,
                    request.CollectionColor,
                    request.CollectionIcon
                );
                await db.TestCollections.AddAsync(tCollection);
                await db.SaveChangesAsync();
                return Results.Ok();
            }
        }
    }
}
