using vokimi_api.Src.db_related.db_entities.draft_tests.draft_general_test;
using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.db_related.context_configuration.db_entities_relations_classes {
    public class RelationsDraftGeneralTestAnswerWithResult
    {
        public DraftGeneralTestAnswerId AnswerId { get; init; }
        public DraftGeneralTestResultId ResultId { get; init; }
        public virtual DraftGeneralTestAnswer Answer { get; init; }
        public virtual DraftGeneralTestResult Result { get; init; }
    }
}
