using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.db_related.db_entities.published_tests.general_test_related
{
    public class GeneralTestResult
    {
        public GeneralTestResultId Id { get; init; }
        public TestId TestId { get; init; }
        public string Name { get; init; }
        public string Text { get; init; }
        public string? ImagePath { get; init; }
        public virtual ICollection<GeneralTestAnswer> AnswersLeadingToResult { get; protected set; } = [];

        public static GeneralTestResult CreateNew(
            GeneralTestResultId id,
            TestId testId,
            string name,
            string text,
            string? imagePath
        ) => new() {
            Id = id,
            Name = name,
            TestId = testId,
            Text = text,
            ImagePath = imagePath
        };


    }
}
