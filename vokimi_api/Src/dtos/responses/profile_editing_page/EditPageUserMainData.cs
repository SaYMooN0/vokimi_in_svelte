using vokimi_api.Src.db_related.db_entities.users;

namespace vokimi_api.Src.dtos.responses.profile_editing_page
{
    public record class EditPageUserMainData(
        string Username,
        string ProfilePicture,
        string BannerColor,
        string RealName,
        DateOnly? BirthDate,
        string AboutMe,
        DateOnly RegistrationDate
    )
    {
        public static EditPageUserMainData FromUser(AppUser user) => new(
            user.Username,
            user.ProfilePicturePath,
            user.UserPageSettings.BannerColor,
            user.UserAdditionalInfo.RealName,
            user.UserAdditionalInfo.BirthDate,
            user.UserPageSettings.AboutMe,
            user.UserAdditionalInfo.RegistrationDate
        );
    }
}
