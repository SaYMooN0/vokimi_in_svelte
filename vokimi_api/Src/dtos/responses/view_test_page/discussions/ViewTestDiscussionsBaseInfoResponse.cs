using vokimi_api.Src.db_related.db_entities.tests_related.discussions;
using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.dtos.responses.view_test_page.discussions
{
    public record class ViewTestDiscussionsBaseInfoResponse(
        int DiscussionsCount,
        int TotalCommentsCount,
        TestDiscussionCommentVm[] comments
    )
    {
        public static ViewTestDiscussionsBaseInfoResponse New(
            ICollection<TestDiscussionsComment> allComments,
            Dictionary<TestDiscussionsCommentId, bool> viewersVotes
        ) => new(
            allComments.Count(c => c.ParentCommentId is null),
            allComments.Count(),
            allComments
                .Where(c => c.ParentComment is null)
                .Select(c => TestDiscussionCommentVm.FromComment(c, viewersVotes))
                .ToArray()
        );
    }
}
