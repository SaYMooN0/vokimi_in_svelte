
using vokimi_api.Src.db_related.db_entities.published_tests.general_test_related;
using vokimi_api.Src.db_related.db_entities.users;
using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.db_related.db_entities.test_taken_records
{
    public record class GeneralTestTakenRecord(
        TestTakenRecordId Id,
        TestId TestId,
        AppUserId UserId,
        GeneralTestResultId ReceivedResultId,
        string? TestFeedback
    ) : BaseTestTakenRecord(Id, TestId, UserId)
    {

        public virtual GeneralTestResult ReceivedResult { get; protected set; }
        //chosen answers
        public static GeneralTestTakenRecord CreateNew(
            TestGeneralTemplate test,
            AppUser? testTaker,
            GeneralTestResultId receivedResultId,
            string? testFeedback
        ) => new(
            new(),
            test.Id,
            testTaker is null ? new AppUserId(Guid.Empty) : testTaker.Id,
            receivedResultId,
            testFeedback
        );
    }
}