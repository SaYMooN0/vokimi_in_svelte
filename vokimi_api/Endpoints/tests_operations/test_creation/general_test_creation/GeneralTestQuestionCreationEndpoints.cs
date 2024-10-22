using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vokimi_api.Src.db_related.db_entities.draft_tests.draft_general_test;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.db_related;
using vokimi_api.Src.dtos.requests.test_creation.general_template;
using vokimi_api.Src.dtos.responses.test_creation_responses.general;
using vokimi_api.Helpers;
using System.Text.Json;
using vokimi_api.Services;
using vokimi_api.Src.db_related.db_entities.draft_published_tests_shared.general_test_answers;
using vokimi_api.Src.dtos.requests.test_creation.general_template.question_update;
using vokimi_api.Src.dtos.shared.general_test_creation.draft_general_test_answers;
using vokimi_api.Src.enums;
using vokimi_api.Src;

namespace vokimi_api.Endpoints.tests_operations.test_creation.general_test_creation
{
    public static class GeneralTestQuestionCreationEndpoints
    {
        public static IResult GetGeneralDraftTestQuestionsData(
            IDbContextFactory<AppDbContext> dbFactory,
            string testId
        ) {
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
        public static IResult CreateGeneralTestQuestion(
            IDbContextFactory<AppDbContext> dbFactory,
            [FromBody] GeneralTestQuestionCreationRequest request
        ) {
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
        public static IResult GetDraftGeneralTestQuestionDataToEdit(
            IDbContextFactory<AppDbContext> dbFactory,
            string questionId
        ) {
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
                        .Include(q => q.Answers)
                            .ThenInclude(a => a.TypeSpecificInfo)
                        .FirstOrDefault(q => q.Id == draftTestQuestionId);
                    if (question is null) {
                        return Results.BadRequest("Question not found");
                    }

                    string jsonOutput = Newtonsoft.Json.JsonConvert.SerializeObject(
                        DraftGeneralTestQuestionDataResponse.FromDraftTestQuestion(question),
                        new Newtonsoft.Json.JsonSerializerSettings() {
                            Formatting = Newtonsoft.Json.Formatting.Indented,
                            ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                        }
                    );
                    return Results.Ok(jsonOutput);
                }
            } catch {
                return Results.BadRequest();
            }

        }
        public static async Task<IResult> DeleteDraftGeneralTestQuestion(
          string questionId,
          IDbContextFactory<AppDbContext> dbFactory,
          VokimiStorageService storageService
      ) {
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
                                        .ThenInclude(a => a.TypeSpecificInfo)
                                        .FirstOrDefault(q => q.Id == questionToDeleteId);
                        if (question is null) {
                            return ResultsHelper.BadRequestWithErr("Unknown questions");
                        }
                        foreach (var answer in question.Answers) {
                            db.AnswerTypeSpecificInfo.Remove(answer.TypeSpecificInfo);
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
                        await ClearUnusedQuestionImages(
                            questionToDeleteId,
                            question.TestId,
                            storageService,
                            null, []
                        );
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
        public static IResult MoveQuestionUpInOrder(
            string questionId,
            IDbContextFactory<AppDbContext> dbFactory
        ) {
            DraftGeneralTestQuestionId questionToMoveId;
            if (!Guid.TryParse(questionId, out _)) {
                return ResultsHelper.BadRequestServerError();
            }
            questionToMoveId = new(new(questionId));
            using (var db = dbFactory.CreateDbContext()) {
                using (var transaction = db.Database.BeginTransaction()) {
                    try {
                        DraftGeneralTestQuestion? questionToMoveUp = db.DraftGeneralTestQuestions
                            .FirstOrDefault(q => q.Id == questionToMoveId);
                        if (questionToMoveUp is null) {
                            return ResultsHelper.BadRequestServerError();
                        }
                        if (questionToMoveUp.OrderInTest == 0) {
                            return ResultsHelper.BadRequestServerError();
                        }
                        var testQuestions = db.DraftGeneralTestQuestions.Where(q => q.TestId == questionToMoveUp.TestId);

                        ushort questionToMoveUpCurrentOrder = questionToMoveUp.OrderInTest;
                        DraftGeneralTestQuestion? questionToMoveDown =
                            testQuestions.FirstOrDefault(q => q.OrderInTest == questionToMoveUpCurrentOrder - 1);
                        if (questionToMoveDown is not null) {
                            questionToMoveDown.UpdateOrderInTest(questionToMoveUpCurrentOrder);
                            db.DraftGeneralTestQuestions.Update(questionToMoveDown);
                        }
                        questionToMoveUp.UpdateOrderInTest((ushort)(questionToMoveUpCurrentOrder - 1));
                        db.DraftGeneralTestQuestions.Update(questionToMoveUp);

                        db.SaveChanges();
                        transaction.Commit();
                        return Results.Ok();

                    } catch {
                        transaction.Rollback();
                        return ResultsHelper.BadRequestServerError();
                    }
                }
            }
        }
        public static IResult MoveQuestionDownInOrder(
            string questionId,
            IDbContextFactory<AppDbContext> dbFactory
        ) {
            DraftGeneralTestQuestionId questionToMoveId;
            if (!Guid.TryParse(questionId, out _)) {
                return ResultsHelper.BadRequestServerError();
            }
            questionToMoveId = new(new(questionId));
            using (var db = dbFactory.CreateDbContext()) {
                using (var transaction = db.Database.BeginTransaction()) {
                    try {
                        DraftGeneralTestQuestion? questionToMoveDown = db.DraftGeneralTestQuestions
                            .FirstOrDefault(q => q.Id == questionToMoveId);
                        if (questionToMoveDown is null) {
                            return ResultsHelper.BadRequestServerError();
                        }

                        var testQuestions = db.DraftGeneralTestQuestions.Where(q => q.TestId == questionToMoveDown.TestId);

                        ushort questionToMoveDownCurrentOrder = questionToMoveDown.OrderInTest;
                        DraftGeneralTestQuestion? questionToMoveUp =
                            testQuestions.FirstOrDefault(q => q.OrderInTest == questionToMoveDownCurrentOrder + 1);
                        if (questionToMoveUp is null) {
                            return ResultsHelper.BadRequestServerError();
                        }
                        questionToMoveUp.UpdateOrderInTest(questionToMoveDownCurrentOrder);
                        questionToMoveDown.UpdateOrderInTest((ushort)(questionToMoveDownCurrentOrder + 1));

                        db.DraftGeneralTestQuestions.Update(questionToMoveUp);
                        db.DraftGeneralTestQuestions.Update(questionToMoveDown);

                        db.SaveChanges();
                        transaction.Commit();
                        return Results.Ok();

                    } catch {
                        transaction.Rollback();
                        return ResultsHelper.BadRequestServerError();
                    }
                }
            }
        }
        public static async Task<IResult> UpdateDraftGeneralTestQuestion(
            HttpContext httpContext,
            IDbContextFactory<AppDbContext> dbFactory,
            VokimiStorageService storageService,
            string answersType
        ) {
            string requestBody = await new StreamReader(httpContext.Request.Body).ReadToEndAsync();

            GeneralTestAnswerType? answerType = GeneralTestAnswerTypeExtensions.FromId(answersType);
            BaseGeneralTestQuestionUpdateRequest? requestData = DeserializeQuestionUpdateRequest(requestBody, answerType);

            if (requestData is null) {

                return ResultsHelper.BadRequestWithErr("Unknown or invalid answers type.");
            }
            if (!Guid.TryParse(requestData.Id, out var questionToUpdateGuid)) {
                return ResultsHelper.BadRequestServerError();
            }
            Err validationErr = requestData.CheckForErr();
            if (validationErr.NotNone()) {
                return ResultsHelper.BadRequestWithErr(validationErr);
            }
            return await ProcessDraftGeneralTestQuestionUpdate(
                new(questionToUpdateGuid),
                requestData,
                dbFactory,
                storageService
            );
        }

        private static BaseGeneralTestQuestionUpdateRequest? DeserializeQuestionUpdateRequest(
            string requestBody,
            GeneralTestAnswerType? answerType
        ) {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return answerType switch {
                GeneralTestAnswerType.TextOnly => JsonSerializer
                    .Deserialize<QuestionWithTextOnlyAnswersUpdateRequest>(requestBody, options),
                GeneralTestAnswerType.ImageOnly => JsonSerializer
                    .Deserialize<QuestionWithImageOnlyAnswersUpdateRequest>(requestBody, options),
                GeneralTestAnswerType.TextAndImage => JsonSerializer
                    .Deserialize<QuestionWithTextAndImageAnswersUpdateRequest>(requestBody, options),
                _ => null
            };
        }

        private static async Task<IResult> ProcessDraftGeneralTestQuestionUpdate(
            DraftGeneralTestQuestionId questionToUpdateId,
            BaseGeneralTestQuestionUpdateRequest updateData,
            IDbContextFactory<AppDbContext> dbFactory,
            VokimiStorageService storageService
        ) {
            using var db = dbFactory.CreateDbContext();
            DraftGeneralTestQuestion? question = await db.DraftGeneralTestQuestions
                .Include(q => q.Answers)
                    .ThenInclude(a => a.TypeSpecificInfo)
                .FirstOrDefaultAsync(q => q.Id == questionToUpdateId);
            if (question is null) {
                return ResultsHelper.BadRequestWithErr("Unknown question");
            }

            using var transaction = db.Database.BeginTransaction();
            try {
                if (updateData.IsMultiple) {
                    question.UpdateAsMultipleChoice(updateData);
                } else {
                    question.UpdateAsSingleChoice(updateData);
                }

                foreach (var a in question.Answers) {
                    db.AnswerTypeSpecificInfo.Remove(a.TypeSpecificInfo);
                    db.DraftGeneralTestAnswers.Remove(a);
                }

                var usedAnswerImgs = CreateAnswersForQuestionUpdate(
                    updateData.GetAnswers,
                    db, questionToUpdateId,
                    question.TestId
                );

                await ClearUnusedQuestionImages(
                    questionToUpdateId,
                    question.TestId,
                    storageService,
                    question.ImagePath,
                    usedAnswerImgs
                );

                await db.SaveChangesAsync();
                await transaction.CommitAsync();

                return Results.Ok();
            } catch {
                await transaction.RollbackAsync();
                return ResultsHelper.BadRequestServerError();
            }
        }

        private static async Task ClearUnusedQuestionImages(
            DraftGeneralTestQuestionId questionId,
            DraftTestId testId,
            VokimiStorageService storageService,
            string? questionImagePath,
            List<string> answerImgs
        ) {
            string questionImgPref = ImgOperationsHelper.DraftGeneralTestQuestionsFolder(testId, questionId);
            await storageService.ClearUnusedObjectsInFolder(questionImgPref, questionImagePath);
            string answerImgPref = ImgOperationsHelper.DraftGeneralTestAnswersFolder(testId, questionId);
            await storageService.ClearUnusedObjectsInFolder(answerImgPref, answerImgs);
        }

        private static List<string> CreateAnswersForQuestionUpdate(
            IEnumerable<BaseDraftGeneralTestAnswerFormData> answers,
            AppDbContext db,
            DraftGeneralTestQuestionId questionId,
            DraftTestId testId
        ) {
            List<string> imagesForAnswers = [];
            ushort currentAnswerOrder = 0;
            foreach (var answer in answers
                .OrderBy(a => a.OrderInQuestion == -1 ? int.MaxValue : a.OrderInQuestion)
            //for answers with unset order to go to the end 
            ) {
                GeneralTestAnswerTypeSpecificInfo specificInfo = CreateAnswerTypeSpecificInfo(answer);
                if (specificInfo is IAnswerTypeSpecificInfoWithImage specificInfoWithImage) {
                    imagesForAnswers.Add(specificInfoWithImage.ImagePath);
                }

                db.AnswerTypeSpecificInfo.Add(specificInfo);
                var dbAnswer = DraftGeneralTestAnswer.CreateNew(
                    questionId,
                    currentAnswerOrder,
                    specificInfo.Id
                );
                currentAnswerOrder++;

                foreach (var relatedResult in answer.RelatedResultsIdName) {
                    DraftGeneralTestResult? resToMarkAsRelated = db.DraftGeneralTestResults.Find(relatedResult.Key);
                    if (resToMarkAsRelated is null) {
                        resToMarkAsRelated = DraftGeneralTestResult.CreateNew(testId, relatedResult.Value);
                        db.DraftGeneralTestResults.Add(resToMarkAsRelated);
                    }
                    dbAnswer.RelatedResults.Add(resToMarkAsRelated);
                }
                db.DraftGeneralTestAnswers.Add(dbAnswer);
            }
            return imagesForAnswers;
        }
        private static GeneralTestAnswerTypeSpecificInfo CreateAnswerTypeSpecificInfo(BaseDraftGeneralTestAnswerFormData answer) =>
            answer switch {
                DraftGeneralTestTextOnlyAnswerFormData textOnly =>
                    TextOnlyAnswerTypeSpecificInfo.CreateNew(textOnly.Text),
                DraftGeneralTestImageOnlyAnswerFormData imageOnly =>
                    ImageOnlyAnswerTypeSpecificInfo.CreateNew(imageOnly.Image),
                DraftGeneralTestTextAndImageAnswerFormData textAndImage =>
                    TextAndImageAnswerTypeSpecificInfo.CreateNew(textAndImage.Text, textAndImage.Image),
                _ => throw new ArgumentException("Unknown type of answer form data")
            };
    }
}
