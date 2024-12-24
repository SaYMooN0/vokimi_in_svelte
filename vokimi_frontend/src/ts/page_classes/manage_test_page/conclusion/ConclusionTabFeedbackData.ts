import { FeedbackRecordData } from "./FeedbackRecordData";

export class ConclusionTabFeedbackData {
    feedbackAccompanyingText: string;
    feedbackMaxLength: number;
    feedbackRecords: FeedbackRecordData[];
    constructor(feedbackAccompanyingText: string, feedbackMaxLength: number, feedbackRecords: FeedbackRecordData[]) {
        this.feedbackAccompanyingText = feedbackAccompanyingText;
        this.feedbackMaxLength = feedbackMaxLength;
        this.feedbackRecords = feedbackRecords;
    }
}
