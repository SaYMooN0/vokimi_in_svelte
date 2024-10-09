import type { PrivacyValues } from "../enums/PrivacyValues";
import type { TestTemplate } from "../enums/TestTemplate";

export class DraftTestBriefInfo {
    readonly id: string;
    readonly name: string;
    readonly description: string;
    readonly cover: string;
    readonly template: TestTemplate;
    readonly privacy: PrivacyValues;

    constructor(id: string, name: string, description: string, cover: string, template: TestTemplate, privacy: PrivacyValues) {
        this.id = id;
        this.name = name;
        this.description = description;
        this.cover = cover;
        this.template = template;
        this.privacy = privacy;
    }
}
