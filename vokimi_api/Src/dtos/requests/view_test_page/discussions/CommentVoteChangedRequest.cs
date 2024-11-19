using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.dtos.requests.view_test_page.discussions
{
    public record class CommentVoteChangedRequest(
        string CommentId,
        bool WasUpPressed
    )
    {
        public TestDiscussionsCommentId? ParsedCommentId =>
            Guid.TryParse(CommentId, out var cGuid) ? new(cGuid) : null;
    }
}
