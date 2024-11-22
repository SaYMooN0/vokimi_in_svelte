using vokimi_api.Src.constants_store_classes;

namespace vokimi_api.Src.dtos.requests.collections_interactions
{
    public record class CollectionCreationRequest(
        string CollectionName,
        string CollectionColor,
        string CollectionIcon
    )
    {
        public Err CheckForErr() {
            int nameLen = string.IsNullOrWhiteSpace(CollectionName) ? 0 : CollectionName.Length;
            if (nameLen < TestCollectionsConsts.MinCollectionNameLength
             || nameLen > TestCollectionsConsts.MaxCollectionNameLength
            ) {
                return new Err(
                    $"Collection name must be from {TestCollectionsConsts.MinCollectionNameLength} to " +
                    $"{TestCollectionsConsts.MaxCollectionNameLength} characters"
                );
            }

            if (!TestCollectionsConsts.CollectionNameRegex.IsMatch(CollectionName)) {
                return new Err("Collection name contains invalid characters");
            }

            if (string.IsNullOrWhiteSpace(CollectionColor)) {
                return new Err("Collection color cannot be empty");
            }
            if (!SharedConsts.HexColorRegex.IsMatch(CollectionColor)) {
                return new Err("Incorrect color data. Please refresh the page try again");
            }
            if (string.IsNullOrWhiteSpace(CollectionIcon)) {
                return new Err("Please choose a collection icon");
            }
            return Err.None;
        }
    }

}
