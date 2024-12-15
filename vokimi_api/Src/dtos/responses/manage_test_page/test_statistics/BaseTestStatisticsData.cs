using vokimi_api.Src.dtos.responses.manage_test_page.test_statistics.templates_shared;

namespace vokimi_api.Src.dtos.responses.manage_test_page.test_statistics
{
    public abstract record class BaseTestStatisticsData(
        TestStatisticsTestTakenRecordsCount TestTakenRecords,
        TestStatisticsRatingsData Ratings,
        TestStatisticsDiscussionsData Discussions
    );

}
