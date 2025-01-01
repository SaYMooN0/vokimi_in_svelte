using vokimi_api.Src.db_related.db_entities.published_tests.published_tests_shared;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.enums;

namespace vokimi_api.Src.dtos.responses.manage_test_page.statistics.templates_shared
{
    public record class AllCommonTestStatisticsData(
        string TestTemplate,
        TestStatisticsTestTakenRecordsCount TestTakenRecords,
        TestStatisticsRatingsData Ratings,
        TestStatisticsDiscussionsData Discussions
    )
    {
        public static AllCommonTestStatisticsData FromTest(BaseTest test) {

            HashSet<AppUserId> creatorsFollowers = test.Creator.Followers.Select(u => u.Id).ToHashSet();
            HashSet<AppUserId> creatorFriends = test.Creator.Friends.Select(u => u.Id).ToHashSet();
            return new(
                test.Template.GetId(),
                TestStatisticsTestTakenRecordsCount.Create(test.BaseTestTakings, creatorsFollowers, creatorFriends),
                TestStatisticsRatingsData.Create(test.Ratings, creatorsFollowers, creatorFriends),
                TestStatisticsDiscussionsData.FromTestComments(test.DiscussionsComments)
            );
        }
    }

}
