using vokimi_api.Src.db_related.db_entities.users;
using vokimi_api.Src.enums;

namespace vokimi_api.Src.dtos.responses.users_page
{
    public record class UserPageAdditionalInfoData(
        string RealName,
        string RegistrationDate,
        string BirthDate,
        Dictionary<string, string> Links,
        string LinksMassage
    )
    {
        public static UserPageAdditionalInfoData ForAnyOne(UserAdditionalInfo userData) =>
                GetUserData(userData, PrivacyValues.Anyone);

        public static UserPageAdditionalInfoData ForFollowers(UserAdditionalInfo userData) =>
            GetUserData(userData, PrivacyValues.FriendsAndFollowers);

        public static UserPageAdditionalInfoData ForFriends(UserAdditionalInfo userData) =>
            GetUserData(userData, PrivacyValues.FriendsOnly);

        private static UserPageAdditionalInfoData GetUserData(UserAdditionalInfo userData, PrivacyValues viewerPrivacyLevel) {
            string realName = userData.PrivacySettings.RealNamePrivacy.IsVisibleTo(viewerPrivacyLevel)
                ? string.IsNullOrEmpty(userData.RealName) ? "(Not set)" : userData.RealName
                : GetPrivacyString(userData.PrivacySettings.RealNamePrivacy);

            string registrationDate = userData.PrivacySettings.RegistrationDatePrivacy.IsVisibleTo(viewerPrivacyLevel)
                ? userData.RegistrationDate.ToString("dd.MM.yyyy")
                : GetPrivacyString(userData.PrivacySettings.RegistrationDatePrivacy);

            string birthDate = userData.PrivacySettings.BirthDatePrivacy.IsVisibleTo(viewerPrivacyLevel)
                ? userData.BirthDate?.ToString("dd.MM.yyyy") ?? "(Not set)"
                : GetPrivacyString(userData.PrivacySettings.BirthDatePrivacy);

            Dictionary<string, string> links;
            string linksNotVisibleMessage = string.Empty;
            if (userData.PrivacySettings.LinksPrivacy.IsVisibleTo(viewerPrivacyLevel)) {
                links = userData.Links
                    .ToDictionary()
                    .Where(kvp => !string.IsNullOrEmpty(kvp.Value))
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            } else {
                links = [];
                linksNotVisibleMessage = "You have no access to users' links" +
                    GetPrivacyString(userData.PrivacySettings.LinksPrivacy);
            }
            return new UserPageAdditionalInfoData(realName, registrationDate, birthDate, links, linksNotVisibleMessage);
        }

        public static UserPageAdditionalInfoData ForMyself(UserAdditionalInfo userData) => new(
            string.IsNullOrEmpty(userData.RealName) ? "(Not set)" : userData.RealName,
            userData.RegistrationDate.ToString("dd.MM.yyyy"),
            userData.BirthDate?.ToString("dd.MM.yyyy") ?? "(Not set)",
            userData.Links.ToDictionary()
            .ToDictionary(
                kvp => kvp.Key,
                kvp => string.IsNullOrEmpty(kvp.Value) ?
                    "(Not set)" :
                    kvp.Value
            ),
            ""
        );
        private static string GetPrivacyString(PrivacyValues privacy) => privacy switch {
            PrivacyValues.Anyone => "(For anyone)",
            PrivacyValues.ForMyself => "(Hidden)",
            PrivacyValues.FriendsOnly => "(For friends only)",
            PrivacyValues.FriendsAndFollowers => "(For friends and followers)",
            _ => ""
        };
    }
}
