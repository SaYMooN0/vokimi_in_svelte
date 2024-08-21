using vokimi_api.Src.db_related.db_entities.draft_tests.draft_tests_shared;
using vokimi_api.Src.enums;
using VokimiShared.src.models.db_classes.test.test_types;

namespace vokimi_api.Src.dtos.responses
{
    public record class UsersDraftTestsVm(
        string ImagePath, string Name, string OverviewLink, TestTemplate Template
        )
    {
        public static UsersDraftTestsVm FromDraftTest(BaseDraftTest test) =>
            new(test.MainInfo.CoverImagePath, test.MainInfo.Name, "", test.Template);
        public static UsersDraftTestsVm FromPublishedTest(BaseTest test) =>
            new(test.Cover, test.Name, "", test.Template);
    }
}
