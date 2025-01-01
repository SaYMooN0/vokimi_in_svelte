import { TestTemplate } from "../../../enums/TestTemplate";
import { GeneralTestStatisticsData } from "./templates/GeneralTestStatisticsData";
import { ScoringTestStatisticsData } from "./templates/ScoringTestStatisticsData";
import type { TestStatisticsDiscussionsData } from "./templates_shared/TestStatisticsDiscussionsData";
import type { TestStatisticsRatingsData } from "./templates_shared/TestStatisticsRatingsData";
import type { TestStatisticsTestTakenRecordsCount } from "./templates_shared/TestStatisticsTestTakenRecordsCount";

export abstract class ManageTestStatisticsTabData {
    readonly testTakenRecords: TestStatisticsTestTakenRecordsCount;
    readonly ratings: TestStatisticsRatingsData;
    readonly discussions: TestStatisticsDiscussionsData;

    constructor(
        testTakenRecords: TestStatisticsTestTakenRecordsCount,
        ratings: TestStatisticsRatingsData,
        discussions: TestStatisticsDiscussionsData,
    ) {
        this.testTakenRecords = testTakenRecords;
        this.ratings = ratings;
        this.discussions = discussions;
    }
    static fromResponseData(data: any): ManageTestStatisticsTabData {
        switch (data.commonData.testTemplate) {
            case TestTemplate.General.toString(): return GeneralTestStatisticsData.fromResponseData(data);
            case TestTemplate.Scoring.toString(): return ScoringTestStatisticsData.fromResponseData(data);
            default: throw new Error("Unknown test template");
        }

    }
}