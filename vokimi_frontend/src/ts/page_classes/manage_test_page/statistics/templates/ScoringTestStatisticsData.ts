import type { ManageTestStatisticsTabData } from "../ManageTestStatisticsTabData";
import type { TestStatisticsDiscussionsData } from "../templates_shared/TestStatisticsDiscussionsData";
import type { TestStatisticsRatingsData } from "../templates_shared/TestStatisticsRatingsData";
import type { TestStatisticsTestTakenRecordsCount } from "../templates_shared/TestStatisticsTestTakenRecordsCount";

export class ScoringTestStatisticsData implements ManageTestStatisticsTabData {
    readonly testTakenRecords: TestStatisticsTestTakenRecordsCount;
    readonly ratings: TestStatisticsRatingsData;
    readonly discussions: TestStatisticsDiscussionsData;
    constructor(
        testTakenRecords: TestStatisticsTestTakenRecordsCount,
        ratings: TestStatisticsRatingsData,
        discussions: TestStatisticsDiscussionsData
    ) {
        this.testTakenRecords = testTakenRecords;
        this.ratings = ratings;
        this.discussions = discussions;
    }
    static fromResponseData(data: any): ScoringTestStatisticsData {
        return new ScoringTestStatisticsData(
            data.commonData.testTakenRecords,
            data.commonData.ratings,
            data.commonData.discussions
        );
    }
}