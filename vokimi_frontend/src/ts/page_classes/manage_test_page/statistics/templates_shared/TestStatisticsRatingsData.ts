export class TestStatisticsRatingsData {
    averageRating: number;
    ratingsCount: number;
    averageRatingByFollowers: number;
    ratingsByFollowersCount: number;
    averageRatingByFriends: number;
    ratingsByFriendsCount: number;
    constructor(
        averageRating: number,
        ratingsCount: number,
        averageRatingByFollowers: number,
        ratingsByFollowersCount: number,
        averageRatingByFriends: number,
        ratingsByFriendsCount: number,
    ) {
        this.averageRating = averageRating;
        this.ratingsCount = ratingsCount;
        this.averageRatingByFollowers = averageRatingByFollowers;
        this.ratingsByFollowersCount = ratingsByFollowersCount;
        this.averageRatingByFriends = averageRatingByFriends;
        this.ratingsByFriendsCount = ratingsByFriendsCount;
    }
}