namespace vokimi_api.Src.enums
{
    public enum Language
    {
        Eng,
        Rus,
        Spa,
        Ger,
        Fra,
        Other
    }
    public static class LanguageExtensions
    {
        public static string GetId(this Language language) => language switch {
            Language.Eng => "eng",
            Language.Rus => "rus",
            Language.Spa => "spa",
            Language.Ger => "ger",
            Language.Fra => "fra",
            Language.Other => "other",
            _ => throw new NotImplementedException()
        };

    }
}
