namespace vokimi_api.Src.db_related.db_entities.draft_published_tests_shared.general_test_answers
{
    public class ImageOnlyAnswerTypeSpecificInfo : BaseGeneralTestAnswerTypeSpecificInfo, IAnswerTypeSpecificInfoWithImage
    {
        public string ImagePath { get; set; }
        public static ImageOnlyAnswerTypeSpecificInfo CreateNew(string imagePath) => new() {
            Id = new(),
            ImagePath = imagePath
        };
    }
}
