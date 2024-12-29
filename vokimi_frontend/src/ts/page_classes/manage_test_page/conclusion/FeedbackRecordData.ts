
export class FeedbackRecordData {
    recordId: string;
    text: string;
    date: Date;
    authorId: string | null;
    authorUsername: string;
    authorProfilePicture: string;

    constructor(
        recordId: string,
        text: string,
        date: Date | string,
        authorId: string | null,
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
    isAnonymous(): boolean {
        return this.authorId === null;
    }
}
