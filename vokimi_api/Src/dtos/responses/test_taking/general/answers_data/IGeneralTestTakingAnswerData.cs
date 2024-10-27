namespace vokimi_api.Src.dtos.responses.test_taking.general.answers_data
{
    public interface IGeneralTestTakingAnswerData
    {
        public string[] RelatedResults { get; init; }
        public ushort OrderInQuestion { get; init; }
    }
}
