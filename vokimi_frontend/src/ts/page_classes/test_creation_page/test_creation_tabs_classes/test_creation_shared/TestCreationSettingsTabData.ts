import { PrivacyValues } from "../../../../enums/PrivacyValues";

export class TestCreationSettingsTabData {
    public readonly privacy: PrivacyValues;
    public readonly discussionsOpen: boolean;
    public readonly testTakenPostsAllowed: boolean;

    constructor(
        privacy: PrivacyValues,
        discussionsOpen: boolean,
        testTakenPostsAllowed: boolean
    ) {
        this.privacy = privacy;
        this.discussionsOpen = discussionsOpen;
        this.testTakenPostsAllowed = testTakenPostsAllowed;
    }


    static empty(): TestCreationSettingsTabData {
        return new TestCreationSettingsTabData(
            PrivacyValues.ForMyself, false, true
        );
    }
}
