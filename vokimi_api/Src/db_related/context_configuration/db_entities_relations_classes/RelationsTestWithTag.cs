using vokimi_api.Src.db_related.db_entities.published_tests.published_tests_shared;
using vokimi_api.Src.db_related.db_entities.tests_related.tags;
using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.db_related.context_configuration.db_entities_relations_classes
{
    public class RelationsTestWithTag
    {
        public TestId TestId { get; set; }
        public virtual BaseTest Test { get; set; }

        public TestTagId TagId { get; set; }
        public virtual TestTag Tag { get; set; }
    }
}
