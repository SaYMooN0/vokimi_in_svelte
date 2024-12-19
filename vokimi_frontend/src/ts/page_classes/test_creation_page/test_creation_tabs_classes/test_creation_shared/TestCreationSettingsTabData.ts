import { PrivacyValues } from "../../../../enums/PrivacyValues";

export class TestCreationSettingsTabData {
    public readonly privacy: PrivacyValues;
    public readonly discussionsOpen: boolean;
    public readonly testTakenPostsAllowed: boolean;
    public readonly enableTestRatings: boolean;
    public readonly tagsSuggestionsAllowed: boolean;

    constructor(
        privacy: PrivacyValues,
        discussionsOpen: boolean,
        testTakenPostsAllowed: boolean,
        enableTestRatings: boolean,
        tagsSuggestionsAllowed: boolean
    ) {
        this.privacy = privacy;
        this.discussionsOpen = discussionsOpen;
        this.testTakenPostsAllowed = testTakenPostsAllowed;
        this.enableTestRatings = enableTestRatings;
        this.tagsSuggestionsAllowed = tagsSuggestionsAllowed;
    }


    static empty(): TestCreationSettingsTabData {
        return new TestCreationSettingsTabData(
            PrivacyValues.ForMyself, false, true, true, true
        );
    }
}
