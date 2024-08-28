export enum TestPrivacy {
    ForMyself = "ForMyself",
    FriendsOnly = "FriendsOnly",
    FriendsAndFollowers = "FriendsAndFollowers",
    Anyone = "Anyone"
}

export namespace TestPrivacy {
    const idToEnumMap: { [key: string]: TestPrivacy } = {
        "for_myself": TestPrivacy.ForMyself,
        "friends_only": TestPrivacy.FriendsOnly,
        "friends_and_followers": TestPrivacy.FriendsAndFollowers,
        "anyone": TestPrivacy.Anyone,
    };

    export function fromId(id: string): TestPrivacy {
        return idToEnumMap[id] || TestPrivacy.ForMyself;
    }
}
