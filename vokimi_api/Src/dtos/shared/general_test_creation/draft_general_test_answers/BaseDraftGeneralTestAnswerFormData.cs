using System.Text.Json.Serialization;
using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.dtos.shared.general_test_creation.draft_general_test_answers
{
    public abstract class BaseDraftGeneralTestAnswerFormData
    {
        [JsonIgnore]
        public Dictionary<DraftGeneralTestResultId, string> RelatedResultsIdName { get; set; } = [];
        [JsonPropertyName("relatedResults")]
        public Dictionary<string, string> RelatedResultsStringified
        {
            get {
                return RelatedResultsIdName.ToDictionary(
                    kvp => kvp.Key.Value.ToString(),
                    kvp => kvp.Value
                );
            }
            set {
                if (value is null) {
                    RelatedResultsIdName = new Dictionary<DraftGeneralTestResultId, string>();
                } else {
                    RelatedResultsIdName = value.ToDictionary(
                        kvp => new DraftGeneralTestResultId(Guid.Parse(kvp.Key)),
                        kvp => kvp.Value
                    );
                }
            }
        }

    }
}
