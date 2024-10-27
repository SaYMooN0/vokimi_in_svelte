using vokimi_api.Src.db_related.db_entities.published_tests.general_test_related;
using vokimi_api.Src.dtos.responses.test_taking.general;
using vokimi_api.Src.enums;

namespace vokimi_api.Src.dtos.responses.test_taking
{
    public record class GeneralTestTakingData(
        string TestTemplate,
        TestTakingConclusionData? ConclusionData,
        GeneralTestTakingQuestionData[] Questions
    ) : ITestTakingDataResponse
    {
        public static GeneralTestTakingData FromGeneralTest(TestGeneralTemplate test) => new(
            test.Template.GetId(),
            test.Conclusion is null ? null : TestTakingConclusionData.FromConclusion(test.Conclusion),
            test.Questions.Select(GeneralTestTakingQuestionData.FromQuestion).ToArray()
        );
    }
}
