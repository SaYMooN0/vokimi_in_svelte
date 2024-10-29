using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.dtos.responses.test_taking.general.answers_data
{
    public record class GeneralTestTakingTextAndImageAnswerData(
        string Text,
        string Image,
        ushort OrderInQuestion,
        string AnswerId
    ) : IGeneralTestTakingAnswerData
    {
        public static GeneralTestTakingTextAndImageAnswerData FromAnswer(
            string text,
            string image,
            ushort orderInQuestion,
            GeneralTestAnswerId answerId
        ) =>
            new(text, image, orderInQuestion, answerId.Value.ToString()
        );
    }
}
