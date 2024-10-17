namespace vokimi_api.Src.db_related.db_entities.draft_published_tests_shared.general_test_answers
{
    public class TextAndImageAnswerTypeSpecificInfo : GeneralTestAnswerTypeSpecificInfo, IAnswerTypeSpecificInfoWithImage
    {
        public string Text { get; set; }
        public string ImagePath { get; set; }
        public static TextAndImageAnswerTypeSpecificInfo CreateNew(string text, string imagePath) => new() {
            Id = new(),
            Text = text,
            ImagePath = imagePath
        };
    }
}
