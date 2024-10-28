export interface IGeneralTestTakingAnswerData {
    readonly relatedResultIds: string[],
    readonly orderInQuestion: number
}
export class GeneralTestTakingImageOnlyAnswerData implements IGeneralTestTakingAnswerData {
    readonly relatedResultIds: string[];
    readonly orderInQuestion: number;
    readonly image: string;

    constructor(relatedResultIds: string[], orderInQuestion: number, image: string) {
        this.relatedResultIds = relatedResultIds;
        this.orderInQuestion = orderInQuestion;
        this.image = image;
    }
}
export class GeneralTestTakingTextOnlyAnswerData implements IGeneralTestTakingAnswerData {
    readonly relatedResultIds: string[];
    readonly orderInQuestion: number;
    readonly text: string;
    constructor(relatedResultIds: string[], orderInQuestion: number, text: string) {
        this.relatedResultIds = relatedResultIds;
        this.orderInQuestion = orderInQuestion;
        this.text = text;

    }
}

export class GeneralTestTakingTextAndImageAnswerData implements IGeneralTestTakingAnswerData {
    readonly relatedResultIds: string[];
    readonly orderInQuestion: number;
    readonly text: string;
    readonly image: string;

    constructor(relatedResultIds: string[], orderInQuestion: number, text: string, image: string) {
        this.relatedResultIds = relatedResultIds;
        this.orderInQuestion = orderInQuestion;
        this.text = text;
        this.image = image;
    }
}