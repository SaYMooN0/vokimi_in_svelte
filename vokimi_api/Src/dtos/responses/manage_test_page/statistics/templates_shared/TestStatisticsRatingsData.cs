using vokimi_api.Src.db_related.db_entities.tests_related;
using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.dtos.responses.manage_test_page.statistics.templates_shared
{
    public record class TestStatisticsRatingsData(
        int AverageRating,
        int RatingsCount,
        int AverageRatingByFollowers,
        int RatingsByFollowersCount,
        int AverageRatingByFriends,
        int RatingsByFriendsCount
    )
    {
        public static TestStatisticsRatingsData Create(
            ICollection<TestRating> ratings,
            HashSet<AppUserId> creatorsFollowers,
            HashSet<AppUserId> creatorFriends
        ) {
            int ratingsCount = ratings.Count();
            int ratingsByFollowersCount = ratings.Where(r => creatorsFollowers.Contains(r.UserId)).Count();
            int ratingsByFriendsCount = ratings.Where(r => creatorFriends.Contains(r.UserId)).Count();
            return new(
                ratingsCount == 0 ? 0 : ratings.Sum(r => r.Rating) / ratingsCount,
                ratingsCount,
                ratingsByFollowersCount == 0 ? 0 : ratings.Sum(r => r.Rating) / ratingsByFollowersCount,
                ratingsByFollowersCount,
                ratingsByFriendsCount == 0 ? 0 : ratings.Sum(r => r.Rating) / ratingsByFriendsCount,
                ratingsByFriendsCount
            );
        }
    }
}
