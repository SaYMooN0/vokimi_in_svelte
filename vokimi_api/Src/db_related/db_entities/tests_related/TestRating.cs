using vokimi_api.Src.db_related.db_entities.users;
using vokimi_api.Src.db_related.db_entities_ids;
using VokimiShared.src.models.db_classes.test.test_types;

namespace vokimi_api.Src.db_related.db_entities.tests_related
{
    public class TestRating
    {
        public TestRatingId Id { get; init; }
        public TestId TestId { get; init; }
        public AppUserId UserId { get; init; }
        public ushort Rating { get; private set; }
        public DateTime LastUpdate { get; private set; }
        public virtual BaseTest Test { get; protected set; }
        public virtual AppUser User { get; protected set; }
        public void UpdateRatingValue(ushort rating) {
            this.Rating = rating;
            LastUpdate = DateTime.UtcNow;
        }
        public static TestRating CreateNew(TestId testId, AppUserId userId, ushort ratingValue) => new() {
            Id = new(),
            TestId = testId,
            UserId = userId,
            Rating = ratingValue,
            LastUpdate = DateTime.UtcNow
        };
    }
}
