using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vokimi_api.Helpers;
using vokimi_api.Src.db_related.db_entities.users;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.db_related;
using vokimi_api.Src.enums;
using vokimi_api.Src.extension_classes;
using VokimiShared.src.models.db_classes.test.test_types;
using vokimi_api.Src.db_related.db_entities.tests_related.discussions;
using vokimi_api.Src.dtos.responses.view_test_page.discussions;
using vokimi_api.Src;
using vokimi_api.Src.dtos.requests.view_test_page.discussions;

namespace vokimi_api.Endpoints.pages.view_test
{
    internal class ViewTestDiscussionsEndpoints
    {
        internal static async Task<IResult> StartNewDiscussion(
            [FromBody] NewDiscussionCreationRequest request,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {

            var requestErr = request.CheckForErr();
            if (requestErr.NotNone()) {
                return ResultsHelper.BadRequest.WithErr(requestErr);
            }
            try {
                using (var db = await dbFactory.CreateDbContextAsync()) {
                    TestId testId = request.GetParsedTestId().Value;
                    BaseTest? test = await db.TestsSharedInfo
                        .Include(t => t.DiscussionsComments)
                        .FirstOrDefaultAsync(t => t.Id == testId);
                    if (test is null) {
                        return ResultsHelper.BadRequest.UnknownTest();
                    }
                    if (!test.Settings.DiscussionsOpen) {
                        return ResultsHelper.BadRequest.WithErr("Discussions for this test are disabled");
                    }
                    if (!httpContext.TryGetUserId(out AppUserId creatorId)) {
                        return ResultsHelper.BadRequest.WithErr("Only logged in users can start new discussions");
                    }
                    if (!await TestAccessValidator.CheckUserAccessToTest(db, test.CreatorId, test.Settings.Privacy, creatorId)) {
                        return ResultsHelper.BadRequest.NoTestAccess();
                    }
                    AppUser? creator = await db.AppUsers.FindAsync(creatorId);
                    if (creator is null) {
                        return ResultsHelper.BadRequest.WithErr("An error has occurred. Please log out, login and try again");
                    }
                    var newComment = TestDiscussionsComment.CreateNew(request.CommentText, null, null, test.Id, creatorId);
                    db.TestDiscussionComments.Add(newComment);
                    await db.SaveChangesAsync();
                    TestDiscussionCommentVm response = new(
                        newComment.Id.Value.ToString(),
                        creatorId.ToString(),
                        creator.Username,
                        creator.ProfilePicturePath,
                        request.CommentText,
                        0, 0,
                        DateTime.UtcNow.ToString("HH:mm dd.MM.yyyy"),
                        null, []
                    );
                    return Results.Ok(response);
                }
            } catch {
                return ResultsHelper.BadRequest.ServerError();
            }
        }
        internal static async Task<IResult> SaveAnswerToComment(
            [FromBody] SavingAnswerToCommentRequest request,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {

            var requestErr = request.CheckForErr();
            if (requestErr.NotNone()) {
                return ResultsHelper.BadRequest.WithErr(requestErr);
            }
            try {
                using (var db = await dbFactory.CreateDbContextAsync()) {
                    TestDiscussionsCommentId parentCommentId = request.GetParsedParentCommentId().Value;
                    TestDiscussionsComment? parrentComment = await db.TestDiscussionComments.FindAsync(parentCommentId);
                    if (parrentComment is null) {
                        return ResultsHelper.BadRequest.WithErr("Unable to find the comment you want to answer");
                    }
                    BaseTest? test = await db.TestsSharedInfo.FindAsync(parrentComment.TestId);
                    if (test is null) {
                        return ResultsHelper.BadRequest.UnknownTest();
                    }
                    if (!test.Settings.DiscussionsOpen) {
                        return ResultsHelper.BadRequest.WithErr("Discussions for this test are disabled");
                    }
                    if (!httpContext.TryGetUserId(out AppUserId creatorId)) {
                        return ResultsHelper.BadRequest.WithErr("Only logged in users can answer to comments");
                    }
                    if (!await TestAccessValidator.CheckUserAccessToTest(db, test.CreatorId, test.Settings.Privacy, creatorId)) {
                        return ResultsHelper.BadRequest.NoTestAccess();
                    }
                    AppUser? creator = await db.AppUsers.FindAsync(creatorId);
                    if (creator is null) {
                        return ResultsHelper.BadRequest.WithErr("An error has occurred. Please log out, login and try again");
                    }
                    var newComment = TestDiscussionsComment.CreateNew(request.AnswerText, parentCommentId, null, test.Id, creatorId);
                    db.TestDiscussionComments.Add(newComment);
                    await db.SaveChangesAsync();
                    TestDiscussionCommentVm response = new(
                        newComment.Id.Value.ToString(),
                        creatorId.ToString(),
                        creator.Username,
                        creator.ProfilePicturePath,
                        request.AnswerText,
                        0, 0,
                        DateTime.UtcNow.ToString("HH:mm dd.MM.yyyy"),
                        null, []
                    );
                    return Results.Ok(response);
                }
            } catch {
                return ResultsHelper.BadRequest.ServerError();
            }
        }
        internal async static Task<IResult> GetFilteredDiscussions(
            [FromBody] GetFilteredDiscussionsRequest request,
            HttpContext httpContext,
            IDbContextFactory<AppDbContext> dbFactory
        ) {

            Err requestErr = request.CheckForErr();
            if (requestErr.NotNone()) {
                return ResultsHelper.BadRequest.WithErr(requestErr);
            }
            ParsedDiscussionsFilter? parsedRequest = request.ToParsedObject();
            if (parsedRequest is null) {
                return ResultsHelper.BadRequest.ServerError();
            }
            using (var db = await dbFactory.CreateDbContextAsync()) {
                try {

                    BaseTest? test = await db.TestsSharedInfo
                        .Include(t => t.DiscussionsComments)
                            .ThenInclude(dc => dc.Author)
                        .FirstOrDefaultAsync(t => t.Id == parsedRequest.TestId);
                    if (test is null) {
                        return ResultsHelper.BadRequest.UnknownTest();
                    }
                    bool haveAccess;

                    HashSet<AppUserId> viewersFriends = [];
                    HashSet<AppUserId> viewersFollowers = [];
                    Dictionary<TestDiscussionsCommentId, bool> viewersVotes = [];

                    if (httpContext.TryGetUserId(out AppUserId viewerId)) {
                        haveAccess = await TestAccessValidator.CheckUserAccessToTest(db, test.CreatorId, test.Settings.Privacy, viewerId);
                        AppUser? viewer = await db.AppUsers
                            .Include(u => u.Friends)
                            .Include(u => u.Followers)
                            .Include(u => u.DiscussionsCommentVotes)
                            .FirstOrDefaultAsync(u => u.Id == viewerId);
                        if (viewer is not null) {
                            viewersFriends = viewer.Friends.Select(u => u.Id).ToHashSet();
                            viewersFollowers = viewer.Followers.Select(u => u.Id).ToHashSet();
                            viewersVotes = viewer.DiscussionsCommentVotes.ToDictionary(v => v.CommentId, v => v.IsUp);
                        }
                    } else {
                        haveAccess = test.Settings.Privacy == PrivacyValues.Anyone;
                    }
                    if (!haveAccess) {
                        return ResultsHelper.BadRequest.NoTestAccess();
                    }

                    HashSet<TestDiscussionsComment> filteredComments = test.DiscussionsComments
                        .Where(c => parsedRequest.IsVotesCountFilterPassed(c.CommentVotes.Count))
                        .Where(c => parsedRequest.IsVotesRatingFilterPassed(
                            c.CommentVotes.Sum(v => v.IsUp ? 1 : -1)
                        ))
                        .Where(c => parsedRequest.IsFollowersAndFriendsFilterPassed(
                            isFriend: viewersFriends.Contains(c.AuthorId),
                            isFollower: viewersFollowers.Contains(c.AuthorId)
                            ))
                        .Where(c => parsedRequest.IsChildCommentsCountFilterPassed(CalculateCommentChildCommentsCount(c)))
                        .ToHashSet();
                    var response = filteredComments
                        .Where(c => c.ParentCommentId is null)
                        .Select(c => TestDiscussionCommentVm.FromCommentWithFilter(
                            c,
                            viewersVotes,
                            (child) => filteredComments.Contains(child))
                        )
                        .ToArray();
                    return Results.Ok(response);
                } catch {
                    return ResultsHelper.BadRequest.ServerError();
                }
            }

        }

        private static int CalculateCommentChildCommentsCount(TestDiscussionsComment comment) {
            if (comment.ChildComments == null || !comment.ChildComments.Any())
                return 0;

            return comment.ChildComments.Count + comment.ChildComments.Sum(CalculateCommentChildCommentsCount);
        }
        internal static async Task<IResult> HandleCommentVoteChanged(
            [FromBody] CommentVoteChangedRequest request,
            HttpContext httpContext,
            IDbContextFactory<AppDbContext> dbFactory
        ) {
            return ResultsHelper.BadRequest.WithErr("not implemented");
        }
    }
}
