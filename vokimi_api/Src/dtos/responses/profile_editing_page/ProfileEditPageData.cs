using vokimi_api.Src.db_related.db_entities.users;

namespace vokimi_api.Src.dtos.responses.profile_editing_page
{
    public record class ProfileEditPageData(
        string Email,
        EditPageUserMainData MainInfoSection,
        EditPageLinksData UserLinks,
        EditPagePrivacySettingsSectionData PrivacySettings
    )
    {
        public static ProfileEditPageData FromUser(AppUser user) => new(
            user.LoginInfo.Email,
            EditPageUserMainData.FromUser(user),
            EditPageLinksData.FromUserAdditionalInfo(user.UserAdditionalInfo),
            EditPagePrivacySettingsSectionData.FromUser(user)
        );
    }
}
