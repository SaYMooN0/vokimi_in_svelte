import type { GeneralTestAnswerType } from "../../../../enums/GeneralTestAnswerType";
import type { IDraftGeneralTestAnswerFormData } from "./answers/IDraftGeneralTestAnswerFormData";

export class DraftGeneralTestQuestionEditingData {
    id: string;
    text: string;
    imagePath: string | null;
    shuffleAnswers: boolean;
    answersType: GeneralTestAnswerType;
    isMultiple: boolean;
    minAnswersCount: number;
    maxAnswersCount: number;
    answers: IDraftGeneralTestAnswerFormData[];

    constructor(
        id: string,
        text: string,
        imagePath: string | null,
        shuffleAnswers: boolean,
        answersType: GeneralTestAnswerType,
        minAnswersCount: number,
        maxAnswersCount: number,
        answers: IDraftGeneralTestAnswerFormData[]
    ) {
        this.id = id;
        this.text = text;
        this.imagePath = imagePath;
        this.shuffleAnswers = shuffleAnswers;
        this.answersType = answersType;
        this.isMultiple = minAnswersCount != maxAnswersCount;
        this.minAnswersCount = minAnswersCount;
        this.maxAnswersCount = maxAnswersCount;
        this.answers = answers;
    }
}