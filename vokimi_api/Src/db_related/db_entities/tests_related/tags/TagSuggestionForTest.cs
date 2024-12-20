using vokimi_api.Src.db_related.db_entities.users;
using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.db_related.db_entities.tests_related.tags
{
    public class TagSuggestionForTest
    {
        public TagSuggestionForTestId Id { get; init; }
        public TestId TestId { get; init; }
        public string Value { get; init; }
        public int SuggestionsCount { get; private set; }
        public DateOnly FirstSuggestionDate { get; init; }
        public void IncreaseSuggestionsCount() => SuggestionsCount += 1;
        public static TagSuggestionForTest CreateNew(string value, TestId testId) => new() {
            Id = new(),
            TestId = testId,
            Value = value,
            SuggestionsCount = 1,
            FirstSuggestionDate = DateOnly.FromDateTime(DateTime.Now)
        };
    }
}
