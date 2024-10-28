import type { IGeneralTestTakingAnswerData } from "./GeneralTestTakingAnswersData";

export class GeneralTestTakingQuestionData {
    readonly text: string;
    readonly image: string | null;
    readonly orderInTest: number;
    readonly minAnswersCount: number;
    readonly maxAnswersCount: number;
    readonly isSingleChoice: boolean;
    readonly answersType: string;
    readonly answers: IGeneralTestTakingAnswerData[];

    constructor(
        text: string,
        image: string | null,
        orderInTest: number,
        minAnswersCount: number,
        maxAnswersCount: number,
        isSingleChoice: boolean,
        answersType: string,
        answers: IGeneralTestTakingAnswerData[]
    ) {
        this.text = text;
        this.image = image;
        this.orderInTest = orderInTest;
        this.minAnswersCount = minAnswersCount;
        this.maxAnswersCount = maxAnswersCount;
        this.isSingleChoice = isSingleChoice;
        this.answersType = answersType;
        this.answers = answers;
    }
}

