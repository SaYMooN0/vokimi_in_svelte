export class TestCollectionVmDataForCertainTest {
    id: string;
    name: string;
    testCount: number;
    isTestInCollection: boolean;
    constructor(id: string, name: string, testCount: number, isTestInCollection: boolean) {
        this.id = id;
        this.name = name;
        this.testCount = testCount;
        this.isTestInCollection = isTestInCollection;
    }
}