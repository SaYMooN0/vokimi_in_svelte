using vokimi_api.Src.db_related.db_entities.draft_published_tests_shared.general_test_answers;
using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.db_related.db_entities.published_tests.general_test_related
{
    public class GeneralTestAnswer
    {
        public GeneralTestAnswerId Id { get; init; }
        public GeneralTestQuestionId QuestionId { get; init; }
        public ushort OrderInQuestion { get; init; }
        public GeneralTestAnswerTypeSpecificInfoId TypeSpecificInfoId { get; init; }
        public virtual GeneralTestAnswerTypeSpecificInfo TypeSpecificInfo { get; init; }
        public virtual ICollection<GeneralTestResult> RelatedResults { get; protected set; } = [];

        public static GeneralTestAnswer CreateNew(GeneralTestQuestionId questionId,
                                                  ushort orderInQuestion,
                                                  GeneralTestAnswerTypeSpecificInfoId typeSpecificInfoId,
                                                  ICollection<GeneralTestResult> relatedResults) =>
            new() {
                Id = new(),
                QuestionId = questionId,
                OrderInQuestion = orderInQuestion,
                TypeSpecificInfoId = typeSpecificInfoId,
                RelatedResults = relatedResults
            };
    }
}
