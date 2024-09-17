using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.dtos.shared.general_test_creation.draft_general_test_answers
{
    public class DraftGeneralTestTextAndImageAnswerFormData : BaseDraftGeneralTestAnswerFormData
    {
        public string Text { get; init; }
        public string Image { get; init; }
        public static DraftGeneralTestTextAndImageAnswerFormData New(
            string text,
            string image,
            Dictionary<DraftGeneralTestResultId, string> relatedResultsIdName
        ) => new()
        {
            Text = text,
            Image = image,
            RelatedResultsIdName = relatedResultsIdName
        };
    }

}
