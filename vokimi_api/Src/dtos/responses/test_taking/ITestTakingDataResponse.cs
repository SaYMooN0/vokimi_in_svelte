using vokimi_api.Src.db_related.db_entities.draft_published_tests_shared;

namespace vokimi_api.Src.dtos.responses.test_taking
{
    public interface ITestTakingDataResponse
    {
        public string TestTemplate { get; init; }
        public TestTakingConclusionData? ConclusionData { get; init; }
    }
    public record class TestTakingConclusionData(
        string ConclusionText,
        string? AdditionalImage,
        bool AnyFeedback,
        string? FeedbackAccompanyingText,
        uint MaxFeedbackLength
    )
    {
        public static TestTakingConclusionData FromConclusion(TestConclusion conclusion) => new(
            conclusion.Text,
            conclusion.AdditionalImage,
            conclusion.AnyFeedback,
            conclusion.FeedbackAccompanyingText,
            conclusion.MaxFeedbackLength
        );

    }
}
