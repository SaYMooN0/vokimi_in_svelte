using Microsoft.EntityFrameworkCore;
using vokimi_api.Helpers;
using vokimi_api.Src.db_related.db_entities.users;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.db_related;
using vokimi_api.Src.dtos.responses.view_test_page;
using vokimi_api.Src.enums;
using vokimi_api.Src.extension_classes;
using VokimiShared.src.models.db_classes.test.test_types;
using vokimi_api.Src.db_related.db_entities.published_tests.general_test_related;
using vokimi_api.Src.dtos.responses.test_taking;

namespace vokimi_api.Endpoints.pages
{
    public static class TestTakingPageEndpoints
    {
        public static IResult LoadTestTakingData(
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
                    return test.Template switch {
                        TestTemplate.General => LoadGeneralTestTakingData(tId, db),
                        _ => ResultsHelper.BadRequestUnknownTest()
                    };
                } else {
                    return ResultsHelper.BadRequestNoTestAccess();
                }

            }
        }
        private static IResult LoadGeneralTestTakingData(TestId testId, AppDbContext db) {
            TestGeneralTemplate? test = db.TestsGeneralType
                .Include(t => t.Conclusion)
                .Include(t => t.Questions)
                    .ThenInclude(q => q.Answers)
                        .ThenInclude(a => a.TypeSpecificInfo)
                .Include(t => t.Questions)
                    .ThenInclude(q => q.Answers)
                        .ThenInclude(a => a.RelatedResults)
                .FirstOrDefault(t => t.Id == testId);
            if (test is null) {
                return Results.Ok(ViewTestAccessCheckResponse.TestNotFound());
            }
            return Results.Ok(GeneralTestTakingData.FromGeneralTest(test)wo;
        }
    }
}
