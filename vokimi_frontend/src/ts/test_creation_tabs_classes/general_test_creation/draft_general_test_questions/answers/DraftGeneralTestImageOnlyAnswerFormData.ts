import { StringUtils } from "../../../../utils/StringUtils";
import type { IDraftGeneralTestAnswerFormData } from "./IDraftGeneralTestAnswerFormData";

export class DraftGeneralTestImageOnlyAnswerFormData implements IDraftGeneralTestAnswerFormData {
    imagePath: string;
    relatedResults: { [key: string]: string };
    checkForErr() {
        return StringUtils.isNullOrWhiteSpace(this.imagePath) ? "Image path can't be empty" : null;
    };

    constructor(imagePath: string, relatedResults: { [key: string]: string }) {
        this.imagePath = imagePath;
        this.relatedResults = relatedResults;
    }
}