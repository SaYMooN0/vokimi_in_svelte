using vokimi_api.Src.db_related.db_entities.users;
using vokimi_api.Src.db_related.db_entities_ids;
using VokimiShared.src.models.db_classes.test.test_types;

namespace vokimi_api.Src.db_related.db_entities
{
    public abstract record class BaseTestTakenRecord(
        TestTakenRecordId Id,
        TestId TestId,
        AppUserId UserId
    )
    {
        public virtual BaseTest Test { get; protected set; }
        public virtual AppUser User { get; protected set; }
    }
}
