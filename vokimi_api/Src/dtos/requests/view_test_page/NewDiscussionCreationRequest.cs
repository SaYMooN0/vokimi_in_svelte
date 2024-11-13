using vokimi_api.Src.constants_store_classes;
using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.dtos.requests.view_test_page
{
    public record class NewDiscussionCreationRequest(
        string TestId,
        string? CommentText
    //attachment data..
    )
    {
        public TestId? GetParsedTestId() =>
            Guid.TryParse(TestId, out var id)
            ? new TestId(id)
            : null;
        public Err CheckForErr() {
            if (GetParsedTestId() is null) {
                return new Err("Data transferring error. Please refresh the page and try again");
            }
            int commentLength = CommentText?.Length ?? 0;
            if (commentLength == 0) {
                return new Err("Comment cannot be empty");
            }
            if (commentLength > TestDiscussionsConsts.MaxDiscussionCommentLength) {
                return new Err($"Comment length cannot be more than {TestDiscussionsConsts.MaxDiscussionCommentLength} characters");
            }
            return Err.None;
        }
    }
}
