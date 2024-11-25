import type { TestRatingVm } from "./ratings_tab_classes/TestRatingVm";

export class ViewTestRatingsTabData {
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
    static createEmpty(): ViewTestRatingsTabData {
        return new ViewTestRatingsTabData(null, 0, []);
    }
}