using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.db_related.db_entities.draft_published_tests_shared
{
    public class TestConclusion
    {
        public TestConclusionId Id { get; init; }

        public string Text { get; private set; }
        public string? AdditionalImage { get; private set; }
        public bool AnyFeedback { get; private set; }
        public string? FeedbackAccompanyingText { get; private set; }
        public uint MaxFeedbackLength { get; private set; }
        public static TestConclusion CreateNew() => new() {
            Id = new(),
            Text = "Text of the conclusion for the test",
            AdditionalImage = null,
            AnyFeedback = false,
            FeedbackAccompanyingText = null,
            MaxFeedbackLength = 64
        };
        public void Update(string text, string? additionalImage, bool anyFeedback, string feedbackText, uint maxFeedbackLength) {
            Text = text;
            AdditionalImage = additionalImage;
            AnyFeedback = anyFeedback;
            FeedbackAccompanyingText = feedbackText;
            MaxFeedbackLength = maxFeedbackLength;
        }

    }
}
