using vokimi_api.Src.db_related.db_entities.published_tests.published_tests_shared;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.enums;

namespace vokimi_api.Src.db_related.db_entities.test_collections
{
    public class TestCollection
    {
        public TestCollectionId Id { get; init; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public AppUserId OwnerId { get; init; }
        public PrivacyValues Privacy { get; private set; }
        public string Color { get; set; }
        public string IconName { get; set; }
        public ICollection<BaseTest> Tests { get; private set; } = [];
        public static TestCollection CreateNew(
            string name,
            AppUserId ownerId,
            string color,
            string iconName
        ) => new() {
            Id = new(),
            Name = name,
            OwnerId = ownerId,
            Privacy = PrivacyValues.ForMyself,
            Color = color,
            IconName = iconName
        };
        public void UpdateName(string name) =>
            Name = name;
        public void UpdateDescription(string description) =>
            Description = description;
        public void UpdateStyles(string newColor, string newIcon) {
            Color = newColor;
            IconName = newIcon;
        }
    }
}
