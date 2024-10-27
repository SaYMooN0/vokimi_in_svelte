namespace vokimi_api.Src.dtos.responses.test_taking.general.answers_data
{
    public record class GeneralTestTakingImageOnlyAnswerData(
        string Image,
        ushort OrderInQuestion,
        string[] RelatedResults
    ) : IGeneralTestTakingAnswerData
    {
        public static GeneralTestTakingImageOnlyAnswerData FromAnswer(
           string image,
           ushort orderInQuestion,
           string[] relatedResults
       ) =>
           new(image, orderInQuestion, relatedResults);
    }
}
