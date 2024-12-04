using vokimi_api.Src.db_related.db_entities.users;
using vokimi_api.Src.enums;

namespace vokimi_api.Src.dtos.responses.profile_editing_page
{
    public record class EditPagePrivacySettingsSectionData(
        PrivacyValues RealNamePrivacy,
        PrivacyValues RegistrationDatePrivacy,
        PrivacyValues BirthdatePrivacy,
        PrivacyValues PublishedTest,
        PrivacyValues Friends,
        PrivacyValues Followers,
        PrivacyValues Followings,
        PrivacyValues LinksPrivacy
    )
    {
        public static EditPagePrivacySettingsSectionData FromUser(AppUser user) => new(
            user.UserAdditionalInfo.PrivacySettings.RealNamePrivacy,
            user.UserAdditionalInfo.PrivacySettings.RegistrationDatePrivacy,
            user.UserAdditionalInfo.PrivacySettings.BirthDatePrivacy,
            user.UserPageSettings.PrivacySettings.PublishedTests,
            user.UserPageSettings.PrivacySettings.Friends,
            user.UserPageSettings.PrivacySettings.Followers,
            user.UserPageSettings.PrivacySettings.Followings,
            user.UserAdditionalInfo.PrivacySettings.LinksPrivacy
        );
    }
}
