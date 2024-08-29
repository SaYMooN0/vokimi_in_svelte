using vokimi_api.Src.db_related.db_entities.draft_tests.draft_tests_shared;
using vokimi_api.Src.enums;

namespace vokimi_api.Src.dtos.responses.test_creation_responses.shared
{
    public record class DraftTestMainInfoDataResponse(
        string Template,
        string Name,
        string? Description,
        string Language,
        string Privacy,
        string ImgPath
    )
    {
        public static DraftTestMainInfoDataResponse FromDraftTest(BaseDraftTest test) {
            if (test is null || test.MainInfo is null) {
                throw new ArgumentNullException(nameof(test));
            }
            return new DraftTestMainInfoDataResponse(
                test.Template.GetId(),
                test.MainInfo.Name,
                test.MainInfo.Description,
                test.MainInfo.Language.GetId(),
                test.MainInfo.Privacy.GetId(),
                test.MainInfo.CoverImagePath
            );
        }
    }
}
