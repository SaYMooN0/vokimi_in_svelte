using vokimi_api.Src.db_related.db_entities.users;
using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.db_related.db_entities.tests_related.discussions
{
    public class DiscussionsCommentVote
    {
        public DiscussionsCommentVoteId Id { get; init; }
        public AppUserId UserId { get; init; }
        public TestDiscussionsCommentId CommentId { get; init; }
        public bool IsUp { get; private set; }
        public virtual AppUser User { get; protected set; }
        public static DiscussionsCommentVote CreateNew(AppUserId userId, TestDiscussionsCommentId commentId, bool isUp) => new() {
            Id = new(),
            UserId = userId,
            CommentId = commentId,
            IsUp = isUp
        };
        public void UpdateIsUp(bool newVal) =>
            IsUp = newVal;
    }
}
