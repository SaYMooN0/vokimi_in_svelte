export class GeneralTestResultStatisticsData {
    resultName: string;
    resultImage: string;
    testTakenRecordsCount: number;
    constructor(resultName: string, resultImage: string, testTakenRecordsCount: number) {
        this.resultName = resultName;
        this.resultImage = resultImage;
        this.testTakenRecordsCount = testTakenRecordsCount;
    }
}