export interface FeedbackRecordData {
    recordId: string;
    text: string;
    date: Date;
}

export class AnonymousFeedbackRecord implements FeedbackRecordData {
    recordId: string;
    text: string;
    date: Date;

    constructor(recordId: string, text: string, date: Date | string) {
        this.recordId = recordId;
        this.text = text;
        this.date = date instanceof Date ? date : new Date(date);
    }
}

export class UserFeedbackRecord implements FeedbackRecordData {
    recordId: string;
    text: string;
    date: Date;
    authorId: string;
    authorUsername: string;
    authorProfilePicture: string;

    constructor(
        recordId: string,
        text: string,
        date: Date | string,
        authorId: string,
        authorUsername: string,
        authorProfilePicture: string
    ) {
        this.recordId = recordId;
        this.text = text;
        this.date = date instanceof Date ? date : new Date(date);
        this.authorId = authorId;
        this.authorUsername = authorUsername;
        this.authorProfilePicture = authorProfilePicture;
    }
}
