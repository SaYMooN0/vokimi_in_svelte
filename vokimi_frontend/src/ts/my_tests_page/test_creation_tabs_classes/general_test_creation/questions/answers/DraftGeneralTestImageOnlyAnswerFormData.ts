import { StringUtils } from "../../../../../utils/StringUtils";
import type { IDraftGeneralTestAnswerFormData } from "./IDraftGeneralTestAnswerFormData";

export class DraftGeneralTestImageOnlyAnswerFormData implements IDraftGeneralTestAnswerFormData {
    image: string;
    relatedResults: { [key: string]: string };
    orderInQuestion: number;

    checkForErr() {
        return StringUtils.isNullOrWhiteSpace(this.image) ? "Image path can't be empty" : null;
    };

    constructor(
        imagePath: string,
        relatedResults: { [key: string]: string },
        orderInQuestion: number) {
        this.image = imagePath;
        this.relatedResults = relatedResults;
        this.orderInQuestion = orderInQuestion
    }
    static empty(): DraftGeneralTestImageOnlyAnswerFormData {
        return new DraftGeneralTestImageOnlyAnswerFormData("", {}, 0);
    }
}