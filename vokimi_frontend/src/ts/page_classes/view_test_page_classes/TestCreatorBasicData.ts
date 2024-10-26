export class TestCreatorBasicData {
    readonly testCreatorId: string;
    readonly testCreatorUserName: string;
    readonly testCreatorProfilePicture: string;
    constructor(testCreatorId: string, testCreatorUserName: string, testCreatorProfilePicture: string) {
        this.testCreatorId = testCreatorId;
        this.testCreatorUserName = testCreatorUserName;
        this.testCreatorProfilePicture = testCreatorProfilePicture;
    }
}