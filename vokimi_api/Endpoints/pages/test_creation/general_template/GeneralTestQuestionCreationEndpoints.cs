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
using Microsoft.AspNetCore.Http;
using static System.Net.Mime.MediaTypeNames;
using vokimi_api.Src.extension_classes;
using vokimi_api.Src.constants_store_classes;
using System.Net.Http;

namespace vokimi_api.Endpoints.pages.test_creation.general_template
{
    public static class GeneralTestQuestionCreationEndpoints
    {
        public static async Task<IResult> GetGeneralDraftTestQuestionsData(
            IDbContextFactory<AppDbContext> dbFactory,
            string testId,
            HttpContext httpContext
        ) {
            if (string.IsNullOrEmpty(testId)) { return Results.BadRequest(); }

            DraftTestId draftTestId;
            if (!Guid.TryParse(testId, out _)) {
                return Results.BadRequest();
            }
            draftTestId = new(new(testId));
            try {
                using (var db = await dbFactory.CreateDbContextAsync()) {
                    DraftGeneralTest? test = await db.DraftGeneralTests
                            .Include(t => t.Questions)
                            .ThenInclude(q => q.Answers)
                            .FirstOrDefaultAsync(t => t.Id == draftTestId);
                    if (test is null) {
                        return ResultsHelper.BadRequestUnknownTest();
                    }
                    if (!httpContext.IsAuthenticatedUserIsTestCreator(test)) {
                        return ResultsHelper.BadRequestNotCreator();
                    }
                    var response = test.Questions.Select(DraftGeneralTestQuestionBriefInfo.FromDraftTestQuestion);
                    return Results.Ok(response);
                }
            } catch {
                return Results.BadRequest();
            }
        }
        public static async Task<IResult> CreateGeneralTestQuestion(
            IDbContextFactory<AppDbContext> dbFactory,
            [FromBody] GeneralTestQuestionCreationRequest request,
            HttpContext httpContext
        ) {
            TypedGeneralTestQuestionCreationRequest? data = request.ToObjWithTypes();
            if (data is null) {
                return Results.BadRequest();
            }
            try {
                using (var db = await dbFactory.CreateDbContextAsync()) {

                    DraftGeneralTest? test = await db.DraftGeneralTests.FindAsync(data.TestId);
                    if (test is null) {
                        return ResultsHelper.BadRequestUnknownTest();
                    }
                    if (!httpContext.IsAuthenticatedUserIsTestCreator(test)) {
                        return ResultsHelper.BadRequestNotCreator();
                    }

                    int existingQuestionsCount = db.DraftGeneralTestQuestions
                        .Where(q => q.TestId == data.TestId)
                        .Count();
                    if (existingQuestionsCount >= GeneralTestCreationConsts.MaxQuestionsForTestCount) {
                        return ResultsHelper.BadRequestWithErr(
                            $"General test cannot have more than {GeneralTestCreationConsts.MaxQuestionsForTestCount} questions"
                        );
                    }
                    ushort orderInTest = (ushort)existingQuestionsCount;
                    DraftGeneralTestQuestion question = DraftGeneralTestQuestion.CreateNew(
                        data.TestId,
                        data.AnswersType,
                        orderInTest
                    );
                    await db.DraftGeneralTestQuestions.AddAsync(question);
                    await db.SaveChangesAsync();
                    return Results.Ok();
                }
            } catch {
                return Results.StatusCode(500);
            }
        }
        public static async Task<IResult> GetDraftGeneralTestQuestionDataToEdit(
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext,
            string questionId
        ) {
            if (string.IsNullOrEmpty(questionId)) { return Results.BadRequest(); }

            DraftGeneralTestQuestionId draftTestQuestionId;
            if (!Guid.TryParse(questionId, out Guid questionGuid)) {
                return Results.BadRequest();
            }
            draftTestQuestionId = new(questionGuid);

            try {
                using (var db = await dbFactory.CreateDbContextAsync()) {
                    DraftGeneralTestQuestion? question = await db.DraftGeneralTestQuestions
                        .Include(q => q.Answers)
                            .ThenInclude(a => a.RelatedResults)
                        .Include(q => q.Answers)
                            .ThenInclude(a => a.TypeSpecificInfo)
                        .FirstOrDefaultAsync(q => q.Id == draftTestQuestionId);
                    if (question is null) {
                        return Results.BadRequest("Question not found");
                    }
                    DraftGeneralTest? test = db.DraftGeneralTests.Find(question.TestId);
                    if (test is null) {
                        return ResultsHelper.BadRequestUnknownTest();
                    }
                    if (!httpContext.IsAuthenticatedUserIsTestCreator(test)) {
                        return ResultsHelper.BadRequestNotCreator();
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
          HttpContext httpContext,
          IDbContextFactory<AppDbContext> dbFactory,
          VokimiStorageService storageService
        ) {
            DraftGeneralTestQuestionId questionToDeleteId;
            if (!Guid.TryParse(questionId, out _)) {
                return ResultsHelper.BadRequestWithErr("An error has occurred. Please refresh the page and try again");
            }
            questionToDeleteId = new(new(questionId));
            using (var db = await dbFactory.CreateDbContextAsync()) {
                using (var transaction = await db.Database.BeginTransactionAsync()) {
                    try {
                        DraftGeneralTestQuestion? questionToDelete = await db.DraftGeneralTestQuestions.FindAsync(questionToDeleteId);
                        if (questionToDelete is null) {
                            return ResultsHelper.BadRequestWithErr("Unknown question");
                        }
                        DraftGeneralTest? test = await db.DraftGeneralTests
                            .Include(t => t.Questions)
                            .ThenInclude(q => q.Answers)
                            .ThenInclude(a => a.TypeSpecificInfo)
                            .FirstOrDefaultAsync(t => t.Id == questionToDelete.TestId);
                        if (test is null) {
                            return ResultsHelper.BadRequestUnknownTest();
                        }
                        if (!httpContext.IsAuthenticatedUserIsTestCreator(test)) {
                            return ResultsHelper.BadRequestNotCreator();
                        }
                        questionToDelete = test.Questions.FirstOrDefault(q => q.Id == questionToDelete.Id);
                        if (questionToDelete is null) {
                            return ResultsHelper.BadRequestWithErr("Unknown question");
                        }
                        foreach (var answer in questionToDelete.Answers) {
                            db.AnswerTypeSpecificInfo.Remove(answer.TypeSpecificInfo);
                            db.DraftGeneralTestAnswers.Remove(answer);
                        }
                        db.DraftGeneralTestQuestions.Remove(questionToDelete);
                        test.Questions.Remove(questionToDelete);

                        await db.SaveChangesAsync();
                        DraftGeneralTestQuestion[] remainingQuestions = test.Questions
                            .OrderBy(q => q.OrderInTest)
                            .ToArray();
                        ushort currentOrder = 0;
                        foreach (var remainingQuestion in remainingQuestions) {
                            remainingQuestion.UpdateOrderInTest(currentOrder);
                            currentOrder++;
                        }
                        await storageService.ClearUnusedQuestionImages(
                            questionToDeleteId,
                            questionToDelete.TestId,
                            null, []
                        );
                        await transaction.CommitAsync();
                        await db.SaveChangesAsync();
                        return Results.Ok();
                    } catch {
                        transaction.Rollback();
                        return ResultsHelper.BadRequestServerError();
                    }
                }

            }

        }
        public static async Task<IResult> MoveQuestionUpInOrder(
            string questionId,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {
            DraftGeneralTestQuestionId questionToMoveId;
            if (!Guid.TryParse(questionId, out _)) {
                return ResultsHelper.BadRequestServerError();
            }
            questionToMoveId = new(new(questionId));
            using (var db = await dbFactory.CreateDbContextAsync()) {
                using (var transaction = await db.Database.BeginTransactionAsync()) {
                    try {
                        DraftGeneralTestQuestion? questionToMoveUp = await db.DraftGeneralTestQuestions
                            .FindAsync(questionToMoveId);
                        if (questionToMoveUp is null || questionToMoveUp.OrderInTest == 0) {
                            return ResultsHelper.BadRequestServerError();
                        }
                        DraftGeneralTest? test = db.DraftGeneralTests
                            .Include(t => t.Questions)
                            .FirstOrDefault(t => t.Id == questionToMoveUp.TestId);
                        if (test is null) {
                            return ResultsHelper.BadRequestUnknownTest();
                        }
                        if (!httpContext.IsAuthenticatedUserIsTestCreator(test)) {
                            return ResultsHelper.BadRequestNotCreator();
                        }
                        ushort questionToMoveUpCurrentOrder = questionToMoveUp.OrderInTest;
                        DraftGeneralTestQuestion? questionToMoveDown = test.Questions
                            .FirstOrDefault(q => q.OrderInTest == questionToMoveUpCurrentOrder - 1);
                        if (questionToMoveDown is not null) {
                            questionToMoveDown.UpdateOrderInTest(questionToMoveUpCurrentOrder);
                            db.DraftGeneralTestQuestions.Update(questionToMoveDown);
                        }
                        questionToMoveUp.UpdateOrderInTest((ushort)(questionToMoveUpCurrentOrder - 1));
                        db.DraftGeneralTestQuestions.Update(questionToMoveUp);

                        await db.SaveChangesAsync();
                        await transaction.CommitAsync();
                        return Results.Ok();

                    } catch {
                        await transaction.RollbackAsync();
                        return ResultsHelper.BadRequestServerError();
                    }
                }
            }
        }
        public static async Task<IResult> MoveQuestionDownInOrder(
            string questionId,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {
            DraftGeneralTestQuestionId questionToMoveId;
            if (!Guid.TryParse(questionId, out _)) {
                return ResultsHelper.BadRequestServerError();
            }
            questionToMoveId = new(new(questionId));
            using (var db = await dbFactory.CreateDbContextAsync()) {
                using (var transaction = await db.Database.BeginTransactionAsync()) {
                    try {
                        DraftGeneralTestQuestion? questionToMoveDown = await db.DraftGeneralTestQuestions
                            .FindAsync(questionToMoveId);
                        if (questionToMoveDown is null) {
                            return ResultsHelper.BadRequestServerError();
                        }

                        DraftGeneralTest? test = await db.DraftGeneralTests
                            .Include(t => t.Questions)
                            .FirstOrDefaultAsync(t => t.Id == questionToMoveDown.TestId);
                        if (test is null) {
                            return ResultsHelper.BadRequestUnknownTest();
                        }
                        if (!httpContext.IsAuthenticatedUserIsTestCreator(test)) {
                            return ResultsHelper.BadRequestNotCreator();
                        }

                        ushort questionToMoveDownCurrentOrder = questionToMoveDown.OrderInTest;
                        DraftGeneralTestQuestion? questionToMoveUp = test.Questions
                            .FirstOrDefault(q => q.OrderInTest == questionToMoveDownCurrentOrder + 1);
                        if (questionToMoveUp is null) {
                            return ResultsHelper.BadRequestServerError();
                        }
                        questionToMoveUp.UpdateOrderInTest(questionToMoveDownCurrentOrder);
                        questionToMoveDown.UpdateOrderInTest((ushort)(questionToMoveDownCurrentOrder + 1));

                        db.DraftGeneralTestQuestions.Update(questionToMoveUp);
                        db.DraftGeneralTestQuestions.Update(questionToMoveDown);

                        await db.SaveChangesAsync();
                        await transaction.CommitAsync();
                        return Results.Ok();

                    } catch {
                        await transaction.RollbackAsync();
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
            Err validationErr = requestData.CheckForErr();
            if (validationErr.NotNone()) {
                return ResultsHelper.BadRequestWithErr(validationErr);
            }
            if (!Guid.TryParse(requestData.Id, out var questionToUpdateGuid)) {
                return ResultsHelper.BadRequestServerError();
            }
            using (var db = await dbFactory.CreateDbContextAsync()) {

                DraftGeneralTestQuestionId questionId = new(questionToUpdateGuid);
                DraftGeneralTestQuestion? q = await db.DraftGeneralTestQuestions.FindAsync(questionId);
                if (q is null) {
                    return ResultsHelper.BadRequestWithErr("Question not found");
                }
                DraftGeneralTest? test = db.DraftGeneralTests.Find(q.TestId);
                if (test is null) {
                    return ResultsHelper.BadRequestUnknownTest();
                }
                if (!httpContext.IsAuthenticatedUserIsTestCreator(test)) {
                    return ResultsHelper.BadRequestNotCreator();
                }

                return await ProcessDraftGeneralTestQuestionUpdate(
                    questionId,
                    requestData,
                    db,
                    storageService
                );
            }
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
            AppDbContext db,
            VokimiStorageService storageService
        ) {
            DraftGeneralTestQuestion? question = await db.DraftGeneralTestQuestions
                .Include(q => q.Answers)
                    .ThenInclude(a => a.TypeSpecificInfo)
                .FirstOrDefaultAsync(q => q.Id == questionToUpdateId);
            if (question is null) {
                return ResultsHelper.BadRequestWithErr("Unknown question");
            }

            using var transaction = await db.Database.BeginTransactionAsync();
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

                var usedAnswerImgs = await CreateAnswersForQuestionUpdate(
                    updateData.GetAnswers,
                    db, questionToUpdateId,
                    question.TestId
                );

                await storageService.ClearUnusedQuestionImages(
                    questionToUpdateId,
                    question.TestId,
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
        private static async Task<List<string>> CreateAnswersForQuestionUpdate(
            IEnumerable<BaseDraftGeneralTestAnswerFormData> answers,
            AppDbContext db,
            DraftGeneralTestQuestionId questionId,
            DraftTestId testId
        ) {
            List<string> imagesForAnswers = [];
            ushort currentAnswerOrder = 0;
            foreach (var answer in answers.OrderBy(
                a => a.OrderInQuestion == -1 ? int.MaxValue : a.OrderInQuestion)
            //for answers with unset order to go to the end 
            ) {
                BaseGeneralTestAnswerTypeSpecificInfo specificInfo = CreateAnswerTypeSpecificInfo(answer);
                if (specificInfo is IAnswerTypeSpecificInfoWithImage specificInfoWithImage) {
                    imagesForAnswers.Add(specificInfoWithImage.ImagePath);
                }

                await db.AnswerTypeSpecificInfo.AddAsync(specificInfo);
                var dbAnswer = DraftGeneralTestAnswer.CreateNew(
                    questionId,
                    currentAnswerOrder,
                    specificInfo.Id
                );
                currentAnswerOrder++;

                foreach (var relatedResult in answer.RelatedResultsIdName) {
                    DraftGeneralTestResult? resToMarkAsRelated = await db.DraftGeneralTestResults.FindAsync(relatedResult.Key);
                    if (resToMarkAsRelated is null) {
                        resToMarkAsRelated = DraftGeneralTestResult.CreateNew(testId, relatedResult.Value);
                        await db.DraftGeneralTestResults.AddAsync(resToMarkAsRelated);
                    }
                    dbAnswer.RelatedResults.Add(resToMarkAsRelated);
                }
                await db.DraftGeneralTestAnswers.AddAsync(dbAnswer);
            }
            return imagesForAnswers;
        }
        private static BaseGeneralTestAnswerTypeSpecificInfo CreateAnswerTypeSpecificInfo(BaseDraftGeneralTestAnswerFormData answer) =>
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
