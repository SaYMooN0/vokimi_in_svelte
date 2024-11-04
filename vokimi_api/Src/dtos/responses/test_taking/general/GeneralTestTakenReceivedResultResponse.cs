using vokimi_api.Src.db_related.db_entities.published_tests.general_test_related;

namespace vokimi_api.Src.dtos.responses.test_taking.general
{
    public record class GeneralTestTakenReceivedResultResponse(
        string ReceivedResultName,
        string? ReceivedResultImage,
        string ReceivedResultText,
        GeneralTestTakenResultVm[] AllResults
    )
    {
        public static GeneralTestTakenReceivedResultResponse New(
            GeneralTestResult receivedResult,
            IEnumerable<GeneralTestResult> allResults,
            int totalTestTakingsCount
        ) => new(
            receivedResult.Name,
            receivedResult.ImagePath,
            receivedResult.Text,
            allResults
                .Select(r => GeneralTestTakenResultVm.FromResult(r, totalTestTakingsCount))
                .ToArray()
        );
    }
    public record class GeneralTestTakenResultVm(
        string Id,
        string Name,
        string? Image,
        double ReceivingPercentage
    )
    {
        public static GeneralTestTakenResultVm FromResult(GeneralTestResult res, int totalTestTakingsCount) => new(
            res.Id.Value.ToString(),
            res.Name,
            res.ImagePath,
            Math.Round(res.TestTakenRecordsWithThisResult.Count() * 100.0 / totalTestTakingsCount, 2)
        );
    }
}
