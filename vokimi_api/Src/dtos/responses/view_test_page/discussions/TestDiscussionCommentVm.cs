using vokimi_api.Src.db_related.db_entities.tests_related.discussions;
using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.dtos.responses.view_test_page.discussions
{
    public record class TestDiscussionCommentVm(
        string CommentId,
        string AuthorId,
        string AuthorUsername,
        string AuthorProfilePicture,
        string Text,//IDiscussionCommentContent or attachment
        int VotesRating,
        int TotalVotesCount,
        string CreatedAtDateTime,
        bool? IsViewersVoteUp,
        TestDiscussionCommentVm[] ChildVms
    )
    {
        public static TestDiscussionCommentVm FromComment(
            TestDiscussionsComment comment,
            Dictionary<TestDiscussionsCommentId, bool> viewersVotes
        ) => new(
            comment.Id.Value.ToString(),
            comment.AuthorId.Value.ToString(),
            comment.Author.Username,
            comment.Author.ProfilePicturePath,
            comment.Text,
            CalculateVotesRating(comment.CommentVotes),
            comment.CommentVotes.Count,
            comment.CreatedAt.ToString("HH:mm dd.MM.yyyy"),
            viewersVotes.TryGetValue(comment.Id, out var val) ? val : null,
            comment.ChildComments.Select((c) => FromComment(c, viewersVotes)).ToArray()
        );
        private static int CalculateVotesRating(ICollection<DiscussionsCommentVote> votes) =>
            votes.Sum(v => v.IsUp ? 1 : -1);
    }
}
