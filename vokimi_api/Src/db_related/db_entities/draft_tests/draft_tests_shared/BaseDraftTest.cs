using System.ComponentModel.DataAnnotations.Schema;
using vokimi_api.Src.db_related.db_entities.draft_published_tests_shared;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.enums;


namespace vokimi_api.Src.db_related.db_entities.draft_tests.draft_tests_shared
{
    public abstract class BaseDraftTest
    {
        public DraftTestId Id { get; init; }

        private readonly TestTemplate _template;
        public TestTemplate Template => _template;
        protected BaseDraftTest(TestTemplate template) {
            _template = template;
        }

        public AppUserId CreatorId { get; init; }
        public DraftTestMainInfoId MainInfoId { get; init; }
        public virtual DraftTestMainInfo MainInfo { get; protected set; }
        public DateOnly CreationDate { get; init; }
        public TestConclusionId? ConclusionId { get; protected set; }
        public virtual TestConclusion? Conclusion { get; protected set; }
        public TestStylesSheetId StylesSheetId { get; protected set; }
        public virtual TestStylesSheet StylesSheet { get; protected set; }
        [Column("TagsString")]
        public string tagsString { get; set; } = "";
        [NotMapped]
        public string[] Tags
        {
            get { return string.IsNullOrWhiteSpace(tagsString) ? [] : tagsString.Split('|'); }
            protected set { tagsString = string.Join("|", value); }
        }
        public void SetTags(IEnumerable<string> tags) {
            Tags = tags.ToArray();
        }
        public void SetConclusion(TestConclusion? conclusion) {
            if (conclusion is null) {
                this.ConclusionId = null;
                this.Conclusion = null;
            } else {
                this.ConclusionId = conclusion.Id;
                this.Conclusion = conclusion;
            }
        }

    }
}
