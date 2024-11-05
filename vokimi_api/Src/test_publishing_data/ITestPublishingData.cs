using vokimi_api.Src.db_related.db_entities.draft_published_tests_shared;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.enums;

namespace vokimi_api.Src.test_publishing_data
{
    public interface ITestPublishingData
    {
        public TestId TestId { get; init; }
        public AppUserId CreatorId { get; init; }
        public string TestName { get; init; }
        public string NewTestCoverPath { get; init; }
        public string? Description { get; init; }
        public Language Language { get; init; }
        public TestSettings Settings { get; init; }
        public DateOnly CreationDate { get; init; }
        public TestConclusionId? ConclusionId { get; init; }
        public TestStylesSheetId StylesSheetId { get; init; }
        public string[] Tags { get; init; }
        public List<string> ImgsToDeleteInCaseOfSuccess { get; init; }
    }
}
