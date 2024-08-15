using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.enums;

namespace vokimi_api.Src.db_related.db_entities.draft_tests.draft_general_test
{
    public class DraftGeneralTestQuestion
    {
        public DraftGeneralTestQuestionId Id { get; init; }
        public string Text { get; private set; }
        public string? ImagePath { get; private set; }
        public bool ShuffleAnswers { get; private set; }
        public GeneralTestAnswerType AnswersType { get; init; }
        public ushort OrderInTest { get; private set; }
        public bool IsMultipleChoice { get; private set; }
        public MultipleChoiceAdditionalData? MultipleChoiceData { get; private set; }
        public virtual ICollection<DraftGeneralTestAnswer> Answers { get; private set; } = [];

        public DraftTestId DraftTestId { get; init; }
        public static DraftGeneralTestQuestion CreateNew(string text,
                                                         GeneralTestAnswerType answersType,
                                                         DraftTestId testId,
                                                         ushort orderInTest) =>
            new() {
                Id = new(),
                Text = text,
                ImagePath = null,
                ShuffleAnswers = false,
                AnswersType = answersType,
                OrderInTest = orderInTest,
                IsMultipleChoice = false,
                MultipleChoiceData = null,
                DraftTestId = testId,
                Answers = new List<DraftGeneralTestAnswer>()
            };
    }

    public class MultipleChoiceAdditionalData
    {
        public ushort MinAnswers { get; init; }
        public ushort MaxAnswers { get; init; }
    }

}
