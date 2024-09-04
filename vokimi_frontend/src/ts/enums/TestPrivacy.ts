export enum TestPrivacy {
    ForMyself = "for_myself",
    FriendsOnly = "friends_only",
    FriendsAndFollowers = "friends_and_followers",
    Anyone = "anyone"
}

export namespace TestPrivacyUtils {
    export function fromId(id: string): TestPrivacy {
        for (const key in TestPrivacy) {
            if (TestPrivacy[key as keyof typeof TestPrivacy] === id) {
                return TestPrivacy[key as keyof typeof TestPrivacy];
            }
        }
        return TestPrivacy.ForMyself;
    }
    export function getFullName(privacy: TestPrivacy): string {
        switch (privacy) {
            case TestPrivacy.ForMyself:
                return "For Myself";
            case TestPrivacy.FriendsOnly:
                return "Friends Only";
            case TestPrivacy.FriendsAndFollowers:
                return "Friends and Followers";
            case TestPrivacy.Anyone:
                return "Anyone";
            default:
                return "Unknown Privacy Setting";
        }
    }
}
