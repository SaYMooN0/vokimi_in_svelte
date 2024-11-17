using Microsoft.EntityFrameworkCore;
using vokimi_api.Helpers;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.db_related;
using VokimiShared.src.models.db_classes.test.test_types;
using vokimi_api.Src.extension_classes;
using vokimi_api.Src.enums;
using vokimi_api.Src.db_related.db_entities.users;
using vokimi_api.Src.dtos.responses.view_test_page;
using vokimi_api.Src.dtos.requests;
using Microsoft.AspNetCore.Mvc;
using vokimi_api.Src.db_related.db_entities.tests_related;
using vokimi_api.Src.dtos.responses.view_test_page.discussions;

namespace vokimi_api.Endpoints.pages
{
    internal static class ViewTestPageEndpoints
    {
        internal static IResult CheckTestViewPermission(
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
                    haveAccess = TestAccessValidator.CheckUserAccessToTest(db, test.CreatorId, test.Settings.Privacy, viewerId);
                } else {
                    haveAccess = test.Settings.Privacy == PrivacyValues.Anyone;
                }
                if (haveAccess) {
                    return Results.Ok(ViewTestAccessCheckResponse.Success());
                } else {
                    AppUser creator = db.AppUsers.Find(test.CreatorId);
                    ViewTestAccessCheckResponse returnRes = test.Settings.Privacy switch {
                        PrivacyValues.FriendsAndFollowers => ViewTestAccessCheckResponse.FollowingNeeded(creator),
                        PrivacyValues.FriendsOnly => ViewTestAccessCheckResponse.FriendshipNeeded(creator),
                        _ => ViewTestAccessCheckResponse.Denied(),
                    };
                    return Results.Ok(returnRes);
                }

            }
        }
        internal static IResult GetBasicTestInfo(
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
                    haveAccess = TestAccessValidator.CheckUserAccessToTest(db, test.CreatorId, test.Settings.Privacy, viewerId);
                } else {
                    haveAccess = test.Settings.Privacy == PrivacyValues.Anyone;
                }
                if (!haveAccess) {
                    return ResultsHelper.BadRequestNoTestAccess();
                }
                return Results.Ok(ViewTestBasicTestInfoResponse.FromTest(test));
            }
        }
        internal static IResult GetTestRatingsInfo(
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
                    .Include(t => t.Ratings)
                        .ThenInclude(tr => tr.User)
                    .FirstOrDefault(t => t.Id == tId);
                if (test is null) {
                    return ResultsHelper.BadRequestUnknownTest();
                }
                bool haveAccess;
                ushort? viewerRating = null;

                if (httpContext.TryGetUserId(out AppUserId viewerId)) {
                    haveAccess = TestAccessValidator.CheckUserAccessToTest(db, test.CreatorId, test.Settings.Privacy, viewerId);
                    if (haveAccess) {
                        viewerRating = test.Ratings.FirstOrDefault(tr => tr.UserId == viewerId)?.Rating ?? null;
                    }
                } else {
                    haveAccess = test.Settings.Privacy == PrivacyValues.Anyone;
                }
                if (!haveAccess) {
                    return ResultsHelper.BadRequestNoTestAccess();
                }

                return Results.Ok(ViewTestRatingsBaseInfoResponse.New(viewerRating, test));
            }
        }
        internal static IResult GetTestDiscussionsInfo(
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
                    .Include(t => t.DiscussionsComments)
                        .ThenInclude(td => td.CommentVotes)
                    .Include(t => t.DiscussionsComments)
                        .ThenInclude(td => td.Author)
                    .FirstOrDefault(t => t.Id == tId);
                if (test is null) {
                    return ResultsHelper.BadRequestUnknownTest();
                }
                bool haveAccess;

                Dictionary<TestDiscussionsCommentId, bool> viewersVotes = [];
                if (httpContext.TryGetUserId(out AppUserId viewerId)) {
                    haveAccess = TestAccessValidator.CheckUserAccessToTest(db, test.CreatorId, test.Settings.Privacy, viewerId);
                    AppUser? viewer = db.AppUsers
                        .Include(u => u.DiscussionsCommentVotes)
                        .FirstOrDefault(u => u.Id == viewerId);
                    if (viewer is not null) {
                        viewersVotes = viewer.DiscussionsCommentVotes.ToDictionary(v => v.CommentId, v => v.IsUp);
                    }
                } else {
                    haveAccess = test.Settings.Privacy == PrivacyValues.Anyone;
                }
                if (!haveAccess) {
                    return ResultsHelper.BadRequestNoTestAccess();
                }

                return Results.Ok(ViewTestDiscussionsBaseInfoResponse.New(test.DiscussionsComments, viewersVotes));
            }
        }

    }
}
