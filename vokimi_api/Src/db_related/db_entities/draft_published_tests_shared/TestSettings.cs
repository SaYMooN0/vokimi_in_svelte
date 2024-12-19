using vokimi_api.Src.enums;

namespace vokimi_api.Src.db_related.db_entities.draft_published_tests_shared
{
    public record class TestSettings(
        PrivacyValues Privacy,
        bool DiscussionsOpen,
        bool TestTakenPostsAllowed,
        bool EnableTestRatings,
        bool TagsSuggestionsAllowed
    )
    {
        public static TestSettings Default() => new(PrivacyValues.Anyone, true, true, true,true);
    }
}
