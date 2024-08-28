namespace vokimi_api.Src.enums
{
    public enum GeneralTestAnswerType
    {
        TextOnly,
        TextAndImage,
        ImageOnly,
        //color
    }
    public static class AnswersTypeExtensions
    {
        public static bool HasImage(this GeneralTestAnswerType type) => type switch
        {
            GeneralTestAnswerType.TextOnly => false,
            GeneralTestAnswerType.TextAndImage => true,
            GeneralTestAnswerType.ImageOnly => true,
            _ => throw new NotImplementedException()
        };
        public static string GetId(this GeneralTestAnswerType type) => type switch
        {
            GeneralTestAnswerType.TextOnly => "text_only",
            GeneralTestAnswerType.TextAndImage => "text_and_image",
            GeneralTestAnswerType.ImageOnly => "image_only",
            _ => throw new NotImplementedException()
        };
    }
}
