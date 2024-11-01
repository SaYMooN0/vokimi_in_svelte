using vokimi_api.Src.db_related.db_entities.published_tests.general_test_related;

namespace vokimi_api.Src.dtos.responses
{
    public record class TestTakenSuccessfullyResponse(
        string RecievedResultId,
        string RecievedResultName,
        string? RecievedResultImage,
        string RecievedResultText,
        TestTakenResultVm[] AllResults
    )
    {
        
    }
    public record class TestTakenResultVm(
        string Id,
        string Name,
        string? Image,
        float ReceivingPercentage
    ) {
        public static TestTakenResultVm FromResult(GeneralTestResult res, int totalTestTakingsCount) => new(
            res.Id.Value.ToString(),
            res.Name,
            res.ImagePath,
            0
            //res.TestTakenRecordsWithThisResult.Count()*100.0 /totalTestTakingsCount
        );
    }
}
