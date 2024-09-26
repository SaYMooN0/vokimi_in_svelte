using System.ComponentModel.DataAnnotations.Schema;
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
        [Column("TagsString")]
        public string tagsString { get; set; } = "";
        [NotMapped]
        public IEnumerable<string> Tags
        {
            get { return string.IsNullOrWhiteSpace(tagsString) ? [] : tagsString.Split('|'); }
            protected set { tagsString = string.Join("|", value); }
        }
        public void SetTags(IEnumerable<string> tags) {
            Tags = tags;
        }
        public void SetConclusion(TestConclusion conclusion) {
            this.ConclusionId = conclusion.Id;
            this.Conclusion = conclusion;
        }

    }
}
