import { TestTemplate } from "../../../enums/TestTemplate";
import { GeneralTestStatisticsData } from "./templates/GeneralTestStatisticsData";
import { ScoringTestStatisticsData } from "./templates/ScoringTestStatisticsData";
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
    static fromResponseData(data: any): ManageTestStatisticsTabData {
        let templateSpecificData: GeneralTestStatisticsData | ScoringTestStatisticsData;
        switch (data.testTemplate) {
            case TestTemplate.General: templateSpecificData = GeneralTestStatisticsData.fromResponseData(data);
                break;
            case TestTemplate.Scoring: templateSpecificData = ScoringTestStatisticsData.fromResponseData(data);
                break;
            default: throw new Error("Unknown test template");
        }

        return new ManageTestStatisticsTabData(
            data.testTakenRecordsCount,
            data.ratings,
            data.discussions,
            templateSpecificData
        );
    }
}