using vokimi_api.Src.db_related.db_entities.draft_tests.draft_tests_shared;
using vokimi_api.Src.enums;

namespace vokimi_api.Src.dtos.responses.test_creation_responses.shared
{
    public record class DraftTestMainInfoDataResponse(
        TestTemplate Template,
        string Name,
        string? Description,
        Language Language,
        TestPrivacy Privacy,
        string ImgPath
    )
    {
        public static DraftTestMainInfoDataResponse FromDraftTest(BaseDraftTest test) {
            if (test is null || test.MainInfo is null) {
                throw new ArgumentNullException(nameof(test));
            }
            return new DraftTestMainInfoDataResponse(
                test.Template,
                test.MainInfo.Name,
                test.MainInfo.Description,
                test.MainInfo.Language,
                test.MainInfo.Privacy,
                test.MainInfo.CoverImagePath
            );
        }
    }
}
