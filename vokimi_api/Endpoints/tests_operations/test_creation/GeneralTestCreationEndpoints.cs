using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            using (var db = dbFactory.CreateDbContext()) {
                IEnumerable<DraftGeneralTestQuestion> questions = db.DraftGeneralTestQuestions
                        .Include(q => q.Answers)
                        .Where(q => q.TestId == draftTestId);

                return Results.Ok(questions.Select(DraftGeneralTestQuestionBriefInfo.FromDraftTestQuestion));
            }
        }
        public static IResult CreateGeneralTestQuestion(IDbContextFactory<AppDbContext> dbFactory,
                                                        [FromBody] CreateGeneralTestQuestionRequest request) {
            ParsedCreateGeneralTestQuestionRequest? data = request.ToObjWithTypes();
            if (data is null) {
                return Results.BadRequest();
            }

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
        }
    }
}