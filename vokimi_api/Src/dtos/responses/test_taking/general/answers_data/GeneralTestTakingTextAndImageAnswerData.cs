namespace vokimi_api.Src.dtos.responses.test_taking.general.answers_data
{
    public record class GeneralTestTakingTextAndImageAnswerData(
        string Text,
        string Image,
        ushort OrderInQuestion,
        string[] RelatedResults
    ) : IGeneralTestTakingAnswerData
    {
        public static GeneralTestTakingTextAndImageAnswerData FromAnswer(
            string text,
            string image,
            ushort orderInQuestion,
            string[] relatedResults
        ) =>
            new(text, image, orderInQuestion, relatedResults);
    }
}
