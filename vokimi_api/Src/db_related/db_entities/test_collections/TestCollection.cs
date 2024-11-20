using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.enums;
using VokimiShared.src.models.db_classes.test.test_types;

namespace vokimi_api.Src.db_related.db_entities.test_collections
{
    public class TestCollection
    {
        public TestCollectionId Id { get; init; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public AppUserId OwnerId { get; init; }
        public PrivacyValues Privacy { get; private set; }
        public TestCollectionStyles Styles { get; init; }
        public ICollection<BaseTest> Tests { get; private set; } = [];
        public static TestCollection CreateNew(string name, AppUserId ownerId) => new() {
            Id = new(),
            Name = name,
            OwnerId = ownerId,
            Privacy = PrivacyValues.ForMyself,
            Styles = TestCollectionStyles.Default,
        };
        public void UpdateName(string name) =>
            Name = name;
        public void UpdateDescription(string description) =>
            Description = description;
    }
}
