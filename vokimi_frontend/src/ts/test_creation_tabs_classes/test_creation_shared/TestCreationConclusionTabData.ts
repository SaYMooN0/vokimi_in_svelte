import { StringUtils } from "../../utils/StringUtils";

export class TestCreationConclusionTabData {
    public readonly id: string;
    public readonly text: string;
    public readonly additionalImage: string | null;
    public readonly anyFeedback: boolean;
    public readonly feedbackText: string;
    public readonly maxFeedbackLength: number;

    constructor(
        id: string,
        text: string,
        additionalImage: string | null,
        anyFeedback: boolean,
        feedbackText: string,
        maxFeedbackLength: number
    ) {
        this.id = id;
        this.text = text;
        this.additionalImage = additionalImage;
        this.anyFeedback = anyFeedback;
        this.feedbackText = feedbackText;
        this.maxFeedbackLength = maxFeedbackLength;
    }

    isEmpty(): boolean {
        return StringUtils.isNullOrWhiteSpace(this.id) && StringUtils.isNullOrWhiteSpace(this.text);
    }
    copy(): TestCreationConclusionTabData {
        return new TestCreationConclusionTabData(
            this.id,
            this.text,
            this.additionalImage,
            this.anyFeedback,
            this.feedbackText,
            this.maxFeedbackLength
        );
    }
    static empty(): TestCreationConclusionTabData {
        return new TestCreationConclusionTabData(
            "", "", null, false, "Feedback", 64
        );
    }
}
