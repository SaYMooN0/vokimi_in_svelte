using vokimi_api.Src.constants_store_classes;
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
        public Err CheckForErr() {
            int nameLength = string.IsNullOrEmpty(Name) ? 0 : Name.Length;
            int textLength = string.IsNullOrEmpty(Text) ? 0 : Text.Length;
            if (nameLength > GeneralTestCreationConsts.ResultNameMaxLength || nameLength < GeneralTestCreationConsts.ResultNameMinLength) {
                return new Err($"Name of the result must be between {GeneralTestCreationConsts.ResultNameMinLength} " +
                               $"and {GeneralTestCreationConsts.ResultNameMaxLength} characters");
            }
            if (textLength > GeneralTestCreationConsts.ResultMaxTextLength || textLength < GeneralTestCreationConsts.ResultMinTextLength) {
                return new Err($"Text of the result mustt be between {GeneralTestCreationConsts.ResultNameMinLength} " +
                                  $"and {GeneralTestCreationConsts.ResultNameMaxLength} characters");
            }
            return Err.None;
        }
    }

}
