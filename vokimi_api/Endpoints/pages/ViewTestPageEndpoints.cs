using Microsoft.EntityFrameworkCore;
using vokimi_api.Helpers;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.db_related;
using VokimiShared.src.models.db_classes.test.test_types;
using vokimi_api.Src.extension_classes;
using vokimi_api.Src.enums;
using vokimi_api.Src.db_related.db_entities.users;
using vokimi_api.Src.dtos.responses;
using static System.Net.Mime.MediaTypeNames;
using vokimi_api.Src.dtos.responses.view_test_page;

namespace vokimi_api.Endpoints.pages
{
    public static class ViewTestPageEndpoints
    {
        public static IResult CheckTestViewPermission(
            string testId,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {
            TestId tId;
            if (!Guid.TryParse(testId, out var testGuid)) {
                return ResultsHelper.BadRequestServerError();
            }

            tId = new(testGuid);

            using (var db = dbFactory.CreateDbContext()) {
                BaseTest? test = db.TestsSharedInfo.Find(tId);
                if (test is null) {
                    return Results.Ok(ViewTestAccessCheckResponse.TestNotFound());
                }
                bool haveAccess;
                if (httpContext.TryGetUserId(out AppUserId viewerId)) {
                    haveAccess = TestAccessValidator.CheckUserAccessToTest(db, test.CreatorId, test.Privacy, viewerId);
                } else {
                    haveAccess = test.Privacy == PrivacyValues.Anyone;
                }
                if (haveAccess) {
                    return Results.Ok(ViewTestAccessCheckResponse.Success());
                } else {
                    AppUser creator = db.AppUsers.Find(test.CreatorId);
                    ViewTestAccessCheckResponse returnRes = test.Privacy switch {
                        PrivacyValues.FriendsAndFollowers => ViewTestAccessCheckResponse.FollowingNeeded(creator),
                        PrivacyValues.FriendsOnly => ViewTestAccessCheckResponse.FriendshipNeeded(creator),
                        _ => ViewTestAccessCheckResponse.Denied(),
                    };
                    return Results.Ok(returnRes);
                }

            }
        }
        public static IResult GetBasicTestInfo(
            string testId,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {
            TestId tId;
            if (!Guid.TryParse(testId, out var testGuid)) {
                return ResultsHelper.BadRequestServerError();
            }

            tId = new(testGuid);
            using (var db = dbFactory.CreateDbContext()) {
                BaseTest? test = db.TestsSharedInfo
                    .Include(t => t.Tags)
                    .Include(t => t.Creator)
                    .FirstOrDefault(t => t.Id == tId);
                if (test is null) {
                    return ResultsHelper.BadRequestUnknownTest();
                }
                bool haveAccess;
                if (httpContext.TryGetUserId(out AppUserId viewerId)) {
                    haveAccess = TestAccessValidator.CheckUserAccessToTest(db, test.CreatorId, test.Privacy, viewerId);
                } else {
                    haveAccess = test.Privacy == PrivacyValues.Anyone;
                }
                if (!haveAccess) {
                    return ResultsHelper.BadRequestNoTestAccess();
                }
                return Results.Ok(ViewTestBasicTestInfoResponse.FromTest(test));
            }
        }
    }
}
