using vokimi_api.Src.db_related.db_entities.published_tests.general_test_related;

namespace vokimi_api.Src.dtos.responses.test_taking
{
    public record class TestTakenSuccessfullyResponse(
        string ReceivedResultId,
        string RecievedResultName,
        string? RecievedResultImage,
        string RecievedResultText,
        TestTakenResultVm[] AllResults
    )
    {
        public static TestTakenSuccessfullyResponse New(
            GeneralTestResult receivedResult,
            IEnumerable<GeneralTestResult> allResults,
            int totalTestTakingsCount
        ) => new(
            receivedResult.Id.Value.ToString(),
            receivedResult.Name,
            receivedResult.ImagePath,
            receivedResult.Text,
            allResults
                .Select(r => TestTakenResultVm.FromResult(r, totalTestTakingsCount))
                .ToArray()
        );
    }
    public record class TestTakenResultVm(
        string Id,
        string Name,
        string? Image,
        double ReceivingPercentage
    )
    {
        public static TestTakenResultVm FromResult(GeneralTestResult res, int totalTestTakingsCount) => new(
            res.Id.Value.ToString(),
            res.Name,
            res.ImagePath,
            Math.Round(res.TestTakenRecordsWithThisResult.Count() * 100.0 / totalTestTakingsCount, 2)
        );
    }
}
