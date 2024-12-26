namespace vokimi_api.Src.dtos.responses.manage_test_page.statistics.templates.general
{
    public record GeneralTestResultStatisticsData(
        string ResultName,
        string ResultImage,
        string TestTakenRecordsCount
    )
    {
    }
}
