namespace vokimi_api.Src.db_related.db_entities.draft_published_tests_shared.general_test_answers
{
    public class TextOnlyAnswerAdditionalInfo : GeneralTestAnswerTypeSpecificInfo
    {
        public string Text { get; set; }
        public static TextOnlyAnswerAdditionalInfo CreateNew(string text) => new()
        {
            Id = new(),
            Text = text,
        };
    }
}
