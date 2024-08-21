import type { TestTemplate } from "./enums/TestTemplate";

export class MyTestsPageTestViewModel {
    readonly imagePath: string;
    readonly name: string;
    readonly overviewLink: string;
    readonly template: TestTemplate;

    constructor(imagePath: string, name: string, overviewLink: string, template: TestTemplate) {
        this.imagePath = imagePath;
        this.name = name;
        this.overviewLink = overviewLink;
        this.template = template;
    }
}
