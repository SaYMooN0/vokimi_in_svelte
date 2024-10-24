export class DiscussionsTabData {
    totalCommentsCount: number;
    constructor(totalCommentsCount: number) {
        this.totalCommentsCount = totalCommentsCount;
    }
}

export interface CommentItem {
    readonly authorId: string;
    readonly authorName: string;
    readonly commentId: string;
    commentRating: number;
    readonly dateTime: string;
    children: CommentItem[];
}