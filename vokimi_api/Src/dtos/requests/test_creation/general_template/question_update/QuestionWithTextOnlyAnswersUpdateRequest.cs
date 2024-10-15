using vokimi_api.Src.dtos.shared.general_test_creation.draft_general_test_answers;

namespace vokimi_api.Src.dtos.requests.test_creation.general_template.question_update
{
    public record class QuestionWithTextOnlyAnswersUpdateRequest(
        string Id,
        string Text,
        string? ImagePath,
        bool ShuffleAnswers,
        string AnswersType,
        bool IsMultiple,
        ushort MinAnswersCount,
        ushort MaxAnswersCount,
        ushort OrderInQuestion,
        DraftGeneralTestTextOnlyAnswerFormData[] Answers
    ) : BaseGeneralTestQuestionUpdateRequest
        (
            Id,
            Text,
            ImagePath,
            ShuffleAnswers,
            AnswersType,
            IsMultiple,
            MinAnswersCount,
            MaxAnswersCount,
            OrderInQuestion
        )
    {
        public Err CheckForErr() =>
           CheckForErr(Answers);
    }
}
