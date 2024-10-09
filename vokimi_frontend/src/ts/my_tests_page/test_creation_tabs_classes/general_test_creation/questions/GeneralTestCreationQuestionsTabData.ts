import { GeneralTestAnswerType } from "../../../../enums/GeneralTestAnswerType";
export class GeneralTestCreationQuestionsTabData {
    readonly questions: DraftGeneralTestQuestionBriefInfo[] = [];

    constructor(
        questions: DraftGeneralTestQuestionBriefInfo[]
    ) {
        this.questions = questions;
    }
    static empty(): GeneralTestCreationQuestionsTabData {
        return new GeneralTestCreationQuestionsTabData([]);
    }
    isEmpty(): boolean {
        return this.questions.length === 0;
    }
}
export class DraftGeneralTestQuestionBriefInfo {
    readonly id: string;
    readonly text: string;
    readonly answersType: GeneralTestAnswerType;
    readonly answersCount: number;
    readonly isMultiple: boolean;
    readonly orderInTest: number;

    constructor(
        id: string,
        text: string,
        answersType: GeneralTestAnswerType,
        answersCount: number,
        isMultiple: boolean,
        orderInTest: number
    ) {
        this.id = id;
        this.text = text;
        this.answersType = answersType;
        this.answersCount = answersCount;
        this.isMultiple = isMultiple;
        this.orderInTest = orderInTest;
    }
}