import type { IDraftGeneralTestAnswerFormData } from "./IDraftGeneralTestAnswerFormData";

export class DraftGeneralTestTextAndImageAnswerFormData implements IDraftGeneralTestAnswerFormData {
    imagePath: string;
    text: string;
    relatedResults: { [key: string]: string };
    checkForErr() {
        if (this.imagePath === "" || this.imagePath === null) {
            return "Image path can't be empty";
        };
        if (this.text === "" || this.text === null) {
            return "Text can't be empty";
        };
        return null;
    };

    constructor(imagePath: string, text: string, relatedResults: { [key: string]: string }) {
        this.imagePath = imagePath;
        this.text = text;
        this.relatedResults = relatedResults;
    }
}