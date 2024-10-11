using vokimi_api.Src.enums;

namespace vokimi_api.Src.dtos.responses.test_creation_responses.shared
{
    public record class DraftTestOverviewInfoResponse(
        bool IsViewerCreator,
        string Template,
        string TestName
    )
    {
        public static DraftTestOverviewInfoResponse NewWithViewerIsCreator(TestTemplate template, string testName) => new(
            true,
            template.GetId(),
            testName
        );
        public static DraftTestOverviewInfoResponse NewWithViewerIsNotCreator() => new(
            false, string.Empty, string.Empty
        );
    }
}
