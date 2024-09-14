using vokimi_api.Src.db_related.db_entities.draft_tests.draft_general_test;

namespace vokimi_api.Src.dtos.responses.test_creation_responses.general
{
    public record class DraftGeneralTestResultDataToEdit(
        string Id,
        string Name,
        string Text,
        string? ImagePath
        )
    {
        public static DraftGeneralTestResultDataToEdit FromResult(DraftGeneralTestResult result) => new(
                result.Id.Value.ToString(),
                result.Name,
                result.Text ?? "",
                result.ImagePath
        );
    }

}
