using vokimi_api.Src.db_related.db_entities.published_tests.published_tests_shared;
using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.db_related.db_entities.tests_related.tags
{
    public class TestTag
    {
        public TestTagId Id { get; init; }
        public string Value { get; init; }
        public override string ToString() => Value;
        public virtual ICollection<BaseTest> Tests { get; set; } = [];
        public static TestTag CreateNew(string value) => new() {
            Id = new(),
            Value = value
        };
    }
}
