import type { TestTemplate } from "./enums/TestTemplate";

export class MyTestsPageTestViewModel {
    readonly id: string;
    readonly imagePath: string;
    readonly name: string;
    readonly overviewLink: string;
    readonly template: TestTemplate;

    constructor(id: string, imagePath: string, name: string, overviewLink: string, template: TestTemplate) {
        this.id = id;
        this.imagePath = imagePath;
        this.name = name;
        this.overviewLink = overviewLink;
        this.template = template;
    }
}
