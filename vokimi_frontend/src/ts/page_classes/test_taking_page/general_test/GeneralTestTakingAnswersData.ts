export abstract class BaseGeneralTestTakingAnswerData {
    readonly orderInQuestion: number
    readonly id: string
    constructor(orderInQuestion: number, answerId: string) {
        this.orderInQuestion = orderInQuestion == 0 ? Math.floor(Math.random() * 1000) : orderInQuestion;
        this.id = answerId;
    }
}
export class GeneralTestTakingImageOnlyAnswerData extends BaseGeneralTestTakingAnswerData {
    readonly image: string;
    constructor(orderInQuestion: number, answerId: string, imagePath: string) {
        super(orderInQuestion, answerId);
        this.image = imagePath;
    }
}
export class GeneralTestTakingTextOnlyAnswerData extends BaseGeneralTestTakingAnswerData {
    readonly text: string;
    constructor(orderInQuestion: number, answerId: string, text: string) {
        super(orderInQuestion, answerId);
        this.text = text;
    }
}

export class GeneralTestTakingTextAndImageAnswerData extends BaseGeneralTestTakingAnswerData {
    readonly text: string;
    readonly image: string;
    constructor(orderInQuestion: number, answerId: string, text: string, imagePath: string) {
        super(orderInQuestion, answerId);
        this.text = text;
        this.image = imagePath;
    }
}