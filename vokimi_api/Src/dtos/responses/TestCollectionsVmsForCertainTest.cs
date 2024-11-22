using vokimi_api.Src.db_related.db_entities.test_collections;
using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.dtos.responses
{
    public record class TestCollectionsVmsForCertainTest(
        string CollectionId,
        string Name,
        string IconName,
        string Color,
        bool IsTestIsInCollection
    )
    {
        public static TestCollectionsVmsForCertainTest FromCollection(
            TestCollection testCollection,
            TestId testId
        ) => new(
            testCollection.Id.Value.ToString(),
            testCollection.Name,
            testCollection.IconName,
            testCollection.Color,
            testCollection.Tests.Any(t => t.Id == testId)
        );
    }
}
