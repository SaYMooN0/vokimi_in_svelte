import type { TestStatisticsDiscussionsData } from "./templates_shared/TestStatisticsDiscussionsData";
import type { TestStatisticsRatingsData } from "./templates_shared/TestStatisticsRatingsData";
import type { TestStatisticsTestTakenRecordsCount } from "./templates_shared/TestStatisticsTestTakenRecordsCount";

export interface ITestStatisticsData {
    readonly testTakenRecords: TestStatisticsTestTakenRecordsCount;
    readonly ratings: TestStatisticsRatingsData;
    readonly discussions: TestStatisticsDiscussionsData;
}
