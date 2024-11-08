using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.enums;

namespace vokimi_api.Src.dtos.requests.test_creation.templates_shared
{
    public record class DraftTestSettingsUpdateRequest(
        string TestId,
        string Privacy,
        bool DiscussionsOpen,
        bool TestTakenPostsAllowed,
        bool EnableTestRatings
    )
    {
        public Err CheckForErr() {
            if (GetParsedTestId() is null || GetParsedPrivacy() is null) {
                return new Err("Data transferring error. Please refresh the page and try again");
            }
            return Err.None;
        }
        public DraftTestId? GetParsedTestId() {
            if (Guid.TryParse(TestId, out Guid testGuid)) {
                return new(testGuid);
            }
            return null;
        }
        public PrivacyValues? GetParsedPrivacy() => PrivacyValuesExtensions.FromId(Privacy);
    }
}
