using vokimi_api.Src.db_related.db_entities.draft_tests.draft_general_test;
using vokimi_api.Src.enums;

namespace vokimi_api.Src.dtos.responses.test_creation_responses.general
{
    public record class DraftGeneralTestQuestionDataToEdit(
        string Id,
        string Text,
        string? imagePath,
        bool shuffleAnswers,
        string answersType,
        ushort minAnswersCount,
        ushort maxAnswersCount,
        List<IDraftGeneralTestAnswerFormData> answers
    )
    {
        public static DraftGeneralTestQuestionDataToEdit FromDraftTestQuestion(DraftGeneralTestQuestion question) =>
            new(
                question.Id.Value.ToString(),
                question.Text,
                question.ImagePath,
                question.ShuffleAnswers,
                question.AnswersType.GetId(),
                question.MinAnswersCount,
                question.MaxAnswersCount,
                ExtractAnswers(question)
            );
        private static List<IDraftGeneralTestAnswerFormData> ExtractAnswers(DraftGeneralTestQuestion question) {
            return [];
        }
    }
    public interface IDraftGeneralTestAnswerFormData
    {
        Dictionary<string, string> RelatedResults { get; init; }
    };
}
