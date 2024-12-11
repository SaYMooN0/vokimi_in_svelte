
export class UserPageTopInfoData {
    profilePicturePath: string;
    username: string;
    bannerColor: string;
    publishedTestsCount: number | null;;
    followingsCount: number | null;
    friendsCount: number | null;
    followersCount: number | null;
    constructor(
        profilePicturePath: string,
        username: string,
        bannerColor: string,
        publishedTestsCount: number | null,
        followingsCount: number | null,
        friendsCount: number | null,
        followersCount: number | null
    ) {
        this.profilePicturePath = profilePicturePath;
        this.username = username;
        this.bannerColor = bannerColor;
        this.publishedTestsCount = publishedTestsCount;
        this.followingsCount = followingsCount;
        this.friendsCount = friendsCount;
        this.followersCount = followersCount;
    }
    static fromResponseData(data: any): UserPageTopInfoData {
        return new UserPageTopInfoData(
            data.profilePicturePath,
            data.username,
            data.bannerColor,
            data.publishedTestsCount,
            data.followingsCount,
            data.friendsCount,
            data.followersCount
        )
    }
}

