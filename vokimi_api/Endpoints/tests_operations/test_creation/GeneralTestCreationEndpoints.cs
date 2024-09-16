using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vokimi_api.Helpers;
using vokimi_api.Src;
using vokimi_api.Src.db_related;
using vokimi_api.Src.db_related.db_entities.draft_tests.draft_general_test;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.dtos.requests.test_creation.general_template;
using vokimi_api.Src.dtos.responses.test_creation_responses.general;

namespace vokimi_api.Endpoints.tests_operations.test_creation
{
    public static class GeneralTestCreationEndpoints
    {
        public static IResult GetGeneralDraftTestQuestionsData(IDbContextFactory<AppDbContext> dbFactory,
                                                               string testId) {
            if (string.IsNullOrEmpty(testId)) { return Results.BadRequest(); }

            DraftTestId draftTestId;
            if (!Guid.TryParse(testId, out _)) {
                return Results.BadRequest();
            }
            draftTestId = new(new(testId));
            try {

                using (var db = dbFactory.CreateDbContext()) {
                    var questions = db.DraftGeneralTestQuestions
                            .Include(q => q.Answers)
                            .Where(q => q.TestId == draftTestId)
                            .ToArray();

                    return Results.Ok(questions.Select(DraftGeneralTestQuestionBriefInfo.FromDraftTestQuestion));
                }
            } catch {
                return Results.BadRequest();
            }
        }
        public static IResult CreateGeneralTestQuestion(IDbContextFactory<AppDbContext> dbFactory,
                                                        [FromBody] GeneralTestQuestionCreationRequest request) {
            TypedGeneralTestQuestionCreationRequest? data = request.ToObjWithTypes();
            if (data is null) {
                return Results.BadRequest();
            }
            try {
                using (var db = dbFactory.CreateDbContext()) {


                    int existingQuestionsCount = db.DraftGeneralTestQuestions
                        .Where(q => q.TestId == data.TestId)
                        .Count();
                    ushort orderInTest = (ushort)existingQuestionsCount;
                    DraftGeneralTestQuestion question = DraftGeneralTestQuestion.CreateNew(
                        data.TestId,
                        data.AnswersType,
                        orderInTest);
                    db.DraftGeneralTestQuestions.Add(question);
                    db.SaveChanges();
                    return Results.Ok();
                }
            } catch {
                return Results.StatusCode(500);
            }
        }
        public static IResult GetDraftGeneralTestQuestionDataToEdit(IDbContextFactory<AppDbContext> dbFactory,
                                                                    string questionId) {
            if (string.IsNullOrEmpty(questionId)) { return Results.BadRequest(); }

            DraftGeneralTestQuestionId draftTestQuestionId;
            if (!Guid.TryParse(questionId, out _)) {
                return Results.BadRequest();
            }
            draftTestQuestionId = new(new(questionId));

            try {
                using (var db = dbFactory.CreateDbContext()) {
                    DraftGeneralTestQuestion? question = db.DraftGeneralTestQuestions
                        .Include(q => q.Answers)
                        .ThenInclude(a => a.RelatedResults)
                        .FirstOrDefault(q => q.Id == draftTestQuestionId);
                    if (question is null) {
                        return Results.BadRequest("Question not found");
                    }
                    return Results.Ok(DraftGeneralTestQuestionDataToEdit.FromDraftTestQuestion(question));
                }
            } catch {
                return Results.BadRequest();
            }

        }
        public static IResult UpdateDraftGeneralTestQuestionData(IDbContextFactory<AppDbContext> dbFactory) {
            return Results.BadRequest();
        }
        public static IResult GetResultsIdNameDictionary(string testId, IDbContextFactory<AppDbContext> dbFactory) {
            DraftTestId draftTestId;
            if (!Guid.TryParse(testId, out _)) {
                return ResultsHelper.BadRequestUnknownTest();
            }
            draftTestId = new(new(testId));
            using (var db = dbFactory.CreateDbContext()) {
                var results = db.DraftGeneralTestResults
                    .Where(r => r.TestId == draftTestId)
                    .ToDictionary(r => r.Id.Value.ToString(), r => r.Name);
                return Results.Ok(results);
            }
        }
        public static IResult CreateNewResultForTest(
            [FromBody] GeneralTestResultCreationRequest request,
            IDbContextFactory<AppDbContext> dbFactory) {

            Err formValidatingErr = request.GetError();
            if (formValidatingErr.NotNone()) {
                return ResultsHelper.BadRequestWithErr(formValidatingErr.Message);
            }
            DraftTestId testId = new(new(request.TestId));
            try {
                using (var db = dbFactory.CreateDbContext()) {
                    DraftGeneralTestResult result = DraftGeneralTestResult.CreateNew(testId, request.ResultName);
                    db.DraftGeneralTestResults.Add(result);
                    db.SaveChanges();
                    return Results.Ok();
                }
            } catch {
                return ResultsHelper.BadRequestServerError();
            }
        }
        public static IResult GetResultsDataToEdit(string testId, IDbContextFactory<AppDbContext> dbFactory) {
            DraftTestId draftTestId;
            if (!Guid.TryParse(testId, out _)) {
                return ResultsHelper.BadRequestUnknownTest();
            }
            draftTestId = new(new(testId));
            using (var db = dbFactory.CreateDbContext()) {
                var results = db.DraftGeneralTestResults
                    .Where(r => r.TestId == draftTestId)
                    .Select(DraftGeneralTestResultDataToEdit.FromResult)
                    .ToArray();
                return Results.Ok(results);
            }
        }
        public static IResult DeleteGeneralDraftTestQuestion(string questionId,
                                                            IDbContextFactory<AppDbContext> dbFactory) {
            DraftGeneralTestQuestionId questionToDeleteId;
            if (!Guid.TryParse(questionId, out _)) {
                return ResultsHelper.BadRequestWithErr("An error has occurred. Please refresh the page and try again");
            }
            questionToDeleteId = new(new(questionId));
            using (var db = dbFactory.CreateDbContext()) {
                using (var transaction = db.Database.BeginTransaction()) {
                    try {
                        DraftGeneralTestQuestion? question = db.DraftGeneralTestQuestions
                                        .Include(q => q.Answers)
                                        .ThenInclude(a => a.AdditionalInfo)
                                        .FirstOrDefault(q => q.Id == questionToDeleteId);
                        if (question is null) {
                            return ResultsHelper.BadRequestWithErr("Unknown questions");
                        }
                        foreach (var answer in question.Answers) {
                            db.AnswerTypeSpecificInfo.Remove(answer.AdditionalInfo);
                            db.DraftGeneralTestAnswers.Remove(answer);
                        }
                        db.DraftGeneralTestQuestions.Remove(question);
                        db.SaveChanges();
                        DraftGeneralTestQuestion[] remainingQuestions = db.DraftGeneralTestQuestions
                            .Where(q => q.TestId == question.TestId)
                            .OrderBy(q => q.OrderInTest)
                            .ToArray();
                        ushort currentOrder = 0;
                        foreach (var remainingQuestion in remainingQuestions) {
                            remainingQuestion.UpdateOrderInTest(currentOrder);
                            currentOrder++;
                        }
                        transaction.Commit();
                        db.SaveChanges();
                        return Results.Ok();
                    } catch {
                        transaction.Rollback();
                        return ResultsHelper.BadRequestServerError();
                    }
                }

            }

        }
        public static IResult DeleteGeneralDraftTestResult(string resultId,
                                                           IDbContextFactory<AppDbContext> dbFactory) {
            DraftGeneralTestResultId resultToDeleteId;
            if (!Guid.TryParse(resultId, out _)) {
                return ResultsHelper.BadRequestWithErr("An error has occurred. Please refresh the page and try again");
            }
            resultToDeleteId = new(new(resultId));
            using (var db = dbFactory.CreateDbContext()) {
                try {
                    DraftGeneralTestResult? res = db.DraftGeneralTestResults
                                    .FirstOrDefault(q => q.Id == resultToDeleteId);
                    if (res is null) {
                        return ResultsHelper.BadRequestWithErr("Unknown result");
                    }
                    db.DraftGeneralTestResults.Remove(res);
                    db.SaveChanges();
                    return Results.Ok();
                } catch {
                    return ResultsHelper.BadRequestServerError();
                }

            }

        }
    }
}