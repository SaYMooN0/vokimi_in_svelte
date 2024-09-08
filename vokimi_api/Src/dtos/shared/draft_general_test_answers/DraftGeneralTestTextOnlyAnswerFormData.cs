using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.dtos.shared.draft_general_test_answers
{
    public class DraftGeneralTestTextOnlyAnswerFormData : BaseDraftGeneralTestAnswerFormData
    {
        public string Text { get; init; }
        public static DraftGeneralTestTextOnlyAnswerFormData New(
            string text,
            Dictionary<DraftGeneralTestResultId, string> relatedResultsIdName
        ) => new() {
            Text = text,
            RelatedResultsIdName = relatedResultsIdName
        };
    }
}
