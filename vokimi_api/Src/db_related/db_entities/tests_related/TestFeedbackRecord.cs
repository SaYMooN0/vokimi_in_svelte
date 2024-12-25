using vokimi_api.Src.db_related.db_entities.users;
using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.db_related.db_entities.tests_related
{
    public class TestFeedbackRecord
    {
        public TestFeedbackRecordId Id { get; init; }
        public TestId TestId { get; init; }
        public bool IsAnonymous { get; init; }
        public AppUserId? UserId { get; init; }
        public AppUser? AppUser { get; init; }
        public string Text { get; init; }
        public DateTime CreatedAt { get; init; }
        public static TestFeedbackRecord CreateAnonymous(TestId testId, string text) => new() {
            Id = new(),
            TestId = testId,
            IsAnonymous = true,
            UserId = null,
            AppUser = null,
            Text = text,
            CreatedAt = DateTime.UtcNow
        };
        public static TestFeedbackRecord CreateNew(TestId testId, AppUser user, string text) => new() {
            Id = new(),
            TestId = testId,
            IsAnonymous = false,
            UserId = user.Id,
            AppUser = user,
            Text = text,
            CreatedAt = DateTime.UtcNow
        };
    }
}
