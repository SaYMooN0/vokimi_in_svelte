﻿using Microsoft.EntityFrameworkCore;
using vokimi_api.Helpers;
using vokimi_api.Services;
using vokimi_api.Src;
using vokimi_api.Src.constants_store_classes;
using vokimi_api.Src.db_related;
using vokimi_api.Src.db_related.db_entities.draft_published_tests_shared;
using vokimi_api.Src.db_related.db_entities.draft_published_tests_shared.general_test_answers;
using vokimi_api.Src.db_related.db_entities.draft_tests.draft_general_test;
using vokimi_api.Src.db_related.db_entities.draft_tests.draft_tests_shared;
using vokimi_api.Src.db_related.db_entities.published_tests.general_test_related;
using vokimi_api.Src.db_related.db_entities.tests_related.tags;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.dtos.responses.test_creation_responses.shared;
using vokimi_api.Src.enums;
using vokimi_api.Src.extension_classes;
using vokimi_api.Src.test_publishing_data;

namespace vokimi_api.Endpoints.pages.test_creation
{
    public static class DraftTestPublishingEndpoints
    {
        public async static Task<IResult> CheckDraftTestForPublishingProblems(
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext,
            string testId
        ) {
            DraftTestId draftTestId;
            if (!Guid.TryParse(testId, out Guid testGuid)) {
                return ResultsHelper.BadRequest.WithErr("Unable to check test for problems. Please refresh the page and try again");
            }
            draftTestId = new(testGuid);
            using (var db = await dbFactory.CreateDbContextAsync()) {
                BaseDraftTest? testToPublish = await db.DraftTestsSharedInfo.FindAsync(draftTestId);
                if (testToPublish is null) {
                    return ResultsHelper.BadRequest.WithErr("Unknown draft test");
                }
                if (!httpContext.IsAuthenticatedUserIsTestCreator(testToPublish)) {
                    return ResultsHelper.BadRequest.WithErr("Only test creator can view this information");
                }

                var problems = await GetTestPublilshingProblems(db, draftTestId, testToPublish.Template);
                return Results.Ok(problems);
            }

        }
        private static async Task<List<TestPublishingProblem>> GetTestPublilshingProblems(
            AppDbContext db,
            DraftTestId testId,
            TestTemplate template
        ) => template switch {
            TestTemplate.General => await GetGeneralTestPublilshingProblems(db, testId),
            _ => throw new ArgumentException("Unknown test template")
        };


        private static async Task<List<TestPublishingProblem>> GetGeneralTestPublilshingProblems(
            AppDbContext db,
            DraftTestId testId
        ) {
            DraftGeneralTest? test = await db.DraftGeneralTests
                .Include(t => t.MainInfo)
                .Include(t => t.Conclusion)
                .Include(t => t.Questions)
                    .ThenInclude(q => q.Answers)
                        .ThenInclude(a => a.TypeSpecificInfo)
                .Include(t => t.Questions)
                    .ThenInclude(q => q.Answers)
                        .ThenInclude(a => a.RelatedResults)
                .Include(t => t.PossibleResults)
                .FirstOrDefaultAsync(t => t.Id == testId);
            if (test is null) {
                return [TestPublishingProblem.TestNotFound()];
            }
            List<TestPublishingProblem> problems = CheckTestMainInfoForProblems(test.MainInfo);
            if (test.Conclusion is not null) {
                problems.AddRange(CheckTestConcluisonForProblems(test.Conclusion));
            }
            problems.AddRange(CheckTestTagsForProblems(test.Tags));
            problems.AddRange(CheckGeneralTestResultsForProblems(test.PossibleResults));
            problems.AddRange(CheckGeneralTestQuestionsForProblems(
                test.Questions.OrderBy(q => q.OrderInTest).ToList()
            ));
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
        private static List<TestPublishingProblem> CheckTestTagsForProblems(string[] tags) {
            List<string> problems = [];
            if (tags.Length > TestTagsConsts.MaxTagsForTestCount) {
                problems.Add($"Tags count must not exceed {TestTagsConsts.MaxTagsForTestCount}");
            }

            foreach (var tag in tags) {
                if (!TestTagsConsts.TagRegex.IsMatch(tag)) {
                    problems.Add(TestTagsConsts.InvalidTagMessage(tag));
                }
            }
            return problems.Select(TestPublishingProblem.ForTagsCategory).ToList();
        }
        private static List<TestPublishingProblem> CheckGeneralTestResultsForProblems(ICollection<DraftGeneralTestResult> results) {
            List<string> problems = [];
            if (results.Count < GeneralTestCreationConsts.MinResultsForTestCount) {
                problems.Add($"Test cannot have less than {GeneralTestCreationConsts.MinResultsForTestCount} results");
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
        private static List<TestPublishingProblem> CheckGeneralTestQuestionsForProblems(
            List<DraftGeneralTestQuestion> questions
        ) {
            List<string> problems = [];
            int questionsCount = questions.Count;
            if (questionsCount < GeneralTestCreationConsts.MinQuestionsForTestCount) {
                problems.Add("General test cannot have " +
                            $"less than {GeneralTestCreationConsts.MinQuestionsForTestCount} questions in it");
            } else if (questionsCount > GeneralTestCreationConsts.MaxQuestionsForTestCount) {
                problems.Add("General test cannot have " +
                            $"more than {GeneralTestCreationConsts.MaxQuestionsForTestCount} questions in it");
            }
            for (int i = 0; i < questionsCount; i++) {
                DraftGeneralTestQuestion q = questions[i];
                Func<string, string> withErrPrefix = (string err) => $"Question #{i + 1}: {err}";
                int textLen = string.IsNullOrWhiteSpace(q.Text) ? 0 : q.Text.Length;

                if (textLen < GeneralTestCreationConsts.QuestionTextMinLength
                    || textLen > GeneralTestCreationConsts.QuestionTextMaxLength
                ) {
                    problems.Add(withErrPrefix(
                        $"Text of the question is {textLen} characters long. " +
                        $"The length must be from {GeneralTestCreationConsts.QuestionTextMinLength} " +
                        $"to {GeneralTestCreationConsts.QuestionTextMaxLength} characters")
                    );
                }

                if (!q.IsSingleChoice) {
                    if (q.MaxAnswersCount > q.Answers.Count) {
                        problems.Add(withErrPrefix("maximum answers count cannot be less than total answers count"));
                    }
                    if (q.MinAnswersCount > q.Answers.Count) {
                        problems.Add(withErrPrefix("minimum answers count cannot be more than total answers count"));
                    }
                    if (q.MinAnswersCount < 1) {
                        problems.Add(withErrPrefix("minimum answers count cannot be less than 1"));
                    }
                    if (q.MinAnswersCount > q.MaxAnswersCount) {
                        problems.Add(withErrPrefix("minimum answers count cannot be more than maximum answers count"));
                    }
                }

                if (q.Answers.Count < GeneralTestCreationConsts.MinAnswersForQuestionCount) {
                    problems.Add(withErrPrefix(
                        "General test question cannot have " +
                       $"less than {GeneralTestCreationConsts.MinAnswersForQuestionCount} answers")
                    );
                } else if (q.Answers.Count > GeneralTestCreationConsts.MaxAnswersForQuestionCount) {
                    problems.Add(withErrPrefix(
                        "General test question cannot have " +
                       $"more than {GeneralTestCreationConsts.MaxAnswersForQuestionCount} answers")
                    );
                }
                problems.AddRange(
                  CheckQuestionAnswersForProblems(q.Answers)
                  .Select(e => withErrPrefix(e))
              );
            }
            return problems.Select(TestPublishingProblem.ForQuestionsCategory).ToList();
        }
        private static IEnumerable<string> CheckQuestionAnswersForProblems(
            ICollection<DraftGeneralTestAnswer> answers
        ) {
            foreach (var answer in answers) {
                string? answerProblem = answer.TypeSpecificInfo switch {
                    TextOnlyAnswerTypeSpecificInfo textOnlyInfo => CheckTextOnlyAnswerForProblems(textOnlyInfo),
                    ImageOnlyAnswerTypeSpecificInfo imageOnlyInfo => CheckImageOnlyAnswerForProblems(imageOnlyInfo),
                    TextAndImageAnswerTypeSpecificInfo textAndImageInfo => CheckTextAndImageAnswerForProblems(textAndImageInfo),
                    _ => throw new ArgumentException("Unknown answer type")
                };
                if (!string.IsNullOrEmpty(answerProblem)) {
                    yield return answerProblem;
                }
            }
        }

        private static string? CheckTextOnlyAnswerForProblems(TextOnlyAnswerTypeSpecificInfo info) {
            int textLen = string.IsNullOrWhiteSpace(info.Text) ? 0 : info.Text.Length;
            if (textLen < GeneralTestCreationConsts.AnswerTextMinLength
                || textLen > GeneralTestCreationConsts.AnswerTextMaxLength
            ) {
                return $"Text of the answer is {textLen} characters long. " +
                    $"The length must be from {GeneralTestCreationConsts.AnswerTextMinLength} " +
                    $"to {GeneralTestCreationConsts.AnswerTextMaxLength} characters";
            }
            return null;
        }
        private static string? CheckImageOnlyAnswerForProblems(ImageOnlyAnswerTypeSpecificInfo info) =>
        string.IsNullOrWhiteSpace(info.ImagePath) ? "Answer must contain image" : null;
        private static string? CheckTextAndImageAnswerForProblems(TextAndImageAnswerTypeSpecificInfo info) {
            int textLen = string.IsNullOrWhiteSpace(info.Text) ? 0 : info.Text.Length;
            if (textLen < GeneralTestCreationConsts.AnswerTextMinLength
                || textLen > GeneralTestCreationConsts.AnswerTextMaxLength
            ) {
                return $"Text of the answer is {textLen} characters long. " +
                    $"The length must be from {GeneralTestCreationConsts.AnswerTextMinLength} " +
                    $"to {GeneralTestCreationConsts.AnswerTextMaxLength} characters";
            }
            if (string.IsNullOrWhiteSpace(info.ImagePath)) {
                return "Answer must contain image";
            }
            return null;
        }
        public static async Task<IResult> PublishDraftTest(
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext,
            VokimiStorageService storageService,
            string testId
        ) {
            DraftTestId draftTestId;
            if (!Guid.TryParse(testId, out Guid testGuid)) {
                return ResultsHelper.BadRequest.WithErr("Unable to publish test. Please refresh the page and try again");
            }
            draftTestId = new(testGuid);
            using (var db = await dbFactory.CreateDbContextAsync()) {
                BaseDraftTest? testToPublish = await db.DraftTestsSharedInfo.FindAsync(draftTestId);
                if (testToPublish is null) {
                    return ResultsHelper.BadRequest.WithErr("Unknown draft test");
                }
                if (!httpContext.IsAuthenticatedUserIsTestCreator(testToPublish)) {
                    return ResultsHelper.BadRequest.WithErr("Only test creator can publish it");
                }
                var testProblems = await GetTestPublilshingProblems(db, draftTestId, testToPublish.Template);
                if (testProblems.Count > 0) {
                    return ResultsHelper.BadRequest.WithErr("Test has problems that need to be fixed before publishing");
                }
                return testToPublish.Template switch {
                    TestTemplate.General => await PublishGeneralTest(dbFactory, storageService, draftTestId),
                    _ => throw new ArgumentException("Not implemented template")
                };
            }


        }

        public static async Task<IResult> PublishGeneralTest(
            IDbContextFactory<AppDbContext> dbFactory,
            VokimiStorageService storageService,
            DraftTestId testId
        ) {
            string? folderWithImgsForPublishedTest = null;
            using (var db = await dbFactory.CreateDbContextAsync()) {
                using (var transaction = await db.Database.BeginTransactionAsync()) {
                    try {
                        DraftGeneralTest? draftTest = await db.DraftGeneralTests
                            .Include(t => t.MainInfo)
                            .Include(t => t.Conclusion)
                            .Include(t => t.Questions)
                                .ThenInclude(q => q.Answers)
                                    .ThenInclude(a => a.TypeSpecificInfo)
                            .Include(t => t.Questions)
                                .ThenInclude(q => q.Answers)
                                    .ThenInclude(a => a.RelatedResults)
                            .Include(t => t.PossibleResults)
                            .FirstOrDefaultAsync(t => t.Id == testId);
                        if (draftTest is null) {
                            return ResultsHelper.BadRequest.WithErr("Cannot find this draft general test");
                        }

                        TestId newTestId = new();
                        string newTestCover = ImgOperationsConsts.DefaultTestCoverImg;
                        if (draftTest.MainInfo.CoverImagePath != ImgOperationsConsts.DefaultTestCoverImg) {
                            var extension = Path.GetExtension(draftTest.MainInfo.CoverImagePath);
                            newTestCover = ImgOperationsHelper.GetPublishedTestCoverImgKey(newTestId, extension);
                        }

                        var publishingData = GeneralTestPublishingData.Create(newTestId, draftTest, newTestCover);
                        folderWithImgsForPublishedTest = ImgOperationsHelper.PublishedTestImgsParentFolder(publishingData.TestId);

                        if (publishingData.CoverRelocationNeeded) {
                            Err copyingErr =
                                await storageService.CopyImageFile(draftTest.MainInfo.CoverImagePath, publishingData.NewTestCoverPath);
                            if (copyingErr.NotNone()) {
                                throw new Exception();
                            }
                        }
                        await PublishGeneralTestResults(
                            db, storageService,
                            draftTest.PossibleResults,
                            publishingData
                        );
                        await PublishGeneralTestQuestions(
                            db, storageService,
                            draftTest.Questions,
                            publishingData
                        );

                        var testToPublish = TestGeneralTemplate.CreateNew(publishingData);

                        await db.TestsGeneralTemplate.AddAsync(testToPublish);

                        await db.SaveChangesAsync();

                        foreach (string tag in publishingData.Tags) {
                            TestTag? tagToAssign = await db.TestTags.FirstOrDefaultAsync(t => t.Value == tag);
                            if (tagToAssign is null) {
                                tagToAssign = TestTag.CreateNew(tag);
                                await db.TestTags.AddAsync(tagToAssign);
                            }
                            tagToAssign.Tests.Add(testToPublish);
                        }
                        await ClearDraftGeneralTestData(
                            draftTest,
                            storageService,
                            db
                        );
                        await db.SaveChangesAsync();
                        await transaction.CommitAsync();
                        return Results.Ok(new {
                            TestId = publishingData.TestId.Value.ToString(),
                            TestName = publishingData.TestName,
                        });

                    } catch {
                        await transaction.RollbackAsync();
                        if (!string.IsNullOrEmpty(folderWithImgsForPublishedTest)) {
                            await storageService.ClearFolderWithSubfolders(folderWithImgsForPublishedTest);
                        }
                        return ResultsHelper.BadRequest.WithErr("Something went wrong during publishing. Please try again later");

                    }
                }
            }
        }
        public async static Task PublishGeneralTestResults(
            AppDbContext db,
            VokimiStorageService storageService,
            ICollection<DraftGeneralTestResult> results,
            GeneralTestPublishingData publishingData
        ) {
            foreach (var draftResult in results) {

                string? resultImg = null;
                GeneralTestResultId newResultId = new();
                if (!string.IsNullOrEmpty(draftResult.ImagePath)) {
                    string extension = Path.GetExtension(draftResult.ImagePath);
                    resultImg = ImgOperationsHelper.GetPublishedGeneralTestResultImgKey(
                       publishingData.TestId,
                       newResultId,
                       extension
                    );
                    Err copyingErr = await storageService.CopyImageFile(draftResult.ImagePath, resultImg);
                    if (copyingErr.NotNone()) {
                        throw new Exception();
                    } 
                }

                var resultToPublish = GeneralTestResult.CreateNew(
                    newResultId,
                    publishingData.TestId,
                    draftResult.Name,
                    draftResult.Text ?? draftResult.Name, //name if somehow text is null after all validations
                    resultImg
                );
                await db.GeneralTestResults.AddAsync(resultToPublish);
                publishingData.PublishedResults.Add(draftResult.Id, resultToPublish);
            }
        }
        public async static Task PublishGeneralTestQuestions(
            AppDbContext db,
            VokimiStorageService storageService,
            ICollection<DraftGeneralTestQuestion> questions,
            GeneralTestPublishingData publishingData
        ) {
            ushort questionOrder = 0;

            foreach (var draftQuestion in questions.OrderBy(i => i.OrderInTest)) {
                string? questionImg = null;
                GeneralTestQuestionId newQuestionId = new();
                if (!string.IsNullOrEmpty(draftQuestion.ImagePath)) {
                    string extension = Path.GetExtension(draftQuestion.ImagePath);
                    questionImg = ImgOperationsHelper.GetPublishedGeneralTestQuestionImgKey(
                       publishingData.TestId,
                       newQuestionId,
                       extension
                    );
                    Err copyingErr = await storageService.CopyImageFile(draftQuestion.ImagePath, questionImg);
                    if (copyingErr.NotNone()) {
                        throw new Exception();
                    }
                }

                GeneralTestQuestion questionToPublish = GeneralTestQuestion.CreateNew(
                    newQuestionId,
                    publishingData.TestId,
                    draftQuestion.Text,
                    questionImg,
                    draftQuestion.AnswersType,
                    questionOrder,
                    draftQuestion.MinAnswersCount,
                    draftQuestion.MaxAnswersCount
                );
                await db.GeneralTestQuestions.AddAsync(questionToPublish);

                questionOrder++;

                foreach (var draftAnswer in draftQuestion.Answers) {

                    if (draftAnswer.TypeSpecificInfo is IAnswerTypeSpecificInfoWithImage imgInfo) {
                        string extension = Path.GetExtension(imgInfo.ImagePath);
                        string newAnswerImg = ImgOperationsHelper.GetPublishedGeneralTestAnswerImgKey(
                           publishingData.TestId,
                           newQuestionId,
                           extension
                        );
                        Err copyingErr = await storageService.CopyImageFile(imgInfo.ImagePath, newAnswerImg);
                        if (copyingErr.NotNone()) {
                            throw new Exception();
                        } else {
                            imgInfo.ImagePath = newAnswerImg;
                        }
                    }
                    ushort order = draftQuestion.ShuffleAnswers ? (ushort)0 : draftAnswer.OrderInQuestion;
                    var relatedDraftResultIds = draftAnswer.RelatedResults.Select(r => r.Id);
                    var relatedPublishedResults = publishingData.PublishedResults
                        .Where(p => relatedDraftResultIds.Contains(p.Key))
                        .Select(p => p.Value)
                        .ToList();
                    var answerToPublish = GeneralTestAnswer.CreateNew(
                        questionToPublish.Id,
                        order,
                        draftAnswer.TypeSpecificInfoId,
                        relatedPublishedResults
                    );

                    await db.GeneralTestAnswers.AddAsync(answerToPublish);
                }

                publishingData.PublishedQuestions.Add(questionToPublish);
            }
        }
        public async static Task ClearDraftGeneralTestData(
            DraftGeneralTest test,
            VokimiStorageService storageService,
            AppDbContext db
        ) {
            db.DraftGeneralTestResults.RemoveRange(test.PossibleResults);
            db.DraftGeneralTestAnswers.RemoveRange(test.Questions.SelectMany(q => q.Answers));
            db.DraftGeneralTestQuestions.RemoveRange(test.Questions);
            db.DraftTestMainInfo.Remove(test.MainInfo);
            db.DraftGeneralTests.Remove(test);
            var draftTestImgsFolder = ImgOperationsHelper.DraftTestImgsParentFolder(test.Id);
            await storageService.ClearFolderWithSubfolders(draftTestImgsFolder);
        }
    }
}
