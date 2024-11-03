import { GeneralTestAnswerType, GeneralTestAnswerTypeUtils } from "../../../enums/GeneralTestAnswerType";
import type { BaseGeneralTestTakingAnswerData } from "./GeneralTestTakingAnswersData";

export class GeneralTestTakingQuestionData {
    readonly id:string;
    readonly text: string;
    readonly image: string | null;
    readonly orderInTest: number;
    readonly minAnswersCount: number;
    readonly maxAnswersCount: number;
    readonly isSingleChoice: boolean;
    readonly answersType: GeneralTestAnswerType;
    readonly answers: BaseGeneralTestTakingAnswerData[];

    constructor(
        id:string,
        text: string,
        image: string | null,
        orderInTest: number,
        minAnswersCount: number,
        maxAnswersCount: number,
        isSingleChoice: boolean,
        answersType: string,
        answers: BaseGeneralTestTakingAnswerData[]
    ) {
        this.id=id;
        this.text = text;
        this.image = image;
        this.orderInTest = orderInTest;
        this.minAnswersCount = minAnswersCount;
        this.maxAnswersCount = maxAnswersCount;
        this.isSingleChoice = isSingleChoice;
        this.answersType = GeneralTestAnswerTypeUtils.fromId(answersType);
        this.answers = answers;
    }
}

