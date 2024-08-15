using vokimi_api.Src.db_related.db_entities.published_tests.published_tests_shared;
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

        //if MultiChoiceQuestionData is null question is not multi choice
        public MultiChoiceQuestionDataId? MultiChoiceQuestionDataId { get; init; }
        public virtual MultiChoiceQuestionData? MultiChoiceQuestionData { get; protected set; }


        public virtual ICollection<GeneralTestAnswer> Answers { get; protected set; } = [];

        public static GeneralTestQuestion CreateNew(TestId testId,
                                                    string text,
                                                    string? imagePath,
                                                    GeneralTestAnswerType answersType,
                                                    ushort orderInTest,
                                                    MultiChoiceQuestionDataId? multiChoiceQuestionDataId) =>
            new()
            {
                Id = new(),
                TestId = testId,
                Text = text,
                ImagePath = imagePath,
                AnswersType = answersType,
                OrderInTest = orderInTest,
                MultiChoiceQuestionDataId = multiChoiceQuestionDataId
            };

    }
}
