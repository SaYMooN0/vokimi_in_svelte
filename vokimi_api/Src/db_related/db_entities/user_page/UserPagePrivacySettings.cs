using vokimi_api.Src.enums;

namespace vokimi_api.Src.db_related.db_entities.user_page
{
    public class UserPagePrivacySettings
    {
        public PrivacyValues PublishedTests { get; private set; }
        public PrivacyValues Friends { get; private set; }
        public PrivacyValues Followers { get; private set; }
        public PrivacyValues Followings { get; private set; }
        public static UserPagePrivacySettings Default => new UserPagePrivacySettings() {
            PublishedTests = PrivacyValues.Anyone,
            Friends = PrivacyValues.Anyone,
            Followers = PrivacyValues.Anyone,
            Followings = PrivacyValues.FriendsOnly
        };
        public void Update(
            PrivacyValues publishedTests,
            PrivacyValues friends,
            PrivacyValues followers,
            PrivacyValues followings
        ) {
            PublishedTests = publishedTests;
            Friends = friends;
            Followers = followers;
            Followings = followings;
        }
    }
}
