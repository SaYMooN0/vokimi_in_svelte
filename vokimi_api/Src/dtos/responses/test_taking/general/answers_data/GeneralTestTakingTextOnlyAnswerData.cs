using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.dtos.responses.test_taking.general.answers_data
{
    public record class GeneralTestTakingTextOnlyAnswerData(
        string Text,
        ushort OrderInQuestion,
        string AnswerId
    ) : IGeneralTestTakingAnswerData
    {
        public static GeneralTestTakingTextOnlyAnswerData FromAnswer(
            string text,
            ushort orderInQuestion,
            GeneralTestAnswerId answerId
        ) =>
            new(text, orderInQuestion, answerId.Value.ToString());
    }
}
