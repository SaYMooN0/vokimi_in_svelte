using vokimi_api.Src.enums;
using VokimiShared.src.models.db_classes.test.test_types;

namespace vokimi_api.Src.db_related.db_entities.published_tests.general_test_related
{
    public class TestGeneralType : BaseTest
    {
        public override TestTemplate Template => TestTemplate.General;

        //public virtual List<GeneralTestQuestion> Questions { get; init; } = [];
        //public virtual ICollection<GeneralTestResult> PossibleResults { get; init; } = [];
        //public static TestGeneralType CreateNew(TestPublishingDto dto,
        //    List<GeneralTestQuestion> questions,
        //    ICollection<GeneralTestResult> possibleResults) =>
        //    new()
        //    {
        //        Id = dto.Id,
        //        CreatorId = dto.CreatorId,
        //        Name = dto.Name,
        //        Cover = dto.NewCover,
        //        Description = dto.Description,
        //        Language = dto.Language,
        //        Privacy = dto.Privacy,
        //        CreationDate = dto.CreationDate,
        //        PublicationDate = DateTime.UtcNow,
        //        ConclusionId = dto.ConclusionId,
        //        StylesSheetId = dto.StylesSheetId,
        //        Questions = questions,
        //        PossibleResults = possibleResults
        //    };

    }
}
