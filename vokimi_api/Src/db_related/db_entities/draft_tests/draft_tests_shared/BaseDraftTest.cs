using vokimi_api.Src.db_related.db_entities.draft_published_tests_shared;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.enums;


namespace vokimi_api.Src.db_related.db_entities.draft_tests.draft_tests_shared
{
    public abstract class BaseDraftTest
    {
        public DraftTestId Id { get; init; }
        public AppUserId CreatorId { get; init; }
        public DraftTestMainInfoId MainInfoId { get; init; }
        public virtual DraftTestMainInfo MainInfo { get; protected set; }
        public DateTime CreationDate { get; init; }
        public TestConclusionId? ConclusionId { get; protected set; }
        public virtual TestConclusion? Conclusion { get; protected set; }
        public TestTemplate Template { get; init; }
        public TestStylesSheetId StylesSheetId { get; protected set; }
        public virtual TestStylesSheet StylesSheet { get; protected set; }
    }
}
