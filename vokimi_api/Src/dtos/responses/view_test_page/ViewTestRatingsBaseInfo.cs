
using vokimi_api.Src.db_related.db_entities.tests_related;

namespace vokimi_api.Src.dtos.responses.view_test_page
{
    public record class ViewTestRatingsBaseInfo(
        ushort ViewerRating,
        double AverageRating,
        TestRatingVm[] RatingsList
    )
    {

    }
    public record class TestRatingVm(
        ushort RatingValue,
        string UserId,
        string Username,
        string UserProfilePicturePath,
        string DateTime
    )
    {
        public static TestRatingVm FromTestRating(TestRating tr) => throw new NotImplementedException();
    }
}
