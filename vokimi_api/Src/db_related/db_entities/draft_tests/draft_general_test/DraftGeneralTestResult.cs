using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.db_related.db_entities.draft_tests.draft_general_test
{
    public class DraftGeneralTestResult
    {
        public DraftGeneralTestResultId Id { get; init; }
        public DraftTestId TestId { get; init; }
        public string Name { get; private set; }
        public string? Text { get; private set; }
        public string? ImagePath { get; private set; }
        public virtual ICollection<DraftGeneralTestAnswer> AnswersLeadingToResult { get; set; } = [];


        public static DraftGeneralTestResult CreateEmpty(string name, DraftTestId testId)
            => CreateNew(testId, name, string.Empty, string.Empty);
        public static DraftGeneralTestResult CreateNew(DraftTestId testId, string name, string? text, string? imagePath) =>
            new() {
                Id = new(),
                TestId = testId,
                Name = name,
                Text = text,
                ImagePath = imagePath
            };
        public void Update(string name, string text, string? imagePath) {
            Text = text;
            ImagePath = imagePath;
        }
    }
}
