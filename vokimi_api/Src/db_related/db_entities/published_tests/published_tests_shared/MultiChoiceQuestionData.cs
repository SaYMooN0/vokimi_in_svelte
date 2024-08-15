using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.db_related.db_entities.published_tests.published_tests_shared
{
    public class MultiChoiceQuestionData
    {
        public MultiChoiceQuestionDataId Id { get; init; }
        public ushort MinAnswersCount { get; init; }
        public ushort MaxAnswersCount { get; init; }
        public static MultiChoiceQuestionData CreateNew(ushort minAnswersCount, ushort maxAnswersCount) =>
            new()
            {
                Id = new(),
                MinAnswersCount = minAnswersCount,
                MaxAnswersCount = maxAnswersCount
            };
    }
}
