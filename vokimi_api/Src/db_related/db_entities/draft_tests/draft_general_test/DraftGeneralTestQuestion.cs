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
        public ushort MinAnswersCount { get; private set; }
        public ushort MaxAnswersCount { get; private set; }
        public virtual ICollection<DraftGeneralTestAnswer> Answers { get; private set; } = [];
        public DraftTestId TestId { get; init; }
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
                MinAnswersCount = 1,
                MaxAnswersCount = 1,
                TestId = testId,
                Answers = new List<DraftGeneralTestAnswer>()
            };
        public bool IsSingleChoice => MinAnswersCount == 1 && MaxAnswersCount == 1;
    }
}

