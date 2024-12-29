import { FeedbackRecordData } from "./FeedbackRecordData";

export class ConclusionTabFeedbackData {
    accompanyingText: string;
    maxLength: number;
    records: FeedbackRecordData[];

    constructor(
        accompanyingText: string,
        maxLength: number,
        records: FeedbackRecordData[]
    ) {
        this.accompanyingText = accompanyingText;
        this.maxLength = maxLength;
        this.records = records;
    }
    static fromResponseData(data: any): ConclusionTabFeedbackData {
        const records = data.feedbackRecords
            .map((recordData: any) => new FeedbackRecordData(
                recordData.recordId,
                recordData.text,
                recordData.date,
                recordData.authorId,
                recordData.authorUsername,
                recordData.authorProfilePicture
            ));

        return new ConclusionTabFeedbackData(
            data.feedbackAccompanyingText,
            data.feedbackMaxLength,
            records
        );
    }
}


