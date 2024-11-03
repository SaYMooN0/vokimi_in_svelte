using vokimi_api.Src.db_related.db_entities;
using vokimi_api.Src.db_related.db_entities.draft_published_tests_shared;
using vokimi_api.Src.db_related.db_entities.tests_related;
using vokimi_api.Src.db_related.db_entities.users;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.enums;

namespace VokimiShared.src.models.db_classes.test.test_types
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
        public PrivacyValues Privacy { get; init; }
        public DateOnly CreationDate { get; init; }
        public DateOnly PublicationDate { get; init; }

        public TestConclusionId? ConclusionId { get; init; }
        public virtual TestConclusion? Conclusion { get; protected set; }

        public TestStylesSheetId StylesSheetId { get; init; }
        public virtual TestStylesSheet StylesSheet { get; protected set; }

        public virtual ICollection<TestTag> Tags { get; protected set; } = [];
        public abstract ICollection<BaseTestTakenRecord> GetBaseTestTakings();

    }
}
