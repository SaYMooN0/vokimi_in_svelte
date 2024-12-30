import { ConclusionTabFeedbackData } from "./ConclusionTabFeedbackData";

export class ManageTestConclusionTabData {
    conclusionText: string;
    conclusionImage: string | null;
    anyFeedback: boolean;
    feedbackData: ConclusionTabFeedbackData | null;

    constructor(
        conclusionText: string,
        conclusionImage: string | null,
        anyFeedback: boolean,
        feedbackData: ConclusionTabFeedbackData | null
    ) {
        this.conclusionText = conclusionText;
        this.conclusionImage = conclusionImage;
        this.anyFeedback = anyFeedback;
        this.feedbackData = feedbackData;
    }
    static fromResponseData(data: any): ManageTestConclusionTabData {
        return new ManageTestConclusionTabData(
            data.conclusionText,
            data.conclusionImage,
            data.anyFeedback,
            !data.anyFeedback ? null : new ConclusionTabFeedbackData(
                data.feedbackAccompanyingText,
                data.feedbackMaxLength,
            )
        );
    }
}