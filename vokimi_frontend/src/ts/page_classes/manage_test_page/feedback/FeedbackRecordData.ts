export class FeedbackRecordData {
    feedbackRecordId: string;
    feedbackText: string;
    feedbackDate: Date;
    feedbackAuthorId: string;
    feedbackAuthorUsername: string;
    feedbackAuthorProfilePicture: string;
    constructor(
        feedbackRecordId: string,
        feedbackText: string,
        feedbackDate: Date | string,
        feedbackAuthorId: string,
        feedbackAuthorUsername: string,
        feedbackAuthorProfilePicture: string
    ) {
        this.feedbackRecordId = feedbackRecordId;
        this.feedbackText = feedbackText;
        this.feedbackDate = feedbackDate instanceof Date
            ? feedbackDate
            : new Date(feedbackDate);
        this.feedbackAuthorId = feedbackAuthorId;
        this.feedbackAuthorUsername = feedbackAuthorUsername;
        this.feedbackAuthorProfilePicture = feedbackAuthorProfilePicture;
    }
}