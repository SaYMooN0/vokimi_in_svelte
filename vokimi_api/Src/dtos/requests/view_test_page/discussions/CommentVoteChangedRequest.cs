namespace vokimi_api.Src.dtos.requests.view_test_page.discussions
{
    public record class CommentVoteChangedRequest(
        string CommentId,
        bool WasUpPressed
    )
    {
    }
}
