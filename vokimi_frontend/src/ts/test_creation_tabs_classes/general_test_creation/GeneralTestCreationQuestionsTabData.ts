import { GeneralTestAnswerType } from "../../../ts/enums/GeneralTestAnswerType";
export class GeneralTestCreationQuestionsTabData {
    readonly questions: DraftGeneralTestQuestionBriefInfo[] = [];

    constructor(
        questions: DraftGeneralTestQuestionBriefInfo[]
    ) {
        this.questions = questions;
    }
    update(newQuestions: DraftGeneralTestQuestionBriefInfo[]): void {
        this.questions.splice(0, this.questions.length, ...newQuestions);
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
    readonly type: GeneralTestAnswerType;
    readonly answersCount: number;
    readonly isMultiple: boolean;
    readonly orderInTest: number;

    constructor(
        id: string,
        text: string,
        type: GeneralTestAnswerType,
        answersCount: number,
        isMultiple: boolean,
        orderInTest: number
    ) {
        this.id = id;
        this.text = text;
        this.type = type;
        this.answersCount = answersCount;
        this.isMultiple = isMultiple;
        this.orderInTest = orderInTest;
    }
}