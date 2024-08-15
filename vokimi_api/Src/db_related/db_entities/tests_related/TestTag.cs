using vokimi_api.Src.db_related.db_entities_ids;
using VokimiShared.src.models.db_classes.test.test_types;

namespace vokimi_api.Src.db_related.db_entities.tests_related
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
