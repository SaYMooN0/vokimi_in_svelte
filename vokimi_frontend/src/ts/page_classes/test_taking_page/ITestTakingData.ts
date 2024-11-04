import type { TestStylesArrowType } from "../../enums/TestStylesArrowType";
import type { TestTemplate } from "../../enums/TestTemplate";

export interface ITestTakingData {
    readonly testTemplate: TestTemplate,
    readonly accentColor: string,
    readonly arrowIcons: TestStylesArrowType,
    readonly conclusion: TestTakingConclusionData | null
}
export class TestTakingConclusionData {
    readonly conclusionText: string;
    readonly additionalImage: string | null;
    readonly anyFeedback: boolean;
    readonly feedbackAccompanyingText: string | null;
    readonly maxFeedbackLength: number;
    constructor(
        text: string,
        additionalImage: string | null,
        anyFeedback: boolean,
        feedbackAccompanyingText: string | null,
        maxFeedbackLength: number
    ) {
        this.conclusionText = text;
        this.additionalImage = additionalImage;
        this.anyFeedback = anyFeedback;
        this.feedbackAccompanyingText = feedbackAccompanyingText;
        this.maxFeedbackLength = maxFeedbackLength;
    }
}