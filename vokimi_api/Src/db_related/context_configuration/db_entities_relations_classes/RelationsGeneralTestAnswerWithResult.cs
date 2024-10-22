using vokimi_api.Src.db_related.db_entities.published_tests.general_test_related;
using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.db_related.context_configuration.db_entities_relations_classes
{
    public class RelationsGeneralTestAnswerWithResult
    {
        public GeneralTestAnswerId AnswerId { get; init; }
        public GeneralTestResultId ResultId { get; init; }
        public virtual GeneralTestAnswer Answer { get; init; }
        public virtual GeneralTestResult Result { get; init; }
    }
}
