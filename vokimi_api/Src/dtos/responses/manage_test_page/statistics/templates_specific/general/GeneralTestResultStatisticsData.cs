using vokimi_api.Src.db_related.db_entities.published_tests.general_test_related;

namespace vokimi_api.Src.dtos.responses.manage_test_page.statistics.templates_specific.general
{
    public record GeneralTestResultStatisticsData(
        string ResultName,
        string? ResultImage,
        int TestTakenRecordsCount
    )
    {
        public static GeneralTestResultStatisticsData FromGeneralTestResult(GeneralTestResult result) => new(
            result.Name,
            result.ImagePath,
            result.TestTakenRecordsWithThisResult.Count
        );
    }
}
