import { FeedbackRecordData } from "./FeedbackRecordData";

export class ManageTestFeedbackTabData {
    feedbackAllowed: boolean;
    feedbackAccompanyingText: string;
    feedbackImagePath: string | null;
    feedbackRecords: FeedbackRecordData[];
    constructor(feedbackAllowed: boolean, feedbackAccompanyingText: string, feedbackImagePath: string | null, feedbackRecords: FeedbackRecordData[]) {
        this.feedbackAllowed = feedbackAllowed;
        this.feedbackAccompanyingText = feedbackAccompanyingText;
        this.feedbackImagePath = feedbackImagePath;
        this.feedbackRecords = feedbackRecords;
    }
    static fromResponseData(data: any): ManageTestFeedbackTabData {
        return new ManageTestFeedbackTabData(
            data.feedbackAllowed,
            data.feedbackAccompanyingText,
            data.feedbackImagePath,
            data.feedbackRecords.map((feedbackRecord: any) => new FeedbackRecordData(
                feedbackRecord.feedbackRecordId,
                feedbackRecord.feedbackText,
                feedbackRecord.feedbackDate,
                feedbackRecord.feedbackAuthorId,
                feedbackRecord.feedbackAuthorUsername,
                feedbackRecord.feedbackAuthorProfilePicture
            )),
        )
    }
}
