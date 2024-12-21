using vokimi_api.Src.constants_store_classes;
using vokimi_api.Src.db_related.db_entities.published_tests.published_tests_shared;
using vokimi_api.Src.db_related.db_entities.tests_related.tags;

namespace vokimi_api.Src.dtos.responses.manage_test_page.tags
{
    public record class ManageTestTagsTabDataResponse(
        string[] TestTags,
        TagSuggestionForTestData[] TagsSuggestions,
        bool TagsSuggestionsAllowed,
        int MaxTagsForTestCount
    )
    {
        public static ManageTestTagsTabDataResponse FromTest(BaseTest test) => new(
            test.Tags.Select(t => t.Value).ToArray(),
            test.SuggestedTags.Select(TagSuggestionForTestData.FromTagSuggestions).ToArray(),
            test.Settings.TagsSuggestionsAllowed,
            TestTagsConsts.MaxTagsForTestCount
        );
    }
}
