using vokimi_api.Src.db_related.db_entities.draft_published_tests_shared;
using vokimi_api.Src.db_related.db_entities.test_taken_records;
using vokimi_api.Src.enums;
using vokimi_api.Src.test_publishing_data;
using VokimiShared.src.models.db_classes.test.test_types;

namespace vokimi_api.Src.db_related.db_entities.published_tests.general_test_related
{
    public class TestGeneralTemplate : BaseTest
    {
        public TestGeneralTemplate() : base(TestTemplate.General) { }

        public virtual List<GeneralTestQuestion> Questions { get; init; } = [];
        public virtual ICollection<GeneralTestResult> PossibleResults { get; init; } = [];
        public virtual ICollection<GeneralTestTakenRecord> TestTakings { get; init; } = [];
        public override ICollection<BaseTestTakenRecord> GetBaseTestTakings() =>
            TestTakings.OfType<BaseTestTakenRecord>().ToList();
        public static TestGeneralTemplate CreateNew(GeneralTestPublishingData data) => new() {
            Id = data.TestId,
            CreatorId = data.CreatorId,
            Name = data.TestName,
            Cover = data.NewTestCoverPath,
            Description = data.Description,
            Language = data.Language,
            Settings = TestSettings.Default(),
            CreationDate = data.CreationDate,
            PublicationDate = DateOnly.FromDateTime(DateTime.UtcNow),
            ConclusionId = data.ConclusionId,
            StylesSheetId = data.StylesSheetId
        };


    }
}
