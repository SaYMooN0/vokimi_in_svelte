import { AnonymousFeedbackRecord, UserFeedbackRecord, type FeedbackRecordData } from "./FeedbackRecordData";

export class ConclusionTabFeedbackData {
    accompanyingText: string;
    maxLength: number;
    records: (AnonymousFeedbackRecord | UserFeedbackRecord)[];

    constructor(
        accompanyingText: string,
        maxLength: number,
        records: (AnonymousFeedbackRecord | UserFeedbackRecord)[]
    ) {
        this.accompanyingText = accompanyingText;
        this.maxLength = maxLength;
        this.records = records;
    }
    static fromResponseData(data: any): ConclusionTabFeedbackData {
        const records = data.feedbackRecords
            .map(this.mapRecordCreationFromResponseData)
            .filter((f: AnonymousFeedbackRecord | UserFeedbackRecord | null) => f != null);

        return new ConclusionTabFeedbackData(
            data.feedbackAccompanyingText,
            data.feedbackMaxLength,
            records
        );
    }
    private static mapRecordCreationFromResponseData(data: any): AnonymousFeedbackRecord | UserFeedbackRecord | null {
        if (data.Type = "UserFeedback") {
            return new UserFeedbackRecord(
                data.recordId,
                data.text,
                data.date,
                data.authorId,
                data.authorUsername,
                data.authorProfilePicture
            );
        }
        else if (data.Type = "AnonymousFeedback") {
            return new AnonymousFeedbackRecord(
                data.recordId,
                data.text,
                data.date
            );
        }
        else {
            return null;
        }
    }


}
