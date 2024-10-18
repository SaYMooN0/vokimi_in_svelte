using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Tls;
using System.Collections.Generic;
using vokimi_api.Helpers;
using vokimi_api.Src;
using vokimi_api.Src.constants_store_classes;
using vokimi_api.Src.db_related;
using vokimi_api.Src.db_related.db_entities.draft_published_tests_shared;
using vokimi_api.Src.db_related.db_entities.draft_tests.draft_general_test;
using vokimi_api.Src.db_related.db_entities.draft_tests.draft_tests_shared;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.dtos.responses.test_creation_responses.shared;
using vokimi_api.Src.dtos.shared.general_test_creation;
using vokimi_api.Src.enums;
using static System.Net.Mime.MediaTypeNames;

namespace vokimi_api.Endpoints.tests_operations
{
    public static class DraftTestPublishingEndpoints
    {
        public static async Task<IResult> PublishDraftTest(
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext,
            string testId
        ) {
            DraftTestId draftTestId;
            if (!Guid.TryParse(testId, out Guid testGuid)) {
                return ResultsHelper.BadRequestWithErr("Unable to publish test. Please refresh the page and try again");
            }
            draftTestId = new(testGuid);
            using (var db = await dbFactory.CreateDbContextAsync()) {
                if (CheckDraftTestForPublilshingProblems(db, draftTestId).Count == 0) {
                    return ResultsHelper.BadRequestWithErr("Test has some publishing problems. Please fix them");
                }
                return ResultsHelper.BadRequestWithErr("Not implemented yet");
            }

            return Results.Ok();
        }

        private static List<TestPublishingProblem> CheckDraftTestForPublilshingProblems(
            AppDbContext db,
            DraftTestId testId
        ) {
            BaseDraftTest? t = db.DraftTestsSharedInfo.Find(testId);
            if (t is null) {
                return [new("Test error", "Test not found")];
            }
            return (t.Template) switch {
                TestTemplate.General => CheckDraftGeneralTestForPublilshingProblems(db, testId),
                _ => throw new ArgumentException("Unknown test template")
            };


        }
        private static List<TestPublishingProblem> CheckDraftGeneralTestForPublilshingProblems(
            AppDbContext db,
            DraftTestId testId
        ) {
            DraftGeneralTest? test = db.DraftGeneralTests
                .Include(t => t.MainInfo)
                .Include(t => t.Conclusion)
                .Include(t => t.Questions)
                    .ThenInclude(q => q.Answers)
                    .ThenInclude(a => a.TypeSpecificInfo)
                .Include(t => t.PossibleResults)
                .FirstOrDefault(t => t.Id == testId);
            //check if answ leading to res are fetched
            if (test is null) {
                return [TestPublishingProblem.TestNotFound()];
            }
            List<TestPublishingProblem> problems = CheckTestMainInfoForProblems(test.MainInfo);
            if (test.Conclusion is not null) {
                problems.AddRange(CheckTestConcluisonForProblems(test.Conclusion));
            }
            problems.AddRange(CheckGeneralTestResultsForProblems(test.PossibleResults));
            problems.AddRange(CheckGeneralTestQuestionsForProblems(test.Questions));
            return problems;

        }
        private static List<TestPublishingProblem> CheckTestMainInfoForProblems(DraftTestMainInfo mainInfo) {
            List<string> problems = [];
            if (
                string.IsNullOrWhiteSpace(mainInfo.Name)
                || mainInfo.Name.Length > BaseTestCreationConsts.MaxTestNameLength
                || mainInfo.Name.Length < BaseTestCreationConsts.MinTestNameLength
            ) {
                problems.Add(
                    $"Name of the test must be from " +
                    $"{BaseTestCreationConsts.MinTestNameLength} to " +
                    $"{BaseTestCreationConsts.MaxTestNameLength} characters"
                );
            }
            int descriptionLength = mainInfo.Description?.Length ?? 0;
            if (descriptionLength > BaseTestCreationConsts.MaxTestDescriptionLength) {
                problems.Add($"Description of the test cannot be more than {BaseTestCreationConsts.MaxTestDescriptionLength} characters");
            }
            return problems.Select(TestPublishingProblem.ForMainInfoCategory).ToList();
        }
        private static List<TestPublishingProblem> CheckTestConcluisonForProblems(TestConclusion conclusion) {
            List<string> problems = [];
            int textLength = string.IsNullOrWhiteSpace(conclusion.Text) ? 0 : conclusion.Text.Length;
            if (textLength > BaseTestCreationConsts.ConclusionMaxTextLength) {
                problems.Add($"Conclusion text cannot be longer than {BaseTestCreationConsts.ConclusionMaxTextLength} characters");
            }
            if (textLength == 0 && string.IsNullOrWhiteSpace(conclusion.AdditionalImage)) {
                problems.Add($"Conclusion must have either text or image");
            }
            if (conclusion.AnyFeedback) {
                int feedbackAccomplyingTextLength =
                    string.IsNullOrWhiteSpace(conclusion.FeedbackAccompanyingText) ? 0 :
                    conclusion.FeedbackAccompanyingText.Length;

                if (feedbackAccomplyingTextLength > BaseTestCreationConsts.ConclusionMaxAccompanyingFeedbackTextLength) {
                    problems.Add(
                        "Maximal length of the feedback accompanying text is" +
                        $"{BaseTestCreationConsts.ConclusionMaxAccompanyingFeedbackTextLength} characters"
                    );
                }
                if (conclusion.MaxFeedbackLength > BaseTestCreationConsts.ConclusionMaxFeedbackLength) {
                    problems.Add(
                        "Maximal feedback length cannot be more than " +
                        $"{BaseTestCreationConsts.ConclusionMaxFeedbackLength} characters"
                    );
                }
            }
            return problems.Select(TestPublishingProblem.ForConclusionCategory).ToList();
        }
        private static List<TestPublishingProblem> CheckGeneralTestResultsForProblems(ICollection<DraftGeneralTestResult> results) {
            List<string> problems = [];
            if (results.Count < 2) {
                problems.Add("Test cannot have less than two results");
            } else if (results.Count > GeneralTestCreationConsts.MaxResultsForTestCount) {
                problems.Add($"Test cannot have more than {GeneralTestCreationConsts.MaxResultsForTestCount} results");
            }
            foreach (var result in results) {
                if (result.AnswersLeadingToResult.Count == 0) {
                    problems.Add($"Result '{result.Name}' has no answers leading to it");
                }

                int textLength = string.IsNullOrWhiteSpace(result.Text) ? 0 : result.Text.Length;
                if (textLength < GeneralTestCreationConsts.ResultMinTextLength
                    || textLength > GeneralTestCreationConsts.ResultMaxTextLength
                ) {
                    problems.Add(
                       $"Text of the result with name: '{result.Name}' is {textLength} characters long. " +
                       $"The length must be from {GeneralTestCreationConsts.ResultMinTextLength} " +
                       $"to {GeneralTestCreationConsts.ResultMaxTextLength} characters");
                }
            }
            return problems.Select(TestPublishingProblem.ForResultsCategory).ToList();
        }
        private static List<TestPublishingProblem> CheckGeneralTestQuestionsForProblems(ICollection<DraftGeneralTestQuestion> questions) {
            return [];
        }


    }
}
