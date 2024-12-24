using vokimi_api.Src.db_related.db_entities.published_tests.published_tests_shared;

namespace vokimi_api.Src.dtos.responses.manage_test_page.conclusion
{
    public record class ManageTestConclusionTabDataResponse(
        string ConclusionText,
        string? ConclusionImage,
        bool AnyFeedback,
        ConclusionTabFeedbackData? FeedbackData
    )
    {
        public static ManageTestConclusionTabDataResponse FromTest(BaseTest test) {
            throw new NotImplementedException();
        }
    }
}
