using vokimi_api.Src.db_related.db_entities.published_tests.general_test_related;

namespace vokimi_api.Src.dtos.responses.manage_test_page.statistics.templates.general
{
    public record GeneralTestStatisticsData(
        GeneralTestResultStatisticsData[] Results
    ) : ITemplateSpecificStatisticsData
    {
        public static GeneralTestStatisticsData FromGeneralTest(TestGeneralTemplate test) => new(
            test.PossibleResults
                .Select(GeneralTestResultStatisticsData.FromGeneralTestResult)
                .ToArray()
        );
    }
}
