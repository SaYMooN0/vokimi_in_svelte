import { TestPrivacy } from "../../enums/TestPrivacy";
import { Language } from "../../enums/Language";
import { TestTemplate } from "../../enums/TestTemplate";

export class TestCreationMainInfoTabData {
    private _template: TestTemplate;
    private _name: string;
    private _description: string;
    private _language: Language;
    private _privacy: TestPrivacy;
    private _imgPath: string;

    constructor(
        template: TestTemplate,
        name: string,
        language: Language,
        privacy: TestPrivacy,
        description: string,
        imgPath: string
    ) {
        this._template = template;
        this._name = name;
        this._language = language;
        this._privacy = privacy;
        this._description = description;
        this._imgPath = imgPath;
    }

    get template(): TestTemplate { return this._template; }

    get name(): string { return this._name; }

    get description(): string { return this._description; }

    get language(): Language { return this._language; }

    get privacy(): TestPrivacy { return this._privacy; }

    get imgPath(): string { return this._imgPath; }

    isEmpty(): boolean {
        return this._name === "" && this._imgPath === "";
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

    update(
        template: TestTemplate,
        name: string,
        language: Language,
        privacy: TestPrivacy,
        description: string,
        imgPath: string
    ): void {
        this._template = template;
        this._name = name;
        this._language = language;
        this._privacy = privacy;
        this._description = description;
        this._imgPath = imgPath;
    }
}
