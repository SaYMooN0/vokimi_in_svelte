export class TestStatisticsDiscussionsData {
    readonly totalCommentsCount: number;
    readonly discussionsCount: number;
    constructor(totalCommentsCount: number, discussionsCount: number) {
        this.totalCommentsCount = totalCommentsCount;
        this.discussionsCount = discussionsCount;
    }
}