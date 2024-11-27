using vokimi_api.Src.db_related.db_entities.users;
using vokimi_api.Src.enums;

namespace vokimi_api.Src.dtos.responses.profile_editing_page
{
    public record class EditPageUserPagePrivacySectionData(
        PrivacyValues PublishedTest,
        PrivacyValues Friends,
        PrivacyValues Followers,
        PrivacyValues Followings
    )
    {
        public static EditPageUserPagePrivacySectionData FromUserPageSettings(
            UserPageSettings pageData
        ) => new(
            pageData.PrivacySettings.PublishedTests,
            pageData.PrivacySettings.Friends,
            pageData.PrivacySettings.Followers,
            pageData.PrivacySettings.Followings
        );
    }
}
