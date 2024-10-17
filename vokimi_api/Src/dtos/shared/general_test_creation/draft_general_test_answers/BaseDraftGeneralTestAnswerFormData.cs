using System.Text.Json.Serialization;
using vokimi_api.Src.constants_store_classes;
using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.dtos.shared.general_test_creation.draft_general_test_answers
{
    public abstract class BaseDraftGeneralTestAnswerFormData
    {
        public int OrderInQuestion { get; set; }
        //system text for deserialization
        //newtons soft for serialization

        [Newtonsoft.Json.JsonIgnore]
        [JsonIgnore] 
        public Dictionary<DraftGeneralTestResultId, string> RelatedResultsIdName { get; set; } = [];

        [JsonPropertyName("relatedResults")]
        [Newtonsoft.Json.JsonProperty(PropertyName = "relatedResults")]
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
        protected Err CheckForResultsCount() {
            if (RelatedResultsIdName.Count > GeneralTestCreationConsts.MaxRelatedResultsForAnswerCount) {
                return new Err($"Answer has too many related results ({RelatedResultsIdName.Count}). " +
                               $"Maximal related results for answer is {GeneralTestCreationConsts.MaxRelatedResultsForAnswerCount}");
            }
            return Err.None;
        }
        public abstract Err CheckForErr(int answerNumber);
    }
}
