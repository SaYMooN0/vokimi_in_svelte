namespace vokimi_api.Src.db_related.db_entities.draft_published_tests_shared.general_test_answers
{
    public class TextOnlyAnswerTypeSpecificInfo : BaseGeneralTestAnswerTypeSpecificInfo
    {
        public string Text { get; set; }
        public static TextOnlyAnswerTypeSpecificInfo CreateNew(string text) => new()
        {
            Id = new(),
            Text = text,
        };
    }
}
