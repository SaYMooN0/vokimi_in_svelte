using vokimi_api.Src.db_related.db_entities.tests_related;
using VokimiShared.src.models.db_classes.test.test_types;

namespace vokimi_api.Src.dtos.responses.view_test_page
{
    public record class ViewTestRatingsBaseInfoResponse(
        ushort? ViewerRating,
        double AverageRating,
        TestRatingVm[] RatingsList
    )
    {
        public static ViewTestRatingsBaseInfoResponse New(ushort? viewerRating, BaseTest test) => new(
            viewerRating,
            CalculateAverageRating(test.Ratings),
            test.Ratings.Select(TestRatingVm.FromTestRating).ToArray()
        );
        public static double CalculateAverageRating(ICollection<TestRating> ratings) {
            if (ratings.Count < 1) {
                return 0;
            }
            double sum = ratings.Sum(r => r.Rating);
            return Math.Round(sum / ratings.Count, 2);
        }
    }
    public record class TestRatingVm(
        ushort RatingValue,
        string UserId,
        string Username,
        string UserProfilePicture,
        string LastUpdateDateTime
    )
    {
        public static TestRatingVm FromTestRating(TestRating tr) => new(
            tr.Rating,
            tr.UserId.Value.ToString(),
            tr.User.Username,
            tr.User.ProfilePicturePath,
            tr.LastUpdate.ToString("HH:mm dd.MM.yyyy")
        );
    }
}
