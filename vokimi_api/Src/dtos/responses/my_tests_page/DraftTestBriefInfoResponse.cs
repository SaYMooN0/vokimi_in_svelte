using vokimi_api.Src.db_related.db_entities.draft_tests.draft_tests_shared;
using vokimi_api.Src.enums;

namespace vokimi_api.Src.dtos.responses.my_tests_page
{
    public record class DraftTestBriefInfoResponse(
        string Id,
        string Name,
        string? Description,
        string Cover,
        string Template
    )
    {
        public static DraftTestBriefInfoResponse FromDraftTest(BaseDraftTest test) => new(
            test.Id.Value.ToString(),
            test.MainInfo.Name,
            test.MainInfo.Description,
            test.MainInfo.CoverImagePath,
            test.Template.GetId()
        );
    }
}
