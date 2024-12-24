using Microsoft.EntityFrameworkCore;
using vokimi_api.Helpers;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.db_related;
using vokimi_api.Src.dtos.responses.view_test_page;
using vokimi_api.Src.enums;
using vokimi_api.Src.extension_classes;
using vokimi_api.Src.db_related.db_entities.published_tests.general_test_related;
using vokimi_api.Src.dtos.responses.test_taking;
using vokimi_api.Src;
using vokimi_api.Src.db_related.db_entities.users;
using vokimi_api.Src.db_related.db_entities.test_taken_records;
using Microsoft.AspNetCore.Mvc;
using vokimi_api.Src.dtos.responses.test_taking.general;
using vokimi_api.Src.dtos.requests.test_taken_request;
using vokimi_api.Src.db_related.db_entities.published_tests.published_tests_shared;

namespace vokimi_api.Endpoints.pages
{
    public static class TestTakingPageEndpoints
    {
        public static async Task<IResult> LoadTestTakingData(
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
                BaseTest? test = await db.TestsSharedInfo.FindAsync(tId);
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
                    return test.Template switch {
                        TestTemplate.General => await LoadGeneralTestTakingData(tId, db),
                        _ => ResultsHelper.BadRequest.UnknownTest()
                    };
                } else {
                    return ResultsHelper.BadRequest.NoTestAccess();
                }

            }
        }
        private static async Task<IResult> LoadGeneralTestTakingData(TestId testId, AppDbContext db) {
            TestGeneralTemplate? test = await db.TestsGeneralTemplate
                .Include(t => t.Conclusion)
                .Include(t => t.StylesSheet)
                .Include(t => t.Questions)
                    .ThenInclude(q => q.Answers)
                        .ThenInclude(a => a.TypeSpecificInfo)
                .FirstOrDefaultAsync(t => t.Id == testId);
            if (test is null) {
                return Results.Ok(ViewTestAccessCheckResponse.TestNotFound());
            }
            string jsonOutput = Newtonsoft.Json.JsonConvert.SerializeObject(
                        GeneralTestTakingData.FromGeneralTest(test),
                        new Newtonsoft.Json.JsonSerializerSettings() {
                            Formatting = Newtonsoft.Json.Formatting.Indented,
                            ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                        }
                    );
            return Results.Ok(jsonOutput);
        }
        public static async Task<IResult> GeneralTestTakenRequest(
            [FromBody] GeneralTestTakenRequest takenRequest,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {

            Err requestValidatingErr = takenRequest.CheckRequestForErr();
            if (requestValidatingErr.NotNone()) {
                return ResultsHelper.BadRequest.WithErr(requestValidatingErr);
            }
            using (var db = await dbFactory.CreateDbContextAsync()) {
                TestId testId = takenRequest.GetParsedId().Value;
                TestGeneralTemplate? test = await db.TestsGeneralTemplate
                    .Include(t => t.PossibleResults)
                    .Include(t => t.Questions)
                        .ThenInclude(q => q.Answers)
                            .ThenInclude(a => a.RelatedResults)
                    .Include(t => t.Conclusion)
                    .FirstOrDefaultAsync(t => t.Id == testId);
                if (test is null) {
                    return Results.Ok(ViewTestAccessCheckResponse.TestNotFound());
                }
                bool haveAccess;
                if (httpContext.TryGetUserId(out AppUserId testTakerId)) {
                    haveAccess = await TestAccessValidator.CheckUserAccessToTest(
                        db,
                        test.CreatorId,
                        test.Settings.Privacy,
                        testTakerId
                    );
                } else {
                    haveAccess = test.Settings.Privacy == PrivacyValues.Anyone;
                }
                if (!haveAccess) {
                    return ResultsHelper.BadRequest.WithErr(
                        "You don't have access to this test. Please contact the creator to restore it."
                    );
                }
                if (test.Conclusion is not null) {
                    Err feedbackValidatingErr = takenRequest.CheckFeedbackForErr(test.Conclusion.MaxFeedbackLength);
                    if (feedbackValidatingErr.NotNone()) {
                        return ResultsHelper.BadRequest.WithErr(feedbackValidatingErr);
                    }
                }
                Dictionary<GeneralTestQuestionId, GeneralTestAnswerId[]> parsedChosenAnswers =
                    takenRequest.GetParsedAnswers();
                if (parsedChosenAnswers.Count() < 0) {
                    return ResultsHelper.BadRequest.ServerError();
                }
                Err answersErr = CheckGeneralTestChosenAnswersForErr(parsedChosenAnswers, test);
                if (answersErr.NotNone()) {
                    return ResultsHelper.BadRequest.WithErr(answersErr);
                }
                GeneralTestResult? resultToSetAsReceived = ChooseResultToReceive(parsedChosenAnswers.Values, test);
                if (resultToSetAsReceived is null) {
                    return ResultsHelper.BadRequest.ServerError();
                }

                AppUser? testTaker = null;
                if (httpContext.TryGetUserId(out testTakerId)) {
                    testTaker = db.AppUsers.Find(testTakerId);
                }
                GeneralTestTakenRecord testTakenRecord = GeneralTestTakenRecord.CreateNew(
                    test,
                    testTaker,
                    resultToSetAsReceived.Id
                    
                );
                if(!string.IsNullOrEmpty(takenRequest.TestFeedback)) {
                    TestFeedbackRecord feedbackRecord = TestFeedbackRecord.CreateNew(userId, testId, text, date);
                }
                try {

                    db.GeneralTestTakenRecords.Add(testTakenRecord);
                    await db.SaveChangesAsync();
                    return Results.Ok(new { ReceivedResultId = testTakenRecord.ReceivedResultId.ToString() });
                } catch {
                    return ResultsHelper.BadRequest.ServerError();
                }

            }

        }
        private static Err CheckGeneralTestChosenAnswersForErr(
            Dictionary<GeneralTestQuestionId, GeneralTestAnswerId[]> chosenAnswersForTest,
            TestGeneralTemplate test
        ) {
            HashSet<GeneralTestAnswerId> answersForTest = test.Questions
                .SelectMany(
                    q => q.Answers.Select(a => a.Id))
                .ToHashSet();
            foreach (var currentQuestion in test.Questions) {
                if (chosenAnswersForTest.TryGetValue(currentQuestion.Id, out GeneralTestAnswerId[] chosenAnswerIdsForQuestion)) {

                    int answersCount = chosenAnswerIdsForQuestion.Count();
                    if (answersCount < currentQuestion.MinAnswersCount
                        || answersCount > currentQuestion.MaxAnswersCount
                    ) {
                        return new Err(
                            $"Problem with question #{currentQuestion.OrderInTest + 1}. Answers count: {answersCount}. " +
                            $"Minimum answers count: {currentQuestion.MinAnswersCount}, " +
                            $"Maximal answers count: {currentQuestion.MaxAnswersCount}, "
                        );
                    }
                    foreach (var aId in chosenAnswerIdsForQuestion) {
                        if (!answersForTest.Contains(aId)) {
                            return new Err($"Problem with question #{currentQuestion.OrderInTest + 1}");
                        }

                    }
                } else {
                    return new Err($"Problem with question #{currentQuestion.OrderInTest + 1}");
                }

            }
            return Err.None;
        }
        private static GeneralTestResult? ChooseResultToReceive(
            IEnumerable<GeneralTestAnswerId[]> chosenAnswers,
            TestGeneralTemplate test
        ) {
            Dictionary<GeneralTestResultId, int> resultsWithPoints = [];
            Dictionary<GeneralTestAnswerId, GeneralTestResultId[]> testAnswerWithRelatedResults =
                test.Questions
                    .SelectMany(q => q.Answers)
                    .ToDictionary(
                        a => a.Id,
                        a => a.RelatedResults
                            .Select(r => r.Id)
                            .ToArray()
                    );
            var testQuestions = test.Questions
                .OrderBy(q => q.OrderInTest)
                .ToArray();
            foreach (var chosenAnswersForQuestion in chosenAnswers) {
                foreach (var chosenAnswer in chosenAnswersForQuestion) {
                    if (!testAnswerWithRelatedResults.TryGetValue(chosenAnswer, out var resultsForAnswer)) {
                        return null;
                    }
                    foreach (var relatedResultId in resultsForAnswer) {
                        if (resultsWithPoints.TryGetValue(relatedResultId, out var _)) {
                            resultsWithPoints[relatedResultId]++;
                        } else {
                            resultsWithPoints[relatedResultId] = 1;

                        }
                    }
                }
            }
            KeyValuePair<GeneralTestResultId, int>? resultToReceiveIdWithPoints = resultsWithPoints
                .OrderBy(kvp => kvp.Value)
                .LastOrDefault();
            if (resultToReceiveIdWithPoints is null) {
                return null;
            }
            GeneralTestResultId resultToReceiveId = resultToReceiveIdWithPoints.Value.Key;
            return test.PossibleResults.FirstOrDefault(r => r.Id == resultToReceiveId);
        }
        public static async Task<IResult> GetGeneralTestReceivedResultData(
            string resultId,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {
            GeneralTestResultId resId;
            if (!Guid.TryParse(resultId, out var resGuid)) {
                return ResultsHelper.BadRequest.ServerError();
            }

            resId = new(resGuid);

            using (var db = await dbFactory.CreateDbContextAsync()) {
                GeneralTestResult? result = await db.GeneralTestResults.FindAsync(resId);

                if (result is null) {
                    return ResultsHelper.BadRequest.WithErr("Unknown result");
                }
                TestGeneralTemplate? test = await db.TestsGeneralTemplate
                    .Include(t => t.PossibleResults)
                    .Include(t => t.TestTakings)
                    .FirstOrDefaultAsync(t => t.Id == result.TestId);
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
                return Results.Ok(GeneralTestTakenReceivedResultResponse.New(
                    result,
                    test.PossibleResults,
                    test.TestTakings.Count
                ));

            }
        }
    }
}
