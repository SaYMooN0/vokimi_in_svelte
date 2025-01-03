using vokimi_api.Src.db_related.db_entities.published_tests.published_tests_shared;
using vokimi_api.Src.enums;

namespace vokimi_api.Src.dtos.responses.manage_test_page.overall
{
    public record class ManageTestOverallTabDataResponse(
        string TestName,
        string TestCover,
        string? TestDescription,
        string TestPrivacy,
        string TestLanguage,
        string TestAccentColor
    )
    {
        public static ManageTestOverallTabDataResponse FromTest(BaseTest test) => new(
            test.Name,
            test.Cover,
            test.Description,
            test.Settings.Privacy.GetId(),
            test.Language.GetId(),
            test.StylesSheet.AccentColor
        );
    }
}
