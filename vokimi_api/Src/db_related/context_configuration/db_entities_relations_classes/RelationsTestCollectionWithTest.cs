using vokimi_api.Src.db_related.db_entities.published_tests.published_tests_shared;
using vokimi_api.Src.db_related.db_entities.test_collections;
using vokimi_api.Src.db_related.db_entities_ids;

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
