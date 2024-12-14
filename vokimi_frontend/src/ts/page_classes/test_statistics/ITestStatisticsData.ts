import type { TestStatisticsTestTakenRecordsCount } from "./templates_shared/TestStatisticsTestTakenRecordsCount";

export interface ITestStatisticsData {
    testName: string;
    testTakenRecords: TestStatisticsTestTakenRecordsCount;
    // commentsData:
    // ratingsData
}
