import { TestPrivacy } from "../../enums/TestPrivacy";
import { Language } from "../../enums/Language";
import { TestTemplate } from "../../enums/TestTemplate";

export class TestCreationMainInfoTabData {
    public readonly template: TestTemplate;
    public readonly name: string;
    public readonly description: string;
    public readonly language: Language;
    public readonly privacy: TestPrivacy;
    public readonly imgPath: string;

    constructor(
        template: TestTemplate,
        name: string,
        language: Language,
        privacy: TestPrivacy,
        description: string,
        imgPath: string
    ) {
        this.template = template;
        this.name = name;
        this.language = language;
        this.privacy = privacy;
        this.description = description;
        this.imgPath = imgPath;
    }

    isEmpty(): boolean {
        return this.name === "" && this.imgPath === "";
    }

    static empty(): TestCreationMainInfoTabData {
        return new TestCreationMainInfoTabData(
            TestTemplate.General,
            "",
            Language.Other,
            TestPrivacy.ForMyself,
            "",
            ""
        );
    }
}
