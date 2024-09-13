using vokimi_api.Src.constants_store_classes;
using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.dtos.requests.test_creation.general_template
{
    public record class GeneralTestResultCreationRequest(string TestId, string ResultName)
    {
        public Err GetError() {
            if (string.IsNullOrWhiteSpace(ResultName)
               || ResultName.Length > GeneralTestCreationConsts.ResultNameMaxLength
               || ResultName.Length< GeneralTestCreationConsts.ResultNameMinLength) {

                return new Err($"Result name must be between {GeneralTestCreationConsts.ResultNameMinLength} " +
                               $"and {GeneralTestCreationConsts.ResultNameMaxLength} characters");
            }
            if (!Guid.TryParse(TestId, out _)) {
                return new Err("An error occurred. Please save changes, refresh the page and try again");
            }
            return Err.None;
        }
    }
}
