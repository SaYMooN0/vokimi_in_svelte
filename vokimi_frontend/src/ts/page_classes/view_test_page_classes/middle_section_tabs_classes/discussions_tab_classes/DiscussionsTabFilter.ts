export class DiscussionsTabFilter {
    public minChildCommentsCount: number | null = null;
    public maxChildCommentsCount: number | null = null;
    public minVotesRating: number | null = null;
    public maxVotesRating: number | null = null;
    public minVotesCount: number | null = null;
    public maxVotesCount: number | null = null;
    public onlyByFollowersAndFriends: boolean = false;
    public onlyByFriends: boolean = false;
}