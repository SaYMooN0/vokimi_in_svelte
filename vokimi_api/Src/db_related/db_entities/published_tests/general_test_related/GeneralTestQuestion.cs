using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.enums;

namespace vokimi_api.Src.db_related.db_entities.published_tests.general_test_related
{
    public class GeneralTestQuestion
    {
        public GeneralTestQuestionId Id { get; init; }
        public TestId TestId { get; init; }
        public string Text { get; init; }
        public string? ImagePath { get; init; }
        public GeneralTestAnswerType AnswersType { get; init; }
        public ushort OrderInTest { get; init; }
        public virtual ICollection<GeneralTestAnswer> Answers { get; protected set; } = [];

        //if both equals to 1 then question is single choice
        public ushort MinAnswersCount { get; private set; }
        public ushort MaxAnswersCount { get; private set; }

        public bool IsSingleChoice => MinAnswersCount == 1 && MaxAnswersCount == 1;


        public static GeneralTestQuestion CreateNew(TestId testId,
                                                    string text,
                                                    string? imagePath,
                                                    GeneralTestAnswerType answersType,
                                                    ushort orderInTest) =>
            new() {
                Id = new(),
                TestId = testId,
                Text = text,
                ImagePath = imagePath,
                AnswersType = answersType,
                OrderInTest = orderInTest
            };

    }
}
