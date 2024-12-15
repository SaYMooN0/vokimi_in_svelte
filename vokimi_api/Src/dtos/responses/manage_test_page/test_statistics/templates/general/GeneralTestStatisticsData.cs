using vokimi_api.Src.dtos.responses.manage_test_page.test_statistics.templates_shared;

namespace vokimi_api.Src.dtos.responses.manage_test_page.test_statistics.templates.general
{
    public record GeneralTestStatisticsData(
        TestStatisticsTestTakenRecordsCount TestTakenRecords,
        TestStatisticsRatingsData Ratings,
        TestStatisticsDiscussionsData Discussions
        ) : BaseTestStatisticsData(TestTakenRecords, Ratings, Discussions)
    {

    }
}
