using vokimi_api.Src.db_related.db_entities.draft_published_tests_shared;
using vokimi_api.Src.db_related.db_entities.test_collections;
using vokimi_api.Src.db_related.db_entities.test_taken_records;
using vokimi_api.Src.db_related.db_entities.tests_related;
using vokimi_api.Src.db_related.db_entities.tests_related.discussions;
using vokimi_api.Src.db_related.db_entities.tests_related.tags;
using vokimi_api.Src.db_related.db_entities.users;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.enums;

namespace vokimi_api.Src.db_related.db_entities.published_tests.published_tests_shared
{
    public abstract class BaseTest
    {
        private readonly TestTemplate _template;
        public TestTemplate Template => _template;
        protected BaseTest(TestTemplate template) {
            _template = template;
        }

        public TestId Id { get; init; }
        public AppUserId CreatorId { get; init; }
        public virtual AppUser Creator { get; protected set; }
        public string Name { get; init; }
        public string Cover { get; init; }
        public string? Description { get; init; }
        public Language Language { get; init; }
        public TestSettings Settings { get; protected set; }
        public DateOnly CreationDate { get; init; }
        public DateOnly PublicationDate { get; init; }

        public TestConclusionId? ConclusionId { get; init; }
        public virtual TestConclusion? Conclusion { get; protected set; }

        public TestStylesSheetId StylesSheetId { get; init; }
        public virtual TestStylesSheet StylesSheet { get; protected set; }

        public virtual ICollection<TestTag> Tags { get; protected set; } = [];
        public virtual ICollection<TagSuggestionForTest> SuggestedTags { get; protected set; } = [];

        public virtual ICollection<TestRating> Ratings { get; protected set; } = [];
        public virtual ICollection<TestDiscussionsComment> DiscussionsComments { get; protected set; } = [];
        public virtual ICollection<TestCollection> CollectionTestIn { get; protected set; } = [];

        public abstract ICollection<BaseTestTakenRecord> GetBaseTestTakings();
        public void UpdateSettings(TestSettings settings) => Settings = settings;

    }
}
