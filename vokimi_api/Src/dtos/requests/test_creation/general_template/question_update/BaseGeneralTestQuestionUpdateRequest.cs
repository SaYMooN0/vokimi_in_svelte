using vokimi_api.Src.constants_store_classes;
using vokimi_api.Src.dtos.shared.general_test_creation.draft_general_test_answers;

namespace vokimi_api.Src.dtos.requests.test_creation.general_template.question_update
{
    public record class BaseGeneralTestQuestionUpdateRequest(
        string Id,
        string Text,
        string? ImagePath,
        bool ShuffleAnswers,
        string AnswersType,
        bool IsMultiple,
        ushort MinAnswersCount,
        ushort MaxAnswersCount
    )
    {
        protected Err ValidateBase(int answersCount) {
            int textLen = string.IsNullOrWhiteSpace(Text) ? 0 : Text.Length;
            if (textLen > GeneralTestCreationConsts.QuestionTextMaxLength ||
                textLen < GeneralTestCreationConsts.QuestionTextMinLength) {
                return new Err($"Text of the question must be between {GeneralTestCreationConsts.QuestionTextMinLength} and " +
                               $"{GeneralTestCreationConsts.QuestionTextMinLength} characters");
            }
            if (IsMultiple) {
                if (MaxAnswersCount > MinAnswersCount) {
                    return new Err("Maximum answers count cannot be more than minimum answers count");
                }
                if (answersCount < MinAnswersCount) {
                    return new Err("Minimum answers count cannot be less than total number of answers");
                }
                if (MaxAnswersCount > answersCount) {
                    return new Err("Maximum answers count cannot be more than total number of answers");
                }
            }
            return Err.None;
        }
    }
}
