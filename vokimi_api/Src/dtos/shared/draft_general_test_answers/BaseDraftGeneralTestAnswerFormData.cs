using System.Text.Json.Serialization;
using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.dtos.shared.draft_general_test_answers
{
    public abstract class BaseDraftGeneralTestAnswerFormData
    {
        [JsonIgnore]
        public Dictionary<DraftGeneralTestResultId, string> RelatedResultsIdName { get; init; } = [];
        [JsonPropertyName("relatedResults")]
        public Dictionary<string, string> RelatedResultsStringified
        {
            get {
                return RelatedResultsIdName.ToDictionary(
                    kvp => kvp.Key.Value.ToString(),
                    kvp => kvp.Value
                );
            }
        }
    };
}
