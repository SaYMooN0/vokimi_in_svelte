import type { IDraftGeneralTestAnswerFormData } from "./IDraftGeneralTestAnswerFormData";

export class DraftGeneralTestImageOnlyAnswerFormData implements IDraftGeneralTestAnswerFormData {
    imagePath: string;
    relatedResults: { [key: string]: string };
    checkForErr() {
        return (this.imagePath === "" || this.imagePath === null) ? "Image path can't be empty" : null;
    };

    constructor(imagePath: string, relatedResults: { [key: string]: string }) {
        this.imagePath = imagePath;
        this.relatedResults = relatedResults;
    }
}