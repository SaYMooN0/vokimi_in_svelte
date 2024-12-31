export class GeneralTestResultStatisticsData {
    resultName: string;
    resultImage: string | null;
    testTakenRecordsCount: number;
    constructor(resultName: string, resultImage: string | null, testTakenRecordsCount: number) {
        this.resultName = resultName;
        this.resultImage = resultImage;
        this.testTakenRecordsCount = testTakenRecordsCount;
    }
}