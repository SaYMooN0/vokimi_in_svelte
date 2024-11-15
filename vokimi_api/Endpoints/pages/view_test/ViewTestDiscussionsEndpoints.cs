using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vokimi_api.Helpers;
using vokimi_api.Src.db_related.db_entities.tests_related;
using vokimi_api.Src.db_related.db_entities.users;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.db_related;
using vokimi_api.Src.dtos.requests;
using vokimi_api.Src.enums;
using vokimi_api.Src.extension_classes;
using VokimiShared.src.models.db_classes.test.test_types;
using vokimi_api.Src.dtos.requests.view_test_page;
using vokimi_api.Src.db_related.db_entities.tests_related.discussions;
using vokimi_api.Src.dtos.responses.view_test_page.discussions;

namespace vokimi_api.Endpoints.pages.view_test
{
    internal class ViewTestDiscussionsEndpoints
    {
        internal static IResult StartNewDiscussion(
            [FromBody] NewDiscussionCreationRequest request,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {

            var requestErr = request.CheckForErr();
            if (requestErr.NotNone()) {
                return ResultsHelper.BadRequestWithErr(requestErr);
            }
            try {
                using (var db = dbFactory.CreateDbContext()) {
                    TestId testId = request.GetParsedTestId().Value;
                    BaseTest? test = db.TestsSharedInfo
                        .Include(t => t.DiscussionsComments)
                        .FirstOrDefault(t => t.Id == testId);
                    if (test is null) {
                        return ResultsHelper.BadRequestUnknownTest();
                    }
                    if (!test.Settings.DiscussionsOpen) {
                        return ResultsHelper.BadRequestWithErr("Discussions for this test are disabled");
                    }
                    if (!httpContext.TryGetUserId(out AppUserId creatorId)) {
                        return ResultsHelper.BadRequestWithErr("Only logged in users can start new discussions");
                    }
                    if (!TestAccessValidator.CheckUserAccessToTest(db, test.CreatorId, test.Settings.Privacy, creatorId)) {
                        return ResultsHelper.BadRequestNoTestAccess();
                    }
                    AppUser? creator = db.AppUsers.Find(creatorId);
                    if (creator is null) {
                        return ResultsHelper.BadRequestWithErr("An error has occurred. Please log out, login and try again");
                    }
                    var newComment = TestDiscussionsComment.CreateNew(request.CommentText, null, null, test.Id, creatorId);
                    db.TestDiscussionComments.Add(newComment);
                    db.SaveChanges();
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
                return ResultsHelper.BadRequestServerError();
            }
        }
        internal static IResult SaveAnswerToComment(
            [FromBody] SavingAnswerToCommentRequest request,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {

            var requestErr = request.CheckForErr();
            if (requestErr.NotNone()) {
                return ResultsHelper.BadRequestWithErr(requestErr);
            }
            try {
                using (var db = dbFactory.CreateDbContext()) {
                    TestDiscussionsCommentId parentCommentId = request.GetParsedParentCommentId().Value;
                    TestDiscussionsComment? parrentComment = db.TestDiscussionComments.Find(parentCommentId);
                    if (parrentComment is null) {
                        return ResultsHelper.BadRequestWithErr("Unable to find the comment you want to answer");
                    }
                    BaseTest? test = db.TestsSharedInfo.Find(parrentComment.TestId);
                    if (test is null) {
                        return ResultsHelper.BadRequestUnknownTest();
                    }
                    if (!test.Settings.DiscussionsOpen) {
                        return ResultsHelper.BadRequestWithErr("Discussions for this test are disabled");
                    }
                    if (!httpContext.TryGetUserId(out AppUserId creatorId)) {
                        return ResultsHelper.BadRequestWithErr("Only logged in users can answer to comments");
                    }
                    if (!TestAccessValidator.CheckUserAccessToTest(db, test.CreatorId, test.Settings.Privacy, creatorId)) {
                        return ResultsHelper.BadRequestNoTestAccess();
                    }
                    AppUser? creator = db.AppUsers.Find(creatorId);
                    if (creator is null) {
                        return ResultsHelper.BadRequestWithErr("An error has occurred. Please log out, login and try again");
                    }
                    var newComment = TestDiscussionsComment.CreateNew(request.AnswerText, parentCommentId, null, test.Id, creatorId);
                    db.TestDiscussionComments.Add(newComment);
                    db.SaveChanges();
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
                return ResultsHelper.BadRequestServerError();
            }
        }
    }
}
