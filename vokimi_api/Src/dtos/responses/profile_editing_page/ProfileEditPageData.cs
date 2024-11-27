using vokimi_api.Src.db_related.db_entities.users;

namespace vokimi_api.Src.dtos.responses.profile_editing_page
{
    public record class ProfileEditPageData(
        EditPageAdditionalInfoData AdditionalInfoSection,
        string Email,
        EditPageUserMainData MainInfoSection,
        EditPageLinksData UserLinks,
        EditPageUserPagePrivacySectionData PrivacySettings
    )
    {
        public static ProfileEditPageData FromUser(AppUser user) => new(
            EditPageAdditionalInfoData.FromUserAdditionalInfo(user.UserAdditionalInfo),
            user.LoginInfo.Email,
            EditPageUserMainData.FromUser(user),
            EditPageLinksData.FromUserAdditionalInfo(user.UserAdditionalInfo),
            EditPageUserPagePrivacySectionData.FromUserPageSettings(user.UserPageSettings)
        );
    }
}
