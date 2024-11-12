using vokimi_api.Src.db_related.db_entities.published_tests.general_test_related;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.enums;

namespace vokimi_api.Src.db_related.db_entities.tests_related.discussions.attachments
{
    public class GeneralTestResultCommentAttachment : BaseDiscussionsCommentAttachment
    {
        public GeneralTestResultCommentAttachment() : base(DiscussionsCommentAttachmentType.GeneralTestResult) { }
        public GeneralTestResultId ReceivedResultId { get; init; }
        public GeneralTestResult ReceivedResult { get; init; }
        public static GeneralTestResultCommentAttachment CreateNew(
            GeneralTestResultId resultId
        ) => new() {
            Id = new(),
            ReceivedResultId = resultId
        };
    }
}
