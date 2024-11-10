export class RatingsTabData {
    viewerRating: number | null;
    averageRating: number;
    ratingsList: TestRatingVm[];
    lastRatingsListFetchedNum: number;
    constructor(viewerRating: number | null, averageRating: number, ratingsList: TestRatingVm[]) {
        this.viewerRating = viewerRating;
        this.averageRating = averageRating;
        this.ratingsList = ratingsList;
        this.lastRatingsListFetchedNum = 0;
    }
    addFetchedRatings(newRatingsList: TestRatingVm[]) {
        this.ratingsList = [...this.ratingsList, ...newRatingsList]
    }
}
export interface TestRatingVm {
    readonly ratingValue: number
    readonly userId: string
    readonly username: string
    readonly userProfilePicture: string
    readonly lastUpdateDateTime: string
}