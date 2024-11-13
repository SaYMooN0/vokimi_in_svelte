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
                    bool haveAccess;
                    if (httpContext.TryGetUserId(out AppUserId viewerId)) {
                        haveAccess = TestAccessValidator.CheckUserAccessToTest(db, test.CreatorId, test.Settings.Privacy, viewerId);
                    } else {
                        haveAccess = test.Settings.Privacy == PrivacyValues.Anyone;
                    }
                    if (!haveAccess) {
                        return ResultsHelper.BadRequestNoTestAccess();
                    }
                    AppUser? viewer = db.AppUsers.Find(viewerId);
                    if (viewer is null) {
                        return ResultsHelper.BadRequestWithErr("An error has occurred. Please log out, login and try again");
                    }
                    var newComment = TestDiscussionsComment.CreateNew(request.CommentText, null, null, test.Id, viewerId);
                    db.TestDiscussionComments.Add(newComment);
                    db.SaveChanges();
                    return Results.Ok();
                }
            } catch {
                return ResultsHelper.BadRequestServerError();
            }
        }
    }
}
