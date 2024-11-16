import type { TestDiscussionCommentVm } from "./discussions_tab_classes/TestDiscussionCommentVm";

export class ViewTestDiscussionsTabData {
    public readonly discussionsCount: number;
    public readonly totalCommentsCount: number;
    public readonly comments: TestDiscussionCommentVm[];

    constructor(discussionsCount: number, totalCommentsCount: number, comments: TestDiscussionCommentVm[]) {
        this.discussionsCount = discussionsCount;
        this.totalCommentsCount = totalCommentsCount;
        this.comments = comments;
    }
}
