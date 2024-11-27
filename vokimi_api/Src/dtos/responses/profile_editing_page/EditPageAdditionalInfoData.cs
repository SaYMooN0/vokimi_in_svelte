using vokimi_api.Src.db_related.db_entities.users;
using vokimi_api.Src.enums;

namespace vokimi_api.Src.dtos.responses.profile_editing_page
{
    public record class EditPageAdditionalInfoData(
        string RealName,
        DateOnly RegistrationDate,
        DateOnly? BirthDate,
        PrivacyValues RealNamePrivacy,
        PrivacyValues RegistrationDatePrivacy,
        PrivacyValues BirthDatePrivacy
    )
    {
        public static EditPageAdditionalInfoData FromUserAdditionalInfo(UserAdditionalInfo info) => new(
            info.RealName,
            info.RegistrationDate,
            info.BirthDate,
            info.PrivacySettings.RealNamePrivacy,
            info.PrivacySettings.RegistrationDatePrivacy,
            info.PrivacySettings.BirthDatePrivacy
        );
    }
}
