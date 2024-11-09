export class RatingsTabData {
    viewerRating: number | null;
    averageRating: number;
    ratingsList: TestRating[];
    lastRatingsListFetchedNum: number;
    constructor(viewerRating: number | null, averageRating: number, ratingsList: TestRating[]) {
        this.viewerRating = viewerRating;
        this.averageRating = averageRating;
        this.ratingsList = ratingsList;
        this.lastRatingsListFetchedNum = 0;
    }
    addFetchedRatings(newRatingsList: TestRating[]) {
        this.ratingsList = [...this.ratingsList, ...newRatingsList]
    }
}
export interface TestRating {
    readonly ratingValue: number
    readonly userId: string
    readonly username: string
    readonly userProfilePicture: string
    readonly dateTime: string
}