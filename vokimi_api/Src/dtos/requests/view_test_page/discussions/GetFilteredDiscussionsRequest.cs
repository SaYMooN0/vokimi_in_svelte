using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.dtos.requests.view_test_page.discussions
{
    public record class GetFilteredDiscussionsRequest(
        string TestId,
        int? MinChildCommentsCount,
        int? MaxChildCommentsCount,
        int? MinVotesRating,
        int? MaxVotesRating,
        int? MinVotesCount,
        int? MaxVotesCount,
        bool OnlyByFollowersAndFriends,
        bool OnlyByFriends
    )
    {
        public Err CheckForErr() {
            if (!Guid.TryParse(TestId, out _)) {
                return new Err("Data transferring error. Please refresh the page and try again");
            }

            if (MinChildCommentsCount < 0 || MaxChildCommentsCount < 0) {
                return new Err("Minimal and maximal child comments count cannot be negative");
            }
            if (MinChildCommentsCount > MaxChildCommentsCount) {
                return new Err("Minimal child comments count cannot be more than maximal child comments count");
            }

            if (MinVotesRating.HasValue && MaxVotesRating.HasValue && MinVotesRating > MaxVotesRating) {
                return new Err("Minimal votes rating cannot be more than maximal votes rating");
            }

            if (MinVotesCount < 0 || MaxVotesCount < 0) {
                return new Err("Minimal and maximal votes count cannot be negative");
            }
            if (MinVotesCount > MaxVotesCount) {
                return new Err("Minimal votes count cannot be more than maximal votes count");
            }

            return Err.None;
        }

        public ParsedDiscussionsFilter? ToParsedObject() {
            if (CheckForErr().NotNone()) {
                return null;
            }

            return new ParsedDiscussionsFilter(
                new TestId(Guid.Parse(TestId)),
                MinChildCommentsCount ?? 0,
                MaxChildCommentsCount ?? int.MaxValue,
                MinVotesRating ?? int.MinValue,
                MaxVotesRating ?? int.MaxValue,
                MinVotesCount ?? 0,
                MaxVotesCount ?? int.MaxValue,
                OnlyByFollowersAndFriends,
                OnlyByFriends
            );
        }
    }

    public record class ParsedDiscussionsFilter(
        TestId TestId,
        int MinChildCommentsCount,
        int MaxChildCommentsCount,
        int MinVotesRating,
        int MaxVotesRating,
        int MinVotesCount,
        int MaxVotesCount,
        bool OnlyByFollowersAndFriends,
        bool OnlyByFriends
    )
    {
        public bool IsChildCommentsCountFilterPassed(int commentsCount) =>
            commentsCount >= MinChildCommentsCount && commentsCount <= MaxChildCommentsCount;

        public bool IsVotesRatingFilterPassed(int rating) =>
            rating >= MinVotesRating && rating <= MaxVotesRating;

        public bool IsVotesCountFilterPassed(int votesCount) =>
            votesCount >= MinVotesCount && votesCount <= MaxVotesCount;

        public bool IsFollowersAndFriendsFilterPassed(bool isFollower, bool isFriend) {
            if (isFriend) {
                return true;
            }
            if (isFollower) {
                return !OnlyByFriends;
            }
            return !OnlyByFriends && !OnlyByFollowersAndFriends;
        }

    }

}
