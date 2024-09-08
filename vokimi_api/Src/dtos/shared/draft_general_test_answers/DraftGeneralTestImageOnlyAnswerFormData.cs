using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.dtos.shared.draft_general_test_answers
{
    public class DraftGeneralTestImageOnlyAnswerFormData : BaseDraftGeneralTestAnswerFormData
    {
        public string Image { get; init; }
        public static DraftGeneralTestImageOnlyAnswerFormData New(
            string image,
            Dictionary<DraftGeneralTestResultId, string> relatedResultsIdName
        ) => new() {
            Image = image,
            RelatedResultsIdName = relatedResultsIdName,
        };
    }
}
