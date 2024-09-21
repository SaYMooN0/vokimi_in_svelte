using vokimi_api.Src.db_related.db_entities.draft_published_tests_shared;

namespace vokimi_api.Src.dtos.shared.test_creation_shared
{
    public record class DraftTestConclusionData(
        string Id,
        string Text,
        string? AdditionalImage,
        bool AnyFeedback,
        string FeedbackText,
        uint maxFeedbackLength
    )
    {
        public static DraftTestConclusionData FromConclusion(TestConclusion? conclusion) =>
            conclusion is null ?
            new(string.Empty, string.Empty, null, false, "Conclusion Feedback Text", 64) :
            new(
                conclusion.Id.Value.ToString(),
                conclusion.Text,
                conclusion.AdditionalImage,
                conclusion.AnyFeedback,
                conclusion.FeedbackText,
                conclusion.MaxFeedbackLength
            );
    }
}
