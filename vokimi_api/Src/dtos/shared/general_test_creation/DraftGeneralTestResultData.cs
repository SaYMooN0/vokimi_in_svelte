using vokimi_api.Src.db_related.db_entities.draft_tests.draft_general_test;

namespace vokimi_api.Src.dtos.shared.general_test_creation
{
    public record class DraftGeneralTestResultData(
        string Id,
        string Name,
        string Text,
        string? ImagePath
    )
    {
        public static DraftGeneralTestResultData FromResult(DraftGeneralTestResult result) => new(
                result.Id.Value.ToString(),
                result.Name,
                result.Text ?? "",
                result.ImagePath
        );
    }

}
