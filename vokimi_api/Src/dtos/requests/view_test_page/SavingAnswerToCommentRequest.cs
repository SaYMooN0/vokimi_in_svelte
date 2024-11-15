using vokimi_api.Src.constants_store_classes;
using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.dtos.requests.view_test_page
{
    public record class SavingAnswerToCommentRequest(
        string ParentCommentId,
        string? AnswerText
    //attachment data..    
    )
    {
        public TestDiscussionsCommentId? GetParsedParentCommentId() =>
            Guid.TryParse(ParentCommentId, out var id)
            ? new TestDiscussionsCommentId(id)
            : null;
        public Err CheckForErr() {
            if (GetParsedParentCommentId() is null) {
                return new Err("Data transferring error. Please refresh the page and try again");
            }
            int commentLength = AnswerText?.Length ?? 0;
            if (commentLength == 0) {
                return new Err("Answer cannot be empty");
            }
            if (commentLength > TestDiscussionsConsts.MaxDiscussionCommentLength) {
                return new Err($"Comment length cannot be more than {TestDiscussionsConsts.MaxDiscussionCommentLength} characters");
            }
            return Err.None;
        }
    }
}
