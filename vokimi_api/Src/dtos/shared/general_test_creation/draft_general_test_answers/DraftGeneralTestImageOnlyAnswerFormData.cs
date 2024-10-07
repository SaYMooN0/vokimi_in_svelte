using System.Text.Json.Serialization;
using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.dtos.shared.general_test_creation.draft_general_test_answers
{
    public class DraftGeneralTestImageOnlyAnswerFormData : BaseDraftGeneralTestAnswerFormData
    {
        public string Image { get; init; }
        public DraftGeneralTestImageOnlyAnswerFormData(
            string image,
            Dictionary<DraftGeneralTestResultId, string> relatedResultsIdName
        ) {
            Image = image;
            RelatedResultsIdName = relatedResultsIdName;
        }
    }
}
