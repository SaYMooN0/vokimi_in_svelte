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

    }
}
