import type { ManageTestStatisticsTabData } from "../ManageTestStatisticsTabData";
import type { TestStatisticsDiscussionsData } from "../templates_shared/TestStatisticsDiscussionsData";
import type { TestStatisticsRatingsData } from "../templates_shared/TestStatisticsRatingsData";
import type { TestStatisticsTestTakenRecordsCount } from "../templates_shared/TestStatisticsTestTakenRecordsCount";
import { GeneralTestResultStatisticsData } from "./general/GeneralTestResultStatisticsData";


export class GeneralTestStatisticsData implements ManageTestStatisticsTabData {
    readonly results: GeneralTestResultStatisticsData[];
    readonly testTakenRecords: TestStatisticsTestTakenRecordsCount;
    readonly ratings: TestStatisticsRatingsData;
    readonly discussions: TestStatisticsDiscussionsData;

    constructor(
        results: GeneralTestResultStatisticsData[],
        testTakenRecords: TestStatisticsTestTakenRecordsCount,
        ratings: TestStatisticsRatingsData,
        discussions: TestStatisticsDiscussionsData
    ) {
        this.results = results;
        this.testTakenRecords = testTakenRecords;
        this.ratings = ratings;
        this.discussions = discussions;
    }
    static fromResponseData(data: any): GeneralTestStatisticsData {
        return new GeneralTestStatisticsData(
            data.results.map((result: any) => new GeneralTestResultStatisticsData(
                result.resultName,
                result.resultImage,
                result.testTakenRecordsCount
            )),
            data.commonData.testTakenRecords,
            data.commonData.ratings,
            data.commonData.discussions
        );
    }
}
