using vokimi_api.Src.db_related.db_entities.published_tests.general_test_related;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.dtos.responses.manage_test_page.statistics.templates;
using vokimi_api.Src.dtos.responses.manage_test_page.statistics.templates.general;
using vokimi_api.Src.dtos.responses.manage_test_page.statistics.templates_shared;
using vokimi_api.Src.enums;

namespace vokimi_api.Src.dtos.responses.manage_test_page.statistics
{
    public record class TestStatisticsData(
        string TestTemplate,
        TestStatisticsTestTakenRecordsCount TestTakenRecords,
        TestStatisticsRatingsData Ratings,
        TestStatisticsDiscussionsData Discussions,
        ITemplateSpecificStatisticsData TemplateSpecificData
    )
    {
        public static TestStatisticsData ForGeneralTest(TestGeneralTemplate test) {

            HashSet<AppUserId> creatorsFollowers = test.Creator.Followers.Select(u => u.Id).ToHashSet();
            HashSet<AppUserId> creatorFriends = test.Creator.Friends.Select(u => u.Id).ToHashSet();
            return new(
                enums.TestTemplate.General.GetId(),
                TestStatisticsTestTakenRecordsCount.Create(test.BaseTestTakings, creatorsFollowers, creatorFriends),
                TestStatisticsRatingsData.Create(test.Ratings, creatorsFollowers, creatorFriends),
                TestStatisticsDiscussionsData.FromTestComments(test.DiscussionsComments),
                GeneralTestStatisticsData.FromGeneralTest(test)
            );
        }
    }

}
