using vokimi_api.Src.constants_store_classes;
using vokimi_api.Src.db_related.db_entities.draft_published_tests_shared;
using vokimi_api.Src.db_related.db_entities.draft_tests.draft_general_test;
using vokimi_api.Src.db_related.db_entities.published_tests.general_test_related;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.enums;

namespace vokimi_api.Src.test_publishing_data
{
    public record class GeneralTestPublishingData(
        TestId TestId,
        AppUserId CreatorId,
        string TestName,
        string NewTestCoverPath,
        string? Description,
        Language Language,
        TestSettings Settings,
        DateOnly CreationDate,
        TestConclusionId? ConclusionId,
        TestStylesSheetId StylesSheetId,
        string[] Tags,
        List<string> ImgsToDeleteInCaseOfSuccess,
        Dictionary<DraftGeneralTestResultId, GeneralTestResult> PublishedResults,
        List<GeneralTestQuestion> PublishedQuestions
    ) : ITestPublishingData
    {
        public static GeneralTestPublishingData Create(
            TestId testId,
            DraftGeneralTest draftTest,
            string newTestCoverPath
        ) => new(
            testId,
            draftTest.CreatorId,
            draftTest.MainInfo.Name,
            newTestCoverPath,
            draftTest.MainInfo.Description,
            draftTest.MainInfo.Language,
            draftTest.Settings,
            draftTest.CreationDate,
            draftTest.ConclusionId,
            draftTest.StylesSheetId,
            draftTest.Tags,
            new List<string>(),
            new Dictionary<DraftGeneralTestResultId, GeneralTestResult>(),
            new List<GeneralTestQuestion>()
        );
        public bool CoverRelocationNeeded => NewTestCoverPath != ImgOperationsConsts.DefaultTestCoverImg;
    }
}
