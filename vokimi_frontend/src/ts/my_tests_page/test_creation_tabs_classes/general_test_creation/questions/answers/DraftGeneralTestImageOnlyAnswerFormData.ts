import { StringUtils } from "../../../../../utils/StringUtils";
import type { IDraftGeneralTestAnswerFormData } from "./IDraftGeneralTestAnswerFormData";

export class DraftGeneralTestImageOnlyAnswerFormData implements IDraftGeneralTestAnswerFormData {
    image: string;
    relatedResults: { [key: string]: string };
    checkForErr() {
        return StringUtils.isNullOrWhiteSpace(this.image) ? "Image path can't be empty" : null;
    };

    constructor(imagePath: string, relatedResults: { [key: string]: string }) {
        this.image = imagePath;
        this.relatedResults = relatedResults;
    }
    static empty(): DraftGeneralTestImageOnlyAnswerFormData {
        return new DraftGeneralTestImageOnlyAnswerFormData("", {});
    }
}