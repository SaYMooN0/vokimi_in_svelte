import { StringUtils } from "../../../utils/StringUtils";

export class TestCreationConclusionTabData {
    public readonly text: string;
    public readonly additionalImage: string | null;
    public readonly anyFeedback: boolean;
    public readonly feedbackText: string;
    public readonly maxFeedbackLength: number;

    constructor(
        text: string,
        additionalImage: string | null,
        anyFeedback: boolean,
        feedbackText: string,
        maxFeedbackLength: number
    ) {
        this.text = text;
        this.additionalImage = additionalImage;
        this.anyFeedback = anyFeedback;
        this.feedbackText = feedbackText;
        this.maxFeedbackLength = maxFeedbackLength;
    }

    isEmpty(): boolean {
        return StringUtils.isNullOrWhiteSpace(this.text)
            && StringUtils.isNullOrWhiteSpace(this.additionalImage);
    }
    copy(): TestCreationConclusionTabData {
        return new TestCreationConclusionTabData(
            this.text,
            this.additionalImage,
            this.anyFeedback,
            this.feedbackText,
            this.maxFeedbackLength
        );
    }
    static empty(): TestCreationConclusionTabData {
        return new TestCreationConclusionTabData(
            "", null, false, "Feedback", 64
        );
    }
}
