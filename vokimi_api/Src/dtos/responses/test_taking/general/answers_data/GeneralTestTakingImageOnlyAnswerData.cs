using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.dtos.responses.test_taking.general.answers_data
{
    public record class GeneralTestTakingImageOnlyAnswerData(
        string Image,
        ushort OrderInQuestion,
        string AnswerId
    ) : IGeneralTestTakingAnswerData
    {
        public static GeneralTestTakingImageOnlyAnswerData FromAnswer(
           string image,
           ushort orderInQuestion,
           GeneralTestAnswerId answerId
       ) =>
           new(image, orderInQuestion, answerId.Value.ToString());
    }
}
