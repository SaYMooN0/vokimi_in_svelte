using vokimi_api.Src.db_related.db_entities.draft_published_tests_shared.general_test_answers;
using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.db_related.db_entities.draft_tests.draft_general_test
{
    public class DraftGeneralTestAnswer
    {
        public DraftGeneralTestAnswerId Id { get; init; }
        public DraftGeneralTestQuestionId QuestionId { get; init; }
        public ushort OrderInQuestion { get; set; }
        public virtual ICollection<DraftGeneralTestResult> RelatedResults { get; private set; } = [];
        public GeneralTestAnswerTypeSpecificInfoId TypeSpecificInfoId { get; init; }
        public virtual BaseGeneralTestAnswerTypeSpecificInfo TypeSpecificInfo { get; private set; }
        public static DraftGeneralTestAnswer CreateNew(
            DraftGeneralTestQuestionId questionId,
            ushort orderInQuestion,
            GeneralTestAnswerTypeSpecificInfoId typeSpecificInfoId
        ) => new() {
            Id = new(),
            QuestionId = questionId,
            OrderInQuestion = orderInQuestion,
            TypeSpecificInfoId = typeSpecificInfoId
        };
    }

}
