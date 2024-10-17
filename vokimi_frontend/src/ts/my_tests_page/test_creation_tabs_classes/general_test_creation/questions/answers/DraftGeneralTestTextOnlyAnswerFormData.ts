import type { IDraftGeneralTestAnswerFormData } from "./IDraftGeneralTestAnswerFormData";

export class DraftGeneralTestTextOnlyAnswerFormData implements IDraftGeneralTestAnswerFormData {
    text: string;
    relatedResults: { [key: string]: string };
    orderInQuestion: number;
    checkForErr() {
        return (this.text === "" || this.text === null) ? "Text can't be empty" : null;
    };
    constructor(
        text: string,
        relatedResults: { [key: string]: string },
        orderInQuestion: number
    ) {
        this.text = text;
        this.relatedResults = relatedResults;
        this.orderInQuestion = orderInQuestion
    }
}