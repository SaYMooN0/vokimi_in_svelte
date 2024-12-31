using vokimi_api.Src.db_related.db_entities.published_tests.published_tests_shared;
using vokimi_api.Src.db_related.db_entities.tests_related.discussions;

namespace vokimi_api.Src.dtos.responses.manage_test_page.statistics.templates_shared
{
    public record TestStatisticsDiscussionsData(
        int TotalCommentsCount,
        int DiscussionsCount
    )
    {
        public static TestStatisticsDiscussionsData FromTestComments(ICollection<TestDiscussionsComment> allComments) => new(
            allComments.Count(),
            allComments.Count(c => c.ParentCommentId is null)
        );
    }
}
