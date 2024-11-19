using vokimi_api.Src.db_related.db_entities.test_collections;
using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.dtos.responses
{
    public record class TestCollectionsVmsForCertainTest(
        string CollectionId,
        string CollectionName,
        string CollectionIconName,
        string CollectionColor,
        bool IsTestIsInCollection
    )
    {
        public static TestCollectionsVmsForCertainTest FromCollection(
            TestCollection testCollection,
            TestId testId
        ) => new(
            testCollection.Id.Value.ToString(),
            testCollection.Name,
            testCollection.Styles.IconName,
            testCollection.Styles.Color,
            testCollection.Tests.Any(t => t.Id == testId)
        );
    }
}
