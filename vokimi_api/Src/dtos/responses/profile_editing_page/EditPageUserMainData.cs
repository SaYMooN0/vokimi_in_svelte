using vokimi_api.Src.db_related.db_entities.users;

namespace vokimi_api.Src.dtos.responses.profile_editing_page
{
    public record class EditPageUserMainData(
        string Username,
        string ProfilePicture,
        string AboutMe,
        string BannerColor
    )
    {
        public static EditPageUserMainData FromUser(AppUser user) => new(
            user.Username,
            user.ProfilePicturePath,
            user.UserPageSettings.AboutMe,
            user.UserPageSettings.BannerColor
        );
    }
}
