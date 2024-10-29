namespace vokimi_api.Src.dtos.responses.test_taking.general.answers_data
{
    public interface IGeneralTestTakingAnswerData
    {
        public string AnswerId { get; init; }
        public ushort OrderInQuestion { get; init; }
    }
}
