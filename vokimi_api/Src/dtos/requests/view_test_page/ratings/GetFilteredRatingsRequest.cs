using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.dtos.requests.view_test_page.ratings
{
    public record class GetFilteredRatingsRequest(
        string TestId,
        ushort? RatingMinValue,
        ushort? RatingMaxValue,
        DateOnly? MinDate,
        DateOnly? MaxDate,
        bool OnlyByFollowersAndFriends,
        bool OnlyByFriends
    )
    {
        public Err CheckForErr() {
            if (!Guid.TryParse(TestId, out _)) {
                return new Err("Data transferring error. Please refresh the page and try again");
            }

            if (RatingMinValue < 1 || RatingMaxValue < 1) {
                return new Err("Minimal or maximal rating value cannot be less than 1");
            }
            if (RatingMinValue > 5 || RatingMaxValue > 5) {
                return new Err("Minimal or maximal rating value cannot be greater than 5");
            }
            if (RatingMinValue > RatingMaxValue) {
                return new Err("Minimal rating value cannot be more than maximal rating value");
            }
            var earliestAllowedDate = new DateOnly(2000, 1, 1);
            var latestAllowedDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1));

            if (MinDate < earliestAllowedDate || MaxDate < earliestAllowedDate) {
                return new Err("Dates cannot be earlier than January 1, 2000");
            }
            if (MinDate > latestAllowedDate || MaxDate > latestAllowedDate) {
                return new Err("Dates cannot be later than the current date");
            }
            if (MinDate > MaxDate) {
                return new Err("Minimal date cannot be later than maximal date");
            }

            return Err.None;
        }

        public ParsedRatingsFilter? ToParsedObject() {
            if (CheckForErr().NotNone()) {
                return null;
            }

            return new ParsedRatingsFilter(
                new TestId(Guid.Parse(TestId)),
                RatingMinValue ?? 1,
                RatingMaxValue ?? 5,
                MinDate ?? new DateOnly(2000, 1, 1),
                MaxDate ?? DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1)),
                OnlyByFollowersAndFriends,
                OnlyByFriends
            );
        }
    }

    public record class ParsedRatingsFilter(
        TestId TestId,
        ushort RatingMinValue,
        ushort RatingMaxValue,
        DateOnly MinDate,
        DateOnly MaxDate,
        bool OnlyByFollowersAndFriends,
        bool OnlyByFriends
    )
    {
        public bool IsRatingValueFilterPassed(ushort rating) =>
            rating >= RatingMinValue && rating <= RatingMaxValue;

        public bool IsDateFilterPassed(DateOnly date) =>
            date >= MinDate && date <= MaxDate;

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
