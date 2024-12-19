import type { Language } from "../../../enums/Language";
import type { TestTemplate } from "../../../enums/TestTemplate";

export class ViewTestBaseInfoTabData {
    readonly testDescription: string;
    readonly template: TestTemplate;
    readonly language: Language;
    readonly tags: string[];
    readonly tagsSuggestionsAllowed: boolean;
    readonly enableTestRatings: boolean;
    readonly discussionsOpen: boolean;
    constructor(
        testDescription: string,
        template: TestTemplate,
        language: Language,
        tags: string[],
        tagsSuggestionsAllowed: boolean,
        enableTestRatings: boolean,
        discussionsOpen: boolean,
    ) {
        this.testDescription = testDescription;
        this.template = template;
        this.language = language;
        this.tags = tags;
        this.tagsSuggestionsAllowed = tagsSuggestionsAllowed;
        this.enableTestRatings = enableTestRatings;
        this.discussionsOpen = discussionsOpen;
    }

}