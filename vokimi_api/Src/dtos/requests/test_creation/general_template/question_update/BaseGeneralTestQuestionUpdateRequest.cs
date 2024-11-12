using vokimi_api.Src.constants_store_classes;
using vokimi_api.Src.dtos.shared.general_test_creation.draft_general_test_answers;
namespace vokimi_api.Src.dtos.requests.test_creation.general_template.question_update
{
    public abstract record class BaseGeneralTestQuestionUpdateRequest(
        string Id,
        string Text,
        string? ImagePath,
        bool ShuffleAnswers,
        string AnswersType,
        bool IsMultiple,
        ushort MinAnswersCount,
        ushort MaxAnswersCount,
        ushort OrderInTest
    )
    {
        public abstract BaseDraftGeneralTestAnswerFormData[] GetAnswers
        {
            get;
        }
        public Err CheckForErr() {
            BaseDraftGeneralTestAnswerFormData[] answers = GetAnswers;
            int textLen = string.IsNullOrWhiteSpace(Text) ? 0 : Text.Length;
            if (textLen > GeneralTestCreationConsts.QuestionTextMaxLength ||
                textLen < GeneralTestCreationConsts.QuestionTextMinLength) {
                return new Err($"Text of the question must be between {GeneralTestCreationConsts.QuestionTextMinLength} and " +
                               $"{GeneralTestCreationConsts.QuestionTextMinLength} characters");
            }
            if (IsMultiple) {
                if (MaxAnswersCount < MinAnswersCount) {
                    return new Err("Minimum answers count cannot be more than maximum answers count");
                }
                if (answers.Length < MinAnswersCount) {
                    return new Err("Minimum answers count cannot be less than total number of answers");
                }
                if (MaxAnswersCount > answers.Length) {
                    return new Err("Maximum answers count cannot be more than total number of answers");
                }
            }
            for (int i = 0; i < answers.Length; i++) {
                Err answerErr = answers[i].CheckForErr(i + 1);
                if (answerErr.NotNone()) {
                    return answerErr;
                }
            }
            return Err.None;
        }
    }
}
