using Microsoft.EntityFrameworkCore;
using vokimi_api.Helpers;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.db_related;
using VokimiShared.src.models.db_classes.test.test_types;
using vokimi_api.Src.extension_classes;
using vokimi_api.Src.enums;
using vokimi_api.Src.db_related.db_entities.users;
using vokimi_api.Src.dtos.responses.view_test_page;
using vokimi_api.Src.dtos.responses.view_test_page.discussions;

namespace vokimi_api.Endpoints.pages
{
    internal static class ViewTestPageEndpoints
    {
        internal static async Task<IResult> CheckTestViewPermission(
            string testId,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {
            if (!Guid.TryParse(testId, out var testGuid)) {
                return ResultsHelper.BadRequest.ServerError();
            }

            TestId tId = new(testGuid);

            using (var db = await dbFactory.CreateDbContextAsync()) {
                BaseTest? test = db.TestsSharedInfo.Find(tId);
                if (test is null) {
                    return Results.Ok(ViewTestAccessCheckResponse.TestNotFound());
                }
                bool haveAccess;
                if (httpContext.TryGetUserId(out AppUserId viewerId)) {
                    haveAccess = await TestAccessValidator.CheckUserAccessToTest(
                        db,
                        test.CreatorId,
                        test.Settings.Privacy,
                        viewerId
                    );
                } else {
                    haveAccess = test.Settings.Privacy == PrivacyValues.Anyone;
                }
                if (haveAccess) {
                    return Results.Ok(ViewTestAccessCheckResponse.Success());
                } else {
                    AppUser? creator = await db.AppUsers.FindAsync(test.CreatorId);
                    ViewTestAccessCheckResponse returnRes = test.Settings.Privacy switch {
                        PrivacyValues.FriendsAndFollowers => ViewTestAccessCheckResponse.FollowingNeeded(creator),
                        PrivacyValues.FriendsOnly => ViewTestAccessCheckResponse.FriendshipNeeded(creator),
                        _ => ViewTestAccessCheckResponse.Denied(),
                    };
                    return Results.Ok(returnRes);
                }

            }
        }
        internal static async Task<IResult> GetBasicTestInfo(
            string testId,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {
            TestId tId;
            if (!Guid.TryParse(testId, out var testGuid)) {
                return ResultsHelper.BadRequest.ServerError();
            }

            tId = new(testGuid);
            using (var db = await dbFactory.CreateDbContextAsync()) {
                BaseTest? test = await db.TestsSharedInfo
                    .Include(t => t.Tags)
                    .Include(t => t.Creator)
                    .FirstOrDefaultAsync(t => t.Id == tId);
                if (test is null) {
                    return ResultsHelper.BadRequest.UnknownTest();
                }
                bool haveAccess;
                if (httpContext.TryGetUserId(out AppUserId viewerId)) {
                    haveAccess = await TestAccessValidator.CheckUserAccessToTest(db, test.CreatorId, test.Settings.Privacy, viewerId);
                } else {
                    haveAccess = test.Settings.Privacy == PrivacyValues.Anyone;
                }
                if (!haveAccess) {
                    return ResultsHelper.BadRequest.NoTestAccess();
                }
                return Results.Ok(ViewTestBasicTestInfoResponse.FromTest(test));
            }
        }
        internal static async Task<IResult> GetTestRatingsInfo(
            string testId,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {
            TestId tId;
            if (!Guid.TryParse(testId, out var testGuid)) {
                return ResultsHelper.BadRequest.ServerError();
            }

            tId = new(testGuid);
            using (var db = dbFactory.CreateDbContext()) {
                BaseTest? test = await db.TestsSharedInfo
                    .Include(t => t.Ratings)
                        .ThenInclude(tr => tr.User)
                    .FirstOrDefaultAsync(t => t.Id == tId);
                if (test is null) {
                    return ResultsHelper.BadRequest.UnknownTest();
                }
                bool haveAccess;
                ushort? viewerRating = null;

                if (httpContext.TryGetUserId(out AppUserId viewerId)) {
                    haveAccess = await TestAccessValidator.CheckUserAccessToTest(
                        db,
                        test.CreatorId,
                        test.Settings.Privacy,
                        viewerId
                    );
                    if (haveAccess) {
                        viewerRating = test.Ratings.FirstOrDefault(tr => tr.UserId == viewerId)?.Rating ?? null;
                    }
                } else {
                    haveAccess = test.Settings.Privacy == PrivacyValues.Anyone;
                }
                if (!haveAccess) {
                    return ResultsHelper.BadRequest.NoTestAccess();
                }

                return Results.Ok(ViewTestRatingsBaseInfoResponse.New(viewerRating, test));
            }
        }
        internal static async Task<IResult> GetTestDiscussionsInfo(
            string testId,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {
            TestId tId;
            if (!Guid.TryParse(testId, out var testGuid)) {
                return ResultsHelper.BadRequest.ServerError();
            }

            tId = new(testGuid);
            using (var db = await dbFactory.CreateDbContextAsync()) {
                BaseTest? test = await db.TestsSharedInfo
                    .Include(t => t.DiscussionsComments)
                        .ThenInclude(td => td.CommentVotes)
                    .Include(t => t.DiscussionsComments)
                        .ThenInclude(td => td.Author)
                    .FirstOrDefaultAsync(t => t.Id == tId);
                if (test is null) {
                    return ResultsHelper.BadRequest.UnknownTest();
                }
                bool haveAccess;

                Dictionary<TestDiscussionsCommentId, bool> viewersVotes = [];
                if (httpContext.TryGetUserId(out AppUserId viewerId)) {
                    haveAccess = await TestAccessValidator.CheckUserAccessToTest(
                        db,
                        test.CreatorId,
                        test.Settings.Privacy,
                        viewerId
                    );
                    AppUser? viewer = await db.AppUsers
                        .Include(u => u.DiscussionsCommentVotes)
                        .FirstOrDefaultAsync(u => u.Id == viewerId);
                    if (viewer is not null) {
                        viewersVotes = viewer.DiscussionsCommentVotes.ToDictionary(v => v.CommentId, v => v.IsUp);
                    }
                } else {
                    haveAccess = test.Settings.Privacy == PrivacyValues.Anyone;
                }
                if (!haveAccess) {
                    return ResultsHelper.BadRequest.NoTestAccess();
                }

                return Results.Ok(ViewTestDiscussionsBaseInfoResponse.New(test.DiscussionsComments, viewersVotes));
            }
        }

    }
}
