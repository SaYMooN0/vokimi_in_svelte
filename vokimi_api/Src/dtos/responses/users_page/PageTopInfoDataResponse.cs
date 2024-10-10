using vokimi_api.Src.db_related.db_entities.users;

namespace vokimi_api.Src.dtos.responses.users_page
{
    public record class PageTopInfoDataResponse(
    string ProfilePicturePath,
    string Username,
    string BannerColor
    )
    {
        public static PageTopInfoDataResponse FromUser(AppUser user) => new(
            user.ProfilePicturePath,
            user.Username,
            user.UserPageSettings.BannerColor
        );

    }
}
