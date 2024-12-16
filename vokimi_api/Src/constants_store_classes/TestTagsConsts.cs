using Amazon.S3.Model;
using System.Text.RegularExpressions;

namespace vokimi_api.Src.constants_store_classes
{
    public static class TestTagsConsts
    {
        public const int MaxTagLength = 30;
        public const int MaxTagsForTestCount = 128;

        public static readonly Regex TagRegex =
            new Regex(@"^[a-zA-Zа-яА-Я0-9\+\-_]{1," + MaxTagLength + "}$");
        public static string InvalidTagMessage(string tag) =>
            $"Invalid tag '{tag}'. Tag must contain only " +
            $"Cyrillic, Latin letters, digits or following characters: '+', '-', '_'" +
            $"and be no longer than {MaxTagLength} characters.";
    }
}
