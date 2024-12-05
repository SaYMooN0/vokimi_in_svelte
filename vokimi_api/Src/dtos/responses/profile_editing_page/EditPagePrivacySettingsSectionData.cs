using vokimi_api.Src.db_related.db_entities.users;
using vokimi_api.Src.enums;

namespace vokimi_api.Src.dtos.responses.profile_editing_page
{
    public record class EditPagePrivacySettingsSectionData(
        string RealName,
        string RegistrationDate,
        string Birthdate,
        string PublishedTest,
        string Friends,
        string Followers,
        string Followings,
        string Links
    )
    {
        public static EditPagePrivacySettingsSectionData FromUser(AppUser user) => new(
            user.UserAdditionalInfo.PrivacySettings.RealNamePrivacy.GetId(),
            user.UserAdditionalInfo.PrivacySettings.RegistrationDatePrivacy.GetId(),
            user.UserAdditionalInfo.PrivacySettings.BirthDatePrivacy.GetId(),
            user.UserPageSettings.PrivacySettings.PublishedTests.GetId(),
            user.UserPageSettings.PrivacySettings.Friends.GetId(),
            user.UserPageSettings.PrivacySettings.Followers.GetId(),
            user.UserPageSettings.PrivacySettings.Followings.GetId(),
            user.UserAdditionalInfo.PrivacySettings.LinksPrivacy.GetId()
        );
    }
}
