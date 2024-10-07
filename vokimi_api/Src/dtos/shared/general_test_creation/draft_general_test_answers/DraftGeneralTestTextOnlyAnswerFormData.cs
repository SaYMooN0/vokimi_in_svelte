using System.Text.Json.Serialization;
using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.dtos.shared.general_test_creation.draft_general_test_answers
{
    public class DraftGeneralTestTextOnlyAnswerFormData : BaseDraftGeneralTestAnswerFormData
    {
        public string Text { get; init; }
        public DraftGeneralTestTextOnlyAnswerFormData(
            string text,
            Dictionary<DraftGeneralTestResultId, string> relatedResultsIdName
        ) {
            Text = text;
            RelatedResultsIdName = relatedResultsIdName;
        }
    }
}
