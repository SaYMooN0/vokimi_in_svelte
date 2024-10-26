import type { Language } from "../../../enums/Language";
import type { TestTemplate } from "../../../enums/TestTemplate";

export class TestInfoTabData {
    readonly testDescription: string;
    readonly template: TestTemplate;
    readonly language: Language;
    readonly tags: string[];
    constructor(
        testDescription: string,
        template: TestTemplate,
        language: Language,
        tags: string[]
    ) {
        this.testDescription = testDescription;
        this.template = template;
        this.language = language;
        this.tags = tags;
    }

}