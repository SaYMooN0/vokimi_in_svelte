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
    }
}
