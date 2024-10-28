namespace vokimi_api.Src.dtos.responses.test_taking.general.answers_data
{
    public record class GeneralTestTakingTextOnlyAnswerData(
        string Text,
        ushort OrderInQuestion,
        string[] RelatedResultIds
    ) : IGeneralTestTakingAnswerData
    {
        public static GeneralTestTakingTextOnlyAnswerData FromAnswer(
            string text,
            ushort orderInQuestion,
            string[] relatedResults
        ) =>
            new(text, orderInQuestion, relatedResults);
    }
}
