namespace vokimi_api.Src.enums
{
    public enum GeneralTestAnswerType
    {
        TextOnly,
        TextAndImage,
        ImageOnly,
        //color
    }
    public static class GeneralTestAnswerTypeExtensions
    {
        public static bool HasImage(this GeneralTestAnswerType type) => type switch {
            GeneralTestAnswerType.TextOnly => false,
            GeneralTestAnswerType.TextAndImage => true,
            GeneralTestAnswerType.ImageOnly => true,
            _ => throw new NotImplementedException()
        };
        public static string GetId(this GeneralTestAnswerType type) => type switch {
            GeneralTestAnswerType.TextOnly => "text_only",
            GeneralTestAnswerType.TextAndImage => "text_and_image",
            GeneralTestAnswerType.ImageOnly => "image_only",
            _ => throw new NotImplementedException()
        };
        public static GeneralTestAnswerType? FromId(string? id) => id switch {
            "text_only" => GeneralTestAnswerType.TextOnly,
            "text_and_image" => GeneralTestAnswerType.TextAndImage,
            "image_only" => GeneralTestAnswerType.ImageOnly,
            _ => null
        };
    }
}
