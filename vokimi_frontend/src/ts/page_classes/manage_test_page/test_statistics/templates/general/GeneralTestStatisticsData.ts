import type { ITestStatisticsData } from "../../ITestStatisticsData";
import type { TestStatisticsDiscussionsData } from "../../templates_shared/TestStatisticsDiscussionsData";
import type { TestStatisticsRatingsData } from "../../templates_shared/TestStatisticsRatingsData";
import type { TestStatisticsTestTakenRecordsCount } from "../../templates_shared/TestStatisticsTestTakenRecordsCount";

export class GeneralTestStatisticsData implements ITestStatisticsData {
    testName: string;
    testTakenRecords: TestStatisticsTestTakenRecordsCount;
    ratings: TestStatisticsRatingsData;
    discussions: TestStatisticsDiscussionsData;
    resultsData: GeneralTestStatisticsData[];

    mostPopularResult(): GeneralTestStatisticsData {
        return this.resultsData.reduce((previous, current) => {
            return previous.testTakenRecords.all > current.testTakenRecords.all ? previous : current;
        }, this.resultsData[0]);
    }
    constructor(
        testName: string,
        testTakenRecords: TestStatisticsTestTakenRecordsCount,
        ratings: TestStatisticsRatingsData,
        discussions: TestStatisticsDiscussionsData,
        resultsData: GeneralTestStatisticsData[]
    ) {
        this.testName = testName;
        this.testTakenRecords = testTakenRecords;
        this.ratings = ratings;
        this.discussions = discussions;
        this.resultsData = resultsData;
    }
}
