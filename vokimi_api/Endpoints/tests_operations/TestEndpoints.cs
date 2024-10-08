using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using vokimi_api.Src.db_related.db_entities.users;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.db_related;
using vokimi_api.Src.dtos.responses;
using vokimi_api.Src.db_related.db_entities.draft_tests.draft_tests_shared;
using Microsoft.AspNetCore.Mvc;
using vokimi_api.Src.enums;
using vokimi_api.Src.dtos.requests.test_creation.templates_shared;
using vokimi_api.Helpers;

namespace vokimi_api.Endpoints.tests_operations
{
    public static class TestEndpoints
    {
        private readonly static IResult UnaithorizedUserTestFetchingErr =
            ResultsHelper.BadRequestWithErr("Unable to get tests info. Please log out and log in again");
        public static async Task<IResult> GetUserDraftTestsBriefInfo(HttpContext httpContext,
                                                                     IDbContextFactory<AppDbContext> dbFactory) {
            var cntxUser = httpContext.User;

            if (cntxUser.Identity?.IsAuthenticated ?? false) {
                string? userIdStr = cntxUser.FindFirstValue(PingAuthResponse.ClaimKeyUserId);

                AppUserId? userId = null;
                if (!string.IsNullOrEmpty(userIdStr)) {
                    userId = new(new Guid(userIdStr));
                } else {
                    return UnaithorizedUserTestFetchingErr;
                }
                using (var db = dbFactory.CreateDbContext()) {

                    TestBriefInfoResponse[] responseData = db.DraftTestsSharedInfo
                        .Where(t => t.CreatorId == userId)
                        .Include(t=>t.MainInfo)
                        .Select(TestBriefInfoResponse.FromDraftTest)
                        .ToArray();
                    return Results.Ok(responseData);
                }
            } else {
                return UnaithorizedUserTestFetchingErr;
            }
        }
        public static async Task<IResult> GetUserPublishedTestsBriefInfo(HttpContext httpContext,
                                                                     IDbContextFactory<AppDbContext> dbFactory) {
            return ResultsHelper.BadRequestWithErr("Not implemented");
        }
        public static IResult GetDraftTestOverviewInfo(
            IDbContextFactory<AppDbContext> dbFactory, [FromBody] DraftTestOverviewInfoRequest request) {

            if (
                Guid.TryParse(request.TestId, out Guid testGuid) &&
                Guid.TryParse(request.ViewerId, out Guid userGuid)) {
                DraftTestId testId = new(testGuid);
                AppUserId userId = new(userGuid);

                using (var db = dbFactory.CreateDbContext()) {
                    BaseDraftTest? test = db.DraftTestsSharedInfo
                        .Include(t => t.MainInfo)
                        .FirstOrDefault(t => t.Id == testId);
                    if (test is null) {
                        return Results.BadRequest();
                    }
                    bool isViewerCreator = test.CreatorId == userId;
                    return Results.Ok(new {
                        IsViewerCreator = isViewerCreator,
                        Template = isViewerCreator ? test.Template.GetId() : "",
                        TestName = isViewerCreator ? test.MainInfo.Name : ""
                    });
                }

            } else { return Results.BadRequest(); }

        }
    }
}
