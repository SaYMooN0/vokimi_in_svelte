using vokimi_api.Src.db_related.db_entities.published_tests.general_test_related;
using vokimi_api.Src.dtos.responses.test_taking.general;
using vokimi_api.Src.enums;

namespace vokimi_api.Src.dtos.responses.test_taking
{
    public record class GeneralTestTakingData(
        string TestTemplate,
        string AccentColor,
        string ArrowIcons,
        TestTakingConclusionData? ConclusionData,
        GeneralTestTakingQuestionData[] Questions
    ) : ITestTakingDataResponse
    {
        public static GeneralTestTakingData FromGeneralTest(TestGeneralTemplate test) => new(
            test.Template.GetId(),
            test.StylesSheet.AccentColor,
            test.StylesSheet.ArrowsType.GetId(),
            test.Conclusion is null ? null : TestTakingConclusionData.FromConclusion(test.Conclusion),
            test.Questions.Select(GeneralTestTakingQuestionData.FromQuestion).ToArray()
        );
    }
}
