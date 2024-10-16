namespace vokimi_api.Src.db_related.db_entities.draft_published_tests_shared.general_test_answers
{
    public class ImageOnlyAnswerAdditionalInfo : GeneralTestAnswerTypeSpecificInfo, IAnswerTypeSpecificInfoWithImage
    {
        public string ImagePath { get; set; }
        public static ImageOnlyAnswerAdditionalInfo CreateNew(string imagePath) => new() {
            Id = new(),
            ImagePath = imagePath
        };
    }
}
