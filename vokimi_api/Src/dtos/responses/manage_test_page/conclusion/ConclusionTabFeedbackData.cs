using vokimi_api.Src.db_related.db_entities.draft_published_tests_shared;
using vokimi_api.Src.db_related.db_entities.published_tests.published_tests_shared;

namespace vokimi_api.Src.dtos.responses.manage_test_page.conclusion
{
    public record class ConclusionTabFeedbackData(
        string FeedbackAccompanyingText,
        uint FeedbackMaxLength
    )
    {
        public static ConclusionTabFeedbackData FromTest(BaseTest test) => new(
            test.Conclusion.FeedbackAccompanyingText,
            test.Conclusion.MaxFeedbackLength
        );
    }

}
