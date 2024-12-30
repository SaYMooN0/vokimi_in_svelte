
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
}
