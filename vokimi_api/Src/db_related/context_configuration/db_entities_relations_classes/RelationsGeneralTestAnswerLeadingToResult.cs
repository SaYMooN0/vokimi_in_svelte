using vokimi_api.Src.db_related.db_entities.published_tests.general_test_related;
using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.db_related.context_configuration.db_entities_relations_classes {
    public class RelationsGeneralTestAnswerLeadingToResult
    {
        public GeneralTestAnswerId GenericTestAnswerId { get; init; }
        public GeneralTestResultId GenericTestResultId { get; init; }
        public virtual GeneralTestAnswer GenericTestAnswer { get; init; }
        public virtual GeneralTestResult GenericTestResult { get; init; }
    }
}
