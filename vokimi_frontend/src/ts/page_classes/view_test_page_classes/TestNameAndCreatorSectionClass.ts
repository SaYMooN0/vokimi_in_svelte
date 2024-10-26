export class TestNameAndCreatorSectionClass {
    readonly testName: string
    readonly creatorUsername: string
    readonly creatorId: string
    constructor(testName: string, creatorUsername: string, creatorId: string) {
        this.testName = testName
        this.creatorUsername = creatorUsername
        this.creatorId = creatorId
    }
}
