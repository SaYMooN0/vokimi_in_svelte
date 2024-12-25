using vokimi_api.Src.db_related.db_entities.published_tests.published_tests_shared;
using vokimi_api.Src.db_related.db_entities.users;
using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.db_related.db_entities
{
    public abstract record class BaseTestTakenRecord(
        TestTakenRecordId Id,
        TestId TestId,
        AppUserId? UserId,
        DateTime Date
    )
    {
        public virtual BaseTest Test { get; protected set; }
        public virtual AppUser? User { get; protected set; }
    }
}
