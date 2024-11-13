using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.enums;

namespace vokimi_api.Src.db_related.db_entities.tests_related.discussions.attachments
{
    public abstract class BaseDiscussionsCommentAttachment
    {
        private readonly DiscussionsCommentAttachmentType _attachmentType;
        public DiscussionsCommentAttachmentType AttachmentType => _attachmentType;
        protected BaseDiscussionsCommentAttachment(DiscussionsCommentAttachmentType attachmentType) {
            _attachmentType = attachmentType;
        }

        public DiscussionsCommentAttachmentId Id { get; init; }
    }
}
