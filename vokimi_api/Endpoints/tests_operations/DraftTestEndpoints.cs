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

namespace vokimi_api.Endpoints.tests_operations
{
    public static class DraftTestEndpoints
    {

        //TODO: Rewrite to first package and remaining
        public static async Task<IResult> GetUsersDraftTestsVms(
            HttpContext httpContext, IDbContextFactory<AppDbContext> dbFactory) {
            var cntxUser = httpContext.User;

            if (cntxUser.Identity?.IsAuthenticated ?? false) {
                string? userIdStr = cntxUser.FindFirstValue(PingAuthResponse.ClaimKeyUserId);

                AppUserId? userId = null;
                if (!string.IsNullOrEmpty(userIdStr)) {
                    userId = new(new Guid(userIdStr));
                } else {
                    return Results.Unauthorized();
                }
                using (var db = dbFactory.CreateDbContext()) {
                    AppUser? user = await db.AppUsers
                        .Include(u => u.DraftTests)
                        .FirstOrDefaultAsync(u => u.Id == userId);
                    if (user is null) {
                        return Results.BadRequest(new { Err = "Unknown user" });
                    }
                    return Results.Ok(user.DraftTests.Select(UsersTestsVm.FromDraftTest));
                }
            } else {
                return Results.BadRequest();
            }
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
