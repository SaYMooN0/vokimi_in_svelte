import type { GeneralTestStatisticsData } from "./templates/GeneralTestStatisticsData";
import type { ScoringTestStatisticsData } from "./templates/ScoringTestStatisticsData";
import type { TestStatisticsDiscussionsData } from "./templates_shared/TestStatisticsDiscussionsData";
import type { TestStatisticsRatingsData } from "./templates_shared/TestStatisticsRatingsData";
import type { TestStatisticsTestTakenRecordsCount } from "./templates_shared/TestStatisticsTestTakenRecordsCount";

export class ManageTestStatisticsTabData {
    readonly testTakenRecords: TestStatisticsTestTakenRecordsCount;
    readonly ratings: TestStatisticsRatingsData;
    readonly discussions: TestStatisticsDiscussionsData;
    readonly templateSpecificData: GeneralTestStatisticsData | ScoringTestStatisticsData;

    constructor(
        testTakenRecords: TestStatisticsTestTakenRecordsCount,
        ratings: TestStatisticsRatingsData,
        discussions: TestStatisticsDiscussionsData,
        templateSpecificData: GeneralTestStatisticsData | ScoringTestStatisticsData
    ) {
        this.testTakenRecords = testTakenRecords;
        this.ratings = ratings;
        this.discussions = discussions;
        this.templateSpecificData = templateSpecificData;
    }
}