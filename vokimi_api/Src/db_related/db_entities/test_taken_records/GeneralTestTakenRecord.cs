
using vokimi_api.Src.db_related.db_entities.published_tests.general_test_related;
using vokimi_api.Src.db_related.db_entities.users;
using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.db_related.db_entities.test_taken_records
{
    public record class GeneralTestTakenRecord(
        TestTakenRecordId Id,
        TestId TestId,
        AppUserId UserId,
        DateTime Date,
        GeneralTestResultId ReceivedResultId
    ) : BaseTestTakenRecord(Id, TestId, UserId, Date)
    {

        public virtual GeneralTestResult ReceivedResult { get; protected set; }
        //chosen answers
        public static GeneralTestTakenRecord CreateNew(
            TestGeneralTemplate test,
            AppUser? testTaker,
            GeneralTestResultId receivedResultId
        ) => new(
            new(),
            test.Id,
            testTaker is null ? new AppUserId(Guid.Empty) : testTaker.Id,
            DateTime.Now,
            receivedResultId
        );
    }
}