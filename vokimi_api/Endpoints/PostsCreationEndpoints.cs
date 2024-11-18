using Microsoft.EntityFrameworkCore;
using vokimi_api.Helpers;
using vokimi_api.Src.db_related;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.enums;
using vokimi_api.Src.extension_classes;
using VokimiShared.src.models.db_classes.test.test_types;

namespace vokimi_api.Endpoints
{
    internal static class PostsCreationEndpoints
    {
        internal static async Task<IResult> GetTestTakenPostCreationData(
            HttpContext httpContext,
            IDbContextFactory<AppDbContext> dbFactory,
            string testId,
            string receivedResultId
        ) {
            if (!Guid.TryParse(receivedResultId, out var resultGuid)) {
                return ResultsHelper.BadRequest.WithErr("Unable to get result. Please refresh the page");
            }
            if (!Guid.TryParse(testId, out var testGuid)) {
                return ResultsHelper.BadRequest.WithErr("Unable to get test. Please refresh the page");
            }
            TestId tId = new(testGuid);
            using (var db = await dbFactory.CreateDbContextAsync()) {
                BaseTest? test = await db.TestsSharedInfo.FindAsync(tId);
                if (test is null) {
                    return ResultsHelper.BadRequest.WithErr("Test not found");
                }
                if (!test.Settings.TestTakenPostsAllowed) {
                    return ResultsHelper.BadRequest.WithErr("Creator of the test has forbidden this functionality");
                }
                if (!httpContext.TryGetUserId(out var viewerId)) {
                    return ResultsHelper.BadRequest.LogOutLogIn();
                }
                bool hasAccess = await TestAccessValidator.CheckUserAccessToTest(
                    db,
                    test.CreatorId,
                    test.Settings.Privacy,
                    viewerId
                );
                if (!hasAccess) {
                    return ResultsHelper.BadRequest.NoTestAccess();
                }
                return test.Template switch {
                    TestTemplate.General => GetGeneralTestTakenPostCreationData(viewerId, db, resultGuid),
                    TestTemplate.Scoring => GetScoringTestTakenPostCreationData(viewerId, db, resultGuid),
                    TestTemplate.CorrectAnswers => GetCorrectAnswersTestTakenPostCreationData(viewerId, db, resultGuid),
                    _ => ResultsHelper.BadRequest.WithErr("Unknown test template")
                };
            }
        }
        private static IResult GetGeneralTestTakenPostCreationData(
            AppUserId viewerId,
            AppDbContext db,
            Guid resultId
        ) {
            return ResultsHelper.BadRequest.WithErr("Not implemented <General>");
        }
        private static IResult GetScoringTestTakenPostCreationData(
           AppUserId viewerId,
           AppDbContext db,
           Guid resultId
        ) {
            return ResultsHelper.BadRequest.WithErr("Not implemented <Scoring>");
        }
        private static IResult GetCorrectAnswersTestTakenPostCreationData(
           AppUserId viewerId,
           AppDbContext db,
           Guid resultId
        ) {
            return ResultsHelper.BadRequest.WithErr("Not implemented <CorrectAnswers>");
        }
    }
}
