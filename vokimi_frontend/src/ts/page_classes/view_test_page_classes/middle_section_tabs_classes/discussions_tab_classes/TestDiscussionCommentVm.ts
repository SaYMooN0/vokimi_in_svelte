export class TestDiscussionCommentVm {
    public readonly commentId: string;
    public readonly authorId: string;
    public readonly authorUsername: string;
    public readonly authorProfilePicture: string;
    public readonly text: string;
    public readonly votesRating: number;
    public readonly totalVotesCount: number;
    public readonly createdAtDateTime: string;
    public readonly isViewersVoteUp: boolean | null;
    public childVms: TestDiscussionCommentVm[];

    constructor(
        commentId: string,
        authorId: string,
        authorUsername: string,
        authorProfilePicture: string,
        text: string,
        votesRating: number,
        totalVotesCount: number,
        createdAtDateTime: string,
        isViewersVoteUp: boolean | null,
        childVms: TestDiscussionCommentVm[]
    ) {
        this.commentId = commentId;
        this.authorId = authorId;
        this.authorUsername = authorUsername;
        this.authorProfilePicture = authorProfilePicture;
        this.text = text;
        this.votesRating = votesRating;
        this.totalVotesCount = totalVotesCount;
        this.createdAtDateTime = createdAtDateTime;
        this.isViewersVoteUp = isViewersVoteUp;
        this.childVms = childVms;
    }
}