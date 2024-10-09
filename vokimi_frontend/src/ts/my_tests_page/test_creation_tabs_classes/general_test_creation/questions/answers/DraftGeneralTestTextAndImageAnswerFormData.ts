import type { IDraftGeneralTestAnswerFormData } from "./IDraftGeneralTestAnswerFormData";

export class DraftGeneralTestTextAndImageAnswerFormData implements IDraftGeneralTestAnswerFormData {
    image: string;
    text: string;
    relatedResults: { [key: string]: string };
    checkForErr() {
        if (this.image === "" || this.image === null) {
            return "Image path can't be empty";
        };
        if (this.text === "" || this.text === null) {
            return "Text can't be empty";
        };
        return null;
    };

    constructor(imagePath: string, text: string, relatedResults: { [key: string]: string }) {
        this.image = imagePath;
        this.text = text;
        this.relatedResults = relatedResults;
    }
    static empty(): DraftGeneralTestTextAndImageAnswerFormData {
        return new DraftGeneralTestTextAndImageAnswerFormData("", "", {});
    }
}