using vokimi_api.Src.db_related.db_entities.published_tests.general_test_related;
using vokimi_api.Src.dtos.responses.manage_test_page.statistics.templates_specific.general;

namespace vokimi_api.Src.dtos.responses.manage_test_page.statistics.templates_shared
{
    public record GeneralTestStatisticsData(
        AllCommonTestStatisticsData CommonData,
        GeneralTestResultStatisticsData[] Results
    )
    {
        public static GeneralTestStatisticsData FromTest(TestGeneralTemplate test) => new(
            AllCommonTestStatisticsData.FromTest(test),
            test.PossibleResults
                .Select(GeneralTestResultStatisticsData.FromGeneralTestResult)
                .ToArray()
        );
    }
}
