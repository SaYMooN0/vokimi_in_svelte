import type { TestTemplate } from "../enums/TestTemplate";

export class PublishedTestBriefInfo {
    readonly id: string;
    readonly name: string;
    readonly cover: string;
    readonly template: TestTemplate;
    readonly publishedDate: string;
    readonly takersCount: number;
    readonly averageRating: number;
    readonly commentsCount: number;

    constructor(
        id: string,
        name: string,
        cover: string,
        template: TestTemplate,
        publishedDate: string,
        takersCount: number,
        averageRating: number,
        commentsCount: number
    ) {
        this.id = id;
        this.name = name;
        this.cover = cover;
        this.template = template;
        this.publishedDate = publishedDate;
        this.takersCount = takersCount;
        this.averageRating = averageRating;
        this.commentsCount = commentsCount;
    }
}
