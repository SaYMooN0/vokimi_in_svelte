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
        public override Err CheckForErr(int answerNumber) {
            string errPrefix = $"Error #{answerNumber} answer: ";
            Err resultsCountErr = CheckForResultsCount();
            if (resultsCountErr.NotNone()) {
                return new Err(errPrefix, resultsCountErr);
            }
            if (string.IsNullOrWhiteSpace(Image)) {
                return new Err(errPrefix, new Err("Please choose an image"));
            }
            return Err.None;
        }
    }
}
