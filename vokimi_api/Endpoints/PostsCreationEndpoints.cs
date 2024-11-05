using Microsoft.EntityFrameworkCore;
using vokimi_api.Helpers;
using vokimi_api.Src.db_related;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.enums;
using VokimiShared.src.models.db_classes.test.test_types;

namespace vokimi_api.Endpoints
{
    internal static class PostsCreationEndpoints
    {
        internal static IResult GetTestTakenPostCreationData(
                HttpContext httpContext,
                IDbContextFactory<AppDbContext> dbFactory,
                string testId,
                string receivedResultId
        ) {
            if (!Guid.TryParse(receivedResultId, out var resultGuid)) {
                return ResultsHelper.BadRequestWithErr("Unable to get result. Please refresh the page");
            }
            if (!Guid.TryParse(testId, out var testGuid)) {
                return ResultsHelper.BadRequestWithErr("Unable to get test. Please refresh the page");
            }
            TestId tId = new(testGuid);
            using (var db = dbFactory.CreateDbContext()) {
                BaseTest? test = db.TestsSharedInfo.Find(tId);
                if (test is null) {
                    return ResultsHelper.BadRequestWithErr("Test not found");
                }
                if (!test.Settings.TestTakenPostsAllowed) {
                    return ResultsHelper.BadRequestWithErr("Creator of the test has forbidden this functionality");
                }
                return test.Template switch {
                    TestTemplate.General => GetGeneralTestTakenPostCreationData(httpContext, db, resultGuid),
                    TestTemplate.Scoring => GetScoringTestTakenPostCreationData(httpContext, db, resultGuid),
                    TestTemplate.CorrectAnswers => GetCorrectAnswersTestTakenPostCreationData(httpContext, db, resultGuid),
                    _ => ResultsHelper.BadRequestWithErr("Unknown test template")
                };
            }



        }
        private static IResult GetGeneralTestTakenPostCreationData(
            HttpContext httpContext,
            AppDbContext db,
            Guid resultId
        ) {
            return ResultsHelper.BadRequestWithErr("Not implemented <General>");
        }
        private static IResult GetScoringTestTakenPostCreationData(
           HttpContext httpContext,
           AppDbContext db,
           Guid resultId
        ) {
            return ResultsHelper.BadRequestWithErr("Not implemented <Scoring>");
        }
        private static IResult GetCorrectAnswersTestTakenPostCreationData(
           HttpContext httpContext,
           AppDbContext db,
           Guid resultId
        ) {
            return ResultsHelper.BadRequestWithErr("Not implemented <CorrectAnswers>");
        }
    }
}
