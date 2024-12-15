import type { TestTemplate } from "../../enums/TestTemplate";

export class PublishedTestBriefInfo {
    readonly id: string;
    readonly name: string;
    readonly cover: string;
    readonly template: TestTemplate;
    readonly publishedDate: Date;
    readonly takersCount: number;

    constructor(
        id: string,
        name: string,
        cover: string,
        template: TestTemplate,
        publishedDate: Date,
        takersCount: number
    ) {
        this.id = id;
        this.name = name;
        this.cover = cover;
        this.template = template;
        this.publishedDate = publishedDate;
        this.takersCount = takersCount;
    }
}
