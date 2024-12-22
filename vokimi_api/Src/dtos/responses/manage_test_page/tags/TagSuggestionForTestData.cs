
using vokimi_api.Src.db_related.db_entities.tests_related.tags;

namespace vokimi_api.Src.dtos.responses.manage_test_page.tags
{
    public record class TagSuggestionForTestData(
        string Id,
        string Value,
        int SuggestionsCount,
        DateOnly FirstSuggestionDate
    )
    {
        public static TagSuggestionForTestData FromTagSuggestions(TagSuggestionForTest suggestion) => new(
            suggestion.Id.Value.ToString(),
            suggestion.Value,
            suggestion.SuggestionsCount,
            suggestion.FirstSuggestionDate
        );
    }
}
