import { StringUtils } from "../../../utils/StringUtils";

export class FeedbackRecordData {
    recordId: string;
    text: string;
    createdAt: Date;
    userId: string | null;
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
        this.createdAt = date instanceof Date ? date : new Date(date);
        this.userId = authorId;
        this.authorUsername = authorUsername;
        this.authorProfilePicture = authorProfilePicture;
    }
    isAnonymous(): boolean {
        return StringUtils.isNullOrWhiteSpace(this.userId)
    }
    static fromResponseData(data: any): FeedbackRecordData {
        return new FeedbackRecordData(
            data.recordId,
            data.text,
            data.createdAt,
            data.userId,
            data.authorUsername,
            data.authorProfilePicture
        );
    }
}
