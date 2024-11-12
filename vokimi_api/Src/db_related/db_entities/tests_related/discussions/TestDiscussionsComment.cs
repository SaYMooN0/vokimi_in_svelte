using vokimi_api.Src.db_related.db_entities.tests_related.discussions.attachments;
using vokimi_api.Src.db_related.db_entities.users;
using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.db_related.db_entities.tests_related.discussions
{
    public class TestDiscussionsComment
    {
        public TestDiscussionsCommentId Id { get; init; }
        public TestDiscussionsCommentId? ParentCommentId { get; init; }
        public virtual TestDiscussionsComment? ParentComment { get; protected set; }
        public DiscussionsCommentAttachmentId? AttachmentId { get; init; }
        public virtual BaseDiscussionsCommentAttachment? Attachment { get; protected set; }
        public virtual ICollection<TestDiscussionsComment> CommentChildren { get; protected set; }
        public virtual ICollection<DiscussionsCommentVote> CommentVotes { get; protected set; }
        public string Text { get; private set; }
        public TestId TestId { get; init; }
        public AppUserId UserId { get; init; }
        public virtual AppUser User { get; protected set; }
        public static TestDiscussionsComment CreateNew(
            string text,
            TestDiscussionsCommentId? parentId,
            DiscussionsCommentAttachmentId? attachmentId,
            TestId testId,
            AppUserId userId
        ) => new() {
            Id = new(),
            ParentCommentId = parentId,
            AttachmentId = attachmentId,
            Text = text,
            TestId = testId,
            UserId = userId
        };

    }
}
