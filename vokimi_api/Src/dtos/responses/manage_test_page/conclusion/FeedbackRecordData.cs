using vokimi_api.Src.constants_store_classes;
using vokimi_api.Src.db_related.db_entities.tests_related;

namespace vokimi_api.Src.dtos.responses.manage_test_page.conclusion
{
    public record class FeedbackRecordData(
        string RecordId,
        string Text,
        DateTime CreatedAt,
        string? UserId,
        string AuthorUsername,
        string AuthorProfilePicture
    )
    {
        public static FeedbackRecordData FromTestFeedbackRecord(TestFeedbackRecord feedbackRecord) {
            if (feedbackRecord.IsAnonymous) {
                return Anonymous(feedbackRecord);
            }
            if (feedbackRecord.UserId is not null && feedbackRecord.AppUser is not null) {
                return WihtUser(feedbackRecord);
            }
            throw new InvalidOperationException("Invalid feedback record state.");
        }
        private static FeedbackRecordData Anonymous(TestFeedbackRecord f) =>
            new(f.Id.ToString(), f.Text, f.CreatedAt, null, "Anonymous user", ImgOperationsConsts.AnonymousProfilePicture);
        private static FeedbackRecordData WihtUser(TestFeedbackRecord f) =>
            new(f.Id.ToString(), f.Text, f.CreatedAt, f.AppUser.Id.ToString(), f.AppUser.Username, f.AppUser.ProfilePicturePath);
    }

}
