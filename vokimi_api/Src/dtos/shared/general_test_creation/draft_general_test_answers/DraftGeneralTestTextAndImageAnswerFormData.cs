using System.Text.Json.Serialization;
using vokimi_api.Src.constants_store_classes;
using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.dtos.shared.general_test_creation.draft_general_test_answers
{
    public class DraftGeneralTestTextAndImageAnswerFormData : BaseDraftGeneralTestAnswerFormData
    {
        public string Text { get; init; }
        public string Image { get; init; }
        public DraftGeneralTestTextAndImageAnswerFormData(
            string text,
            string image,
            Dictionary<DraftGeneralTestResultId, string> relatedResultsIdName,
            int orderInQuestion
        ) {
            Text = text;
            Image = image;
            RelatedResultsIdName = relatedResultsIdName;
            OrderInQuestion = orderInQuestion;
        }
        public override Err CheckForErr(int answerNumber) {
            string errPrefix = $"Error #{answerNumber} answer: ";
            Err resultsCountErr = CheckForResultsCount();
            if (resultsCountErr.NotNone()) {
                return new Err(errPrefix, resultsCountErr);
            }
            int textLen = string.IsNullOrWhiteSpace(Text) ? 0 : Text.Length;
            if (textLen > GeneralTestCreationConsts.ResultMaxTextLength ||
                textLen < GeneralTestCreationConsts.ResultMinTextLength) {
                Err textLenErr = new(
                    $"Length of the text must be from {GeneralTestCreationConsts.ResultMinTextLength} to " +
                    $"{GeneralTestCreationConsts.ResultMaxTextLength} characters. Current length: {textLen}"
                );
                return new Err(errPrefix, textLenErr);
            }
            if (string.IsNullOrWhiteSpace(Image)) {
                return new Err(errPrefix, new Err("Please choose an image"));
            }
            return Err.None;
        }
    }

}
