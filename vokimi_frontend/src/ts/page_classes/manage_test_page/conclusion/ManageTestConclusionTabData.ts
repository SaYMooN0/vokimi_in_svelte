import { ConclusionTabFeedbackData } from "./ConclusionTabFeedbackData";
import { FeedbackRecordData } from "./FeedbackRecordData";

export class ManageTestConclusionTabData {
    conclusionText: string;
    conclusionImage: string | null;
    anyFeedback: boolean;
    feedbackData: ConclusionTabFeedbackData | null;

    constructor(conclusionText: string, conclusionImage: string | null, anyFeedback: boolean, feedbackData: ConclusionTabFeedbackData | null) {
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
            !data.anyFeedback ? null :
                new ConclusionTabFeedbackData(
                    data.feedbackData.feedbackAccompanyingText,
                    data.feedbackData.feedbackMaxLength,
                    data.feedbackData.feedbackRecords.map((feedbackRecord: any) => new FeedbackRecordData(
                        feedbackRecord.feedbackRecordId,
                        feedbackRecord.feedbackText,
                        feedbackRecord.feedbackDate,
                        feedbackRecord.feedbackAuthorId,
                        feedbackRecord.feedbackAuthorUsername,
                        feedbackRecord.feedbackAuthorProfilePicture
                    )
                    )
                )
        );

    }
}