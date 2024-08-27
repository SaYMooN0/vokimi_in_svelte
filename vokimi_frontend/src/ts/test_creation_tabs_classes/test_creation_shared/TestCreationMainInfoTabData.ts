import { TestPrivacy } from "../../enums/TestPrivacy";
import { Language } from "../../enums/Language";
import { TestTemplate } from "../../enums/TestTemplate";
export class TestCreationMainInfoTabData {
    Template: TestTemplate;
    Name: string;
    Description: string;
    Language: Language;
    Privacy: TestPrivacy;
    ImgPath: string;

    constructor(template: TestTemplate, name: string, language: Language, privacy: TestPrivacy, description?: string, imgPath: string) {
        this.Template = template;
        this.Name = name;
        this.Language = language;
        this.Privacy = privacy;
        this.Description = description || "(No description)";
        this.ImgPath = imgPath;
    }
}