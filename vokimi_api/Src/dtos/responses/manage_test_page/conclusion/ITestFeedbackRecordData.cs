using vokimi_api.Src.db_related.db_entities.tests_related;

namespace vokimi_api.Src.dtos.responses.manage_test_page.conclusion
{
    public interface ITestFeedbackRecordData
    {
        string RecordId { get; }
        string Text { get; }
        DateTime CreatedAt { get; }
        public static ITestFeedbackRecordData FromTestFeedbackRecord(TestFeedbackRecord feedbackRecord) {
            if (feedbackRecord.IsAnonymous) {
                return new AnonymousFeedbackRecordData(
                    feedbackRecord.Id.ToString(),
                    feedbackRecord.Text,
                    feedbackRecord.CreatedAt
                );
            } 
            if (feedbackRecord.UserId is not null && feedbackRecord.AppUser is not null) {
                return new UserFeedbackRecordData(
                    feedbackRecord.Id.ToString(),
                    feedbackRecord.Text,
                    feedbackRecord.CreatedAt,
                    feedbackRecord.UserId.ToString(),
                    feedbackRecord.AppUser.Username,
                    feedbackRecord.AppUser.ProfilePicturePath
                );
            }
            throw new InvalidOperationException("Invalid feedback record state.");
        }
    }
    public record class AnonymousFeedbackRecordData(
        string RecordId,
        string Text,
        DateTime CreatedAt
    ) : ITestFeedbackRecordData;

    public record class UserFeedbackRecordData(
        string RecordId,
        string Text,
        DateTime CreatedAt,
        string UserId,
        string Username,
        string ProfilePicture
    ) : ITestFeedbackRecordData;
}
