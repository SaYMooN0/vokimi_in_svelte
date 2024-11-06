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

        }
        public DraftTestId? GetParsedTestId() {

        }
        public PrivacyValues? GetParsedPrivacy() {
        }
    }
}
