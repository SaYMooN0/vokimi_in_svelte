export enum PrivacyValues {
    ForMyself = "for_myself",
    FriendsOnly = "friends_only",
    FriendsAndFollowers = "friends_and_followers",
    Anyone = "anyone"
}

export namespace PrivacyValuesUtils {
    export function fromId(id: string): PrivacyValues {
        for (const key in PrivacyValues) {
            if (PrivacyValues[key as keyof typeof PrivacyValues] === id) {
                return PrivacyValues[key as keyof typeof PrivacyValues];
            }
        }
        return PrivacyValues.ForMyself;
    }
    export function getFullName(privacy: PrivacyValues): string {
        switch (privacy) {
            case PrivacyValues.ForMyself:
                return "For Myself";
            case PrivacyValues.FriendsOnly:
                return "Friends Only";
            case PrivacyValues.FriendsAndFollowers:
                return "Friends and Followers";
            case PrivacyValues.Anyone:
                return "Anyone";
            default:
                return "Unknown Privacy Setting";
        }
    }
}
