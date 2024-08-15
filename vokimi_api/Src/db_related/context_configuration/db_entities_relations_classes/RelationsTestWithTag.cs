using vokimi_api.Src.db_related.db_entities.tests_related;
using vokimi_api.Src.db_related.db_entities_ids;
using VokimiShared.src.models.db_classes.test.test_types;

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
