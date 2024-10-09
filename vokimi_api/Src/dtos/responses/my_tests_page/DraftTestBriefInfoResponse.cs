using vokimi_api.Src.db_related.db_entities.draft_tests.draft_tests_shared;
using vokimi_api.Src.enums;

namespace vokimi_api.Src.dtos.responses.my_tests_page
{
    public record class DraftTestBriefInfoResponse(
        string Id,
        string Name,
        string Cover,
        string Template,
        string Privacy
    )
    {
        public static DraftTestBriefInfoResponse FromDraftTest(BaseDraftTest test) => new(
            test.Id.Value.ToString(),
            test.MainInfo.Name,
            test.MainInfo.CoverImagePath,
            test.Template.GetId(),
            test.MainInfo.Privacy.GetId()
        );
    }
}
