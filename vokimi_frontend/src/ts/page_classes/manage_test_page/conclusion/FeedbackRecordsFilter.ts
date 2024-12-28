import { AnonymousFeedbackRecord, UserFeedbackRecord } from "./FeedbackRecordData";

export enum FeedbackTypesForFilter {
    All = "All",
    Anonymous = "AnonymousOnly",
    WithAuthor = "With Author Specified Only"

}
export class FeedbackRecordsFilter {
    dateFrom: Date | null = null;
    dateTo: Date | null = null;
    minLength: number | null = null
    maxLength: number | null = null
    feedbackType: FeedbackTypesForFilter = FeedbackTypesForFilter.All;
    applyFilter(
        records: (AnonymousFeedbackRecord | UserFeedbackRecord)[]
    ): (AnonymousFeedbackRecord | UserFeedbackRecord)[] {
        return records.filter((record) => {
            if (
                (this.dateFrom && new Date(record.date) < new Date(this.dateFrom)) ||
                (this.dateTo && new Date(record.date) > new Date(this.dateTo))
            ) {
                return false;
            }

            if (
                (this.minLength !== null && record.text.length < this.minLength) ||
                (this.maxLength !== null && record.text.length > this.maxLength)
            ) {
                return false;
            }

            if (
                this.feedbackType === FeedbackTypesForFilter.Anonymous &&
                record instanceof UserFeedbackRecord
            ) {
                return false;
            }
            if (
                this.feedbackType === FeedbackTypesForFilter.WithAuthor &&
                record instanceof AnonymousFeedbackRecord
            ) {
                return false;
            }

            return true;
        });
    }
}
