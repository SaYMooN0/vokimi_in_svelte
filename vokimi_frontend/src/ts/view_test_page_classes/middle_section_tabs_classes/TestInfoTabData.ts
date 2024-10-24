export class TestInfoTabData {
    readonly testName: string;
    readonly testDescription: string;
    readonly creatorName: string;
    readonly creatorId;
    constructor(
        testName: string,
        testDescription: string,
        creatorName: string,
        creatorId: string
    ) {
        this.testName = testName;
        this.testDescription = testDescription;
        this.creatorName = creatorName;
        this.creatorId = creatorId;
    }

}