namespace vokimi_api.Src.db_related.db_entities.test_collections
{
    public class TestCollectionStyles
    {
        public string Color { get; set; }
        public string IconName { get; set; }
        public static TestCollectionStyles Default => new() {
            Color = "#796cfa",
            IconName = "collection_default"
        };
    }
}
