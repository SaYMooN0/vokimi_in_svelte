export class TestStatisticsRatingsData {
    averageRating: number;
    ratingsCount: number;
    ratingsByFollowersCount: number;
    ratingsByFriendsCount: number;
    constructor(
        averageRating: number,
        ratingsCount: number,
        ratingsByFollowersCount: number,
        ratingsByFriendsCount: number
    ) {
        this.averageRating = averageRating;
        this.ratingsCount = ratingsCount;
        this.ratingsByFollowersCount = ratingsByFollowersCount;
        this.ratingsByFriendsCount = ratingsByFriendsCount;
    }
}