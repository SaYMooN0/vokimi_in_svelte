import type { TestTemplate } from "../enums/TestTemplate";

export class DraftTestBriefInfo {
    readonly id: string;
    readonly name: string;
    readonly description: string;
    readonly cover: string;
    readonly template: TestTemplate;

    constructor(id: string, name: string, description: string, cover: string, template: TestTemplate) {
        this.id = id;
        this.name = name;
        this.description = description;
        this.cover = cover;
        this.template = template;
    }
}
