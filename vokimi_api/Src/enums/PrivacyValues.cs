namespace vokimi_api.Src.enums
{
    public enum PrivacyValues
    {
        ForMyself,
        FriendsOnly,
        FriendsAndFollowers,
        Anyone
    }
    public static class PrivacyValuesExtensions
    {
        public static string GetId(this PrivacyValues privacy) => privacy switch {
            PrivacyValues.ForMyself => "for_myself",
            PrivacyValues.FriendsOnly => "friends_only",
            PrivacyValues.FriendsAndFollowers => "friends_and_followers",
            PrivacyValues.Anyone => "anyone",
            _ => throw new NotImplementedException()
        };
        public static PrivacyValues? FromId(string? id) => id switch {
            "for_myself" => PrivacyValues.ForMyself,
            "friends_only" => PrivacyValues.FriendsOnly,
            "friends_and_followers" => PrivacyValues.FriendsAndFollowers,
            "anyone" => PrivacyValues.Anyone,
            _ => null
        };
        public static bool IsVisibleTo(this PrivacyValues itemVal, PrivacyValues viewerVal) {
            return (itemVal, viewerVal) switch {
                (PrivacyValues.Anyone, _) => true,
                (_, PrivacyValues.ForMyself) => true,
                (PrivacyValues.FriendsAndFollowers, PrivacyValues.FriendsAndFollowers or PrivacyValues.FriendsOnly) => true,
                (PrivacyValues.FriendsOnly, PrivacyValues.FriendsOnly) => true,
                _ => false
            };
        }
    }
}
