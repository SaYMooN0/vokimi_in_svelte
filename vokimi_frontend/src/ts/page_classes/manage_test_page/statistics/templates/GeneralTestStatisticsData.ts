import { GeneralTestResultStatisticsData } from "./general/GeneralTestResultStatisticsData";


export class GeneralTestStatisticsData {
    results: GeneralTestResultStatisticsData[];
    constructor(results: GeneralTestResultStatisticsData[]) {
        this.results = results;
    }
    static fromResponseData(data: any): GeneralTestStatisticsData {
        return new GeneralTestStatisticsData(
            data.results.map((result: any) => new GeneralTestResultStatisticsData(
                result.resultName,
                result.resultImage,
                result.testTakenRecordsCount)
            )
        );
    }
}
