using System.Linq;

namespace vokimi_api.Src.dtos.requests.manage_test
{
    public record FeedbackRecordsFilter(
        DateTime? DateFrom,
        DateTime? DateTo,
        int? MinLength,
        int? MaxLength,
        string FeedbackType
    )
    {
        public const string FeedbackType_All = "All";
        public const string FeedbackType_Anonymous = "AnonymousOnly";
        public const string FeedbackType_WithAuthor = "With Author Specified Only";

        private string[] _possibleFeedbackTypes = [FeedbackType_All, FeedbackType_Anonymous, FeedbackType_WithAuthor];
        public Err CheckForErr() {
            if (MinLength < 0) {
                return new Err("Min Feedback Length cannot be less than 0");
            }
            if (MaxLength < 0) {
                return new Err("Max Feedback Length cannot be less than 0");
            }
            if (MinLength > MaxLength) {
                return new Err("Min Feedback Length cannot be greater than Max Feedback Length");
            }

            if (DateFrom > DateTo) {
                return new Err("Date From cannot be later than Date To");
            }

            if (!_possibleFeedbackTypes.Contains(FeedbackType)) {
                return new Err($"Invalid feedback type: {FeedbackType}. Allowed values are 'All', 'AnonymousOnly', or 'With Author Specified Only'.");
            }

            return Err.None;
        }
    }
}
