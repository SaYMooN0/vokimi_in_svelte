using Microsoft.EntityFrameworkCore;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.db_related;
using vokimi_api.Src.db_related.db_entities.draft_tests.draft_tests_shared;
using vokimi_api.Helpers;
using vokimi_api.Src.dtos.responses.my_tests_page;
using vokimi_api.Src.extension_classes;
using vokimi_api.Src.dtos.responses.test_creation_responses.shared;
using vokimi_api.Services;

namespace vokimi_api.Endpoints.pages
{
    public static class UserTestsEndpoints
    {
        private readonly static IResult UnaithorizedUserTestFetchingErr =
            ResultsHelper.BadRequest.WithErr("Unable to get tests info. Please log out and log in again");
        public static async Task<IResult> GetUserDraftTestsBriefInfo(
            HttpContext httpContext,
            IDbContextFactory<AppDbContext> dbFactory
        ) {

            if (!httpContext.TryGetUserId(out AppUserId userId)) {
                return UnaithorizedUserTestFetchingErr;
            }

            using (var db = await dbFactory.CreateDbContextAsync()) {
                DraftTestBriefInfoResponse[] responseData = db.DraftTestsSharedInfo
                    .Where(t => t.CreatorId == userId)
                    .Include(t => t.MainInfo)
                    .Select(DraftTestBriefInfoResponse.FromDraftTest)
                    .ToArray();
                return Results.Ok(responseData);
            }
        }

        public static async Task<IResult> GetUserPublishedTestsBriefInfo(
            HttpContext httpContext,
            IDbContextFactory<AppDbContext> dbFactory
        ) {
            if (!httpContext.TryGetUserId(out AppUserId userId)) {
                return UnaithorizedUserTestFetchingErr;
            }
            using (var db = await dbFactory.CreateDbContextAsync()) {
                PublishedTestBriefInfoResponse[] responseData = db.TestsSharedInfo
                    .Where(t => t.CreatorId == userId)
                    .Select(PublishedTestBriefInfoResponse.FromTest)
                    .ToArray();

                return Results.Ok(responseData);
            }
        }
        public static async Task<IResult> GetDraftTestOverviewInfo(
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext,
            string testId
        ) {
            DraftTestId draftTestId;
            if (!Guid.TryParse(testId, out var testGuid)) {
                return ResultsHelper.BadRequest.UnknownTest();
            }
            draftTestId = new(testGuid);

            using (var db = await dbFactory.CreateDbContextAsync()) {
                BaseDraftTest? test = await db.DraftTestsSharedInfo
                    .Include(t => t.MainInfo)
                    .FirstOrDefaultAsync(t => t.Id == draftTestId);
                if (test is null) {
                    return ResultsHelper.BadRequest.UnknownTest();
                }

                DraftTestOverviewInfoResponse returnData = httpContext.IsAuthenticatedUserIsTestCreator(test) ?
                    DraftTestOverviewInfoResponse.NewWithViewerIsCreator(test.Template, test.MainInfo.Name) :
                    DraftTestOverviewInfoResponse.NewWithViewerIsNotCreator();
                return Results.Ok(returnData);
            }
        }
        public async static Task<IResult> DeleteDraftTest(
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext,
            VokimiStorageService storageService,
            string testId
        ) {
            DraftTestId draftTestId;
            if (!Guid.TryParse(testId, out var _)) {
                return ResultsHelper.BadRequest.UnknownTest();
            }
            draftTestId = new(new(testId));

            using (var db = await dbFactory.CreateDbContextAsync()) {
                BaseDraftTest? test = await db.DraftTestsSharedInfo
                    .Include(t => t.MainInfo)
                    .FirstOrDefaultAsync(t => t.Id == draftTestId);
                if (test is null) {
                    return ResultsHelper.BadRequest.UnknownTest();
                }
                if (!httpContext.IsAuthenticatedUserIsTestCreator(test)) {
                    return ResultsHelper.BadRequest.NotCreator();
                }
            }
            return ResultsHelper.BadRequest.WithErr("not implemented");
        }
    }
}
