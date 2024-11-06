using System.ComponentModel.DataAnnotations.Schema;
using vokimi_api.Src.db_related.db_entities.draft_published_tests_shared;
using vokimi_api.Src.db_related.db_entities.draft_tests.draft_tests_shared;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.enums;

namespace vokimi_api.Src.db_related.db_entities.draft_tests.draft_general_test
{
    public class DraftGeneralTest : BaseDraftTest
    {
        public DraftGeneralTest() : base(TestTemplate.General) { }

        public virtual ICollection<DraftGeneralTestQuestion> Questions { get; private set; } = [];
        public virtual ICollection<DraftGeneralTestResult> PossibleResults { get; set; } = [];

        public static DraftGeneralTest CreateNew(
            AppUserId creatorId,
            DraftTestMainInfoId mainInfoId,
            TestStylesSheetId stylesSheetId
        ) => new() {
            Id = new(),
            CreatorId = creatorId,
            MainInfoId = mainInfoId,
            CreationDate = DateOnly.FromDateTime(DateTime.UtcNow),
            Settings = TestSettings.Default(),
            ConclusionId = null,
            StylesSheetId = stylesSheetId
        };
    }
}
