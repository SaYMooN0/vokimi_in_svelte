using vokimi_api.Src.db_related.db_entities.draft_tests.draft_general_test;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.enums;

namespace vokimi_api.Src.dtos.requests.test_creation.general_template
{
    public record class CreateGeneralTestQuestionRequest(string TestId, string AnswersType)
    {
        public ParsedCreateGeneralTestQuestionRequest? ToObjWithTypes() {
            DraftTestId? draftTestId = null;
            if (Guid.TryParse(TestId, out Guid testGuid)) {
                draftTestId = new(testGuid);
            }
            GeneralTestAnswerType? questionAnswersType = GeneralTestAnswerTypeExtensions.FromId(AnswersType);
            if (draftTestId is null ||
               questionAnswersType is null) {
                return null;
            }
            return new(draftTestId.Value, questionAnswersType.Value);


        }
    }

    public record class ParsedCreateGeneralTestQuestionRequest(DraftTestId TestId, GeneralTestAnswerType AnswersType);

}
