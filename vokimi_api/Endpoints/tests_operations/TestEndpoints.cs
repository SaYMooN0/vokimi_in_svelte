using Microsoft.EntityFrameworkCore;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.db_related;
using vokimi_api.Src.db_related.db_entities.draft_tests.draft_tests_shared;
using vokimi_api.Src.enums;
using vokimi_api.Helpers;
using vokimi_api.Src.dtos.responses.my_tests_page;
using vokimi_api.Src.extension_classes;
using vokimi_api.Src.dtos.responses.test_creation_responses.shared;
using vokimi_api.Services;

namespace vokimi_api.Endpoints.tests_operations
{
    public static class TestEndpoints
    {
        private readonly static IResult UnaithorizedUserTestFetchingErr =
            ResultsHelper.BadRequestWithErr("Unable to get tests info. Please log out and log in again");
        public static IResult GetUserDraftTestsBriefInfo(
            HttpContext httpContext,
            IDbContextFactory<AppDbContext> dbFactory
        ) {

            if (httpContext.TryGetUserId(out AppUserId userId)) {
                using (var db = dbFactory.CreateDbContext()) {
                    DraftTestBriefInfoResponse[] responseData = db.DraftTestsSharedInfo
                        .Where(t => t.CreatorId == userId)
                        .Include(t => t.MainInfo)
                        .Select(DraftTestBriefInfoResponse.FromDraftTest)
                        .ToArray();

                    return Results.Ok(responseData);
                }
            }
            return UnaithorizedUserTestFetchingErr;
        }

        public static IResult GetUserPublishedTestsBriefInfo(
            HttpContext httpContext,
            IDbContextFactory<AppDbContext> dbFactory
        ) {
            if (httpContext.TryGetUserId(out AppUserId userId)) {
                using (var db = dbFactory.CreateDbContext()) {
                    PublishedTestBriefInfoResponse[] responseData = db.TestsSharedInfo
                        .Where(t => t.CreatorId == userId)
                        .Select(PublishedTestBriefInfoResponse.FromTest)
                        .ToArray();

                    return Results.Ok(responseData);
                }
            } else { return UnaithorizedUserTestFetchingErr; }
        }
        public static IResult GetDraftTestOverviewInfo(
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext,
            string testId
        ) {
            DraftTestId draftTestId;
            if (!Guid.TryParse(testId, out var _)) {
                return ResultsHelper.BadRequestUnknownTest();
            }
            draftTestId = new(new(testId));

            using (var db = dbFactory.CreateDbContext()) {
                BaseDraftTest? test = db.DraftTestsSharedInfo
                    .Include(t => t.MainInfo)
                    .FirstOrDefault(t => t.Id == draftTestId);
                if (test is null) {
                    return ResultsHelper.BadRequestUnknownTest();
                }

                DraftTestOverviewInfoResponse returnData = httpContext.IfAuthenticatedUserIdIsTestCreator(test) ?
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
                return ResultsHelper.BadRequestUnknownTest();
            }
            draftTestId = new(new(testId));

            using (var db = dbFactory.CreateDbContext()) {
                BaseDraftTest? test = db.DraftTestsSharedInfo
                    .Include(t => t.MainInfo)
                    .FirstOrDefault(t => t.Id == draftTestId);
                if (test is null) {
                    return ResultsHelper.BadRequestUnknownTest();
                }
                if (!httpContext.IfAuthenticatedUserIdIsTestCreator(test)) {
                    return ResultsHelper.BadRequestNotCreator();
                }
            }
            return ResultsHelper.BadRequestWithErr("not implemented");
        }
    }
}
