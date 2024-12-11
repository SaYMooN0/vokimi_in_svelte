using vokimi_api.Src.db_related.db_entities.users;
using vokimi_api.Src.enums;

namespace vokimi_api.Src.dtos.responses.users_page
{
    public record class PageTopInfoDataResponse(
        string ProfilePicturePath,
        string Username,
        string BannerColor,
        int? PublishedTestsCount,
        int? FollowingsCount,
        int? FriendsCount,
        int? FollowersCount
    )
    {
        public static PageTopInfoDataResponse ForBasicUser(AppUser user) => new PageTopInfoDataResponse(
                user.ProfilePicturePath,
                user.Username,
                user.UserPageSettings.BannerColor,
                user.UserPageSettings.PrivacySettings.PublishedTests.IsVisibleTo(PrivacyValues.Anyone)
                    ? user.PublishedTests.Count()
                    : null,
                user.UserPageSettings.PrivacySettings.Followings.IsVisibleTo(PrivacyValues.Anyone)
                    ? user.Followings.Count()
                    : null,
                user.UserPageSettings.PrivacySettings.Friends.IsVisibleTo(PrivacyValues.Anyone)
                    ? user.Friends.Count()
                    : null,
                user.UserPageSettings.PrivacySettings.Followers.IsVisibleTo(PrivacyValues.Anyone)
                    ? user.Followers.Count()
                    : null
            );

        public static PageTopInfoDataResponse ForFollower(AppUser user) => new PageTopInfoDataResponse(
            user.ProfilePicturePath,
            user.Username,
            user.UserPageSettings.BannerColor,
            user.UserPageSettings.PrivacySettings.PublishedTests.IsVisibleTo(PrivacyValues.FriendsAndFollowers)
                ? user.PublishedTests.Count()
                : null,
            user.UserPageSettings.PrivacySettings.Followings.IsVisibleTo(PrivacyValues.FriendsAndFollowers)
                ? user.Followings.Count()
                : null,
            user.UserPageSettings.PrivacySettings.Friends.IsVisibleTo(PrivacyValues.FriendsAndFollowers)
                ? user.Friends.Count()
                : null,
            user.UserPageSettings.PrivacySettings.Followers.IsVisibleTo(PrivacyValues.FriendsAndFollowers)
                ? user.Followers.Count()
                : null
        );

        public static PageTopInfoDataResponse ForFriend(AppUser user) => new PageTopInfoDataResponse(
            user.ProfilePicturePath,
            user.Username,
            user.UserPageSettings.BannerColor,
            user.UserPageSettings.PrivacySettings.PublishedTests.IsVisibleTo(PrivacyValues.FriendsOnly)
                ? user.PublishedTests.Count()
                : null,
            user.UserPageSettings.PrivacySettings.Followings.IsVisibleTo(PrivacyValues.FriendsOnly)
                ? user.Followings.Count()
                : null,
            user.UserPageSettings.PrivacySettings.Friends.IsVisibleTo(PrivacyValues.FriendsOnly)
                ? user.Friends.Count()
                : null,
            user.UserPageSettings.PrivacySettings.Followers.IsVisibleTo(PrivacyValues.FriendsOnly)
                ? user.Followers.Count()
                : null
        );
    }
}
