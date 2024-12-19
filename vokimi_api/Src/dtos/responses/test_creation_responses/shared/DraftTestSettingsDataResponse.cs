using vokimi_api.Src.db_related.db_entities.draft_published_tests_shared;
using vokimi_api.Src.enums;

namespace vokimi_api.Src.dtos.responses.test_creation_responses.shared
{
    public record class DraftTestSettingsDataResponse(
        string Privacy,
        bool DiscussionsOpen,
        bool TestTakenPostsAllowed,
        bool EnableTestRatings,
        bool TagsSuggestionsAllowed
    )
    {
        public static DraftTestSettingsDataResponse FromTestSettings(TestSettings settings) => new(
            settings.Privacy.GetId(),
            settings.DiscussionsOpen,
            settings.TestTakenPostsAllowed,
            settings.EnableTestRatings,
            settings.TagsSuggestionsAllowed
        );
    }
}
