namespace vokimi_api.Src.enums
{
    public enum TestPrivacy
    {
        ForMyself,
        FriendsOnly,
        FriendsAndFollowers,
        Anyone
    }
    public static class TestPrivacyExtensions
    {
        public static string GetId(this TestPrivacy privacy) => privacy switch {
            TestPrivacy.ForMyself => "for_myself",
            TestPrivacy.FriendsOnly => "friends_only",
            TestPrivacy.FriendsAndFollowers => "friends_and_followers",
            TestPrivacy.Anyone => "anyone",
            _ => throw new NotImplementedException()
        };
        public static TestPrivacy? FromId(string? id) => id switch {
            "for_myself" => TestPrivacy.ForMyself,
            "friends_only" => TestPrivacy.FriendsOnly,
            "friends_and_followers" => TestPrivacy.FriendsAndFollowers,
            "anyone" => TestPrivacy.Anyone,
            _ => null
        };
    }
}
