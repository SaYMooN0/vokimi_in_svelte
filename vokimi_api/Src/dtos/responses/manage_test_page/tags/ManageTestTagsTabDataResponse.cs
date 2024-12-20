using vokimi_api.Src.db_related.db_entities.published_tests.published_tests_shared;
using vokimi_api.Src.db_related.db_entities.tests_related.tags;

namespace vokimi_api.Src.dtos.responses.manage_test_page.tags
{
    public record class ManageTestTagsTabDataResponse(
        string[] testTags,
        TagSuggestionForTestData[] tagsSuggestions,
        bool tagsSuggestionsAllowed
    )
    {
        public static ManageTestTagsTabDataResponse FromTest(BaseTest test) => new(
            test.Tags.Select(t => t.Value).ToArray(),
            test.SuggestedTags.Select(TagSuggestionForTestData.FromTagSuggestions).ToArray(),
            test.Settings.TagsSuggestionsAllowed
        );
    }
}
