using vokimi_api.Src.db_related.db_entities.test_collections;
using vokimi_api.Src.db_related.db_entities_ids;
using VokimiShared.src.models.db_classes.test.test_types;

namespace vokimi_api.Src.db_related.context_configuration.db_entities_relations_classes
{
    public class RelationsTestCollectionWithTest
    {
        public TestCollectionId TestCollectionId { get; init; }
        public TestCollection TestCollection { get; init; }
        public TestId TestId { get; init; }
        public BaseTest Test { get; init; }
    }
}
