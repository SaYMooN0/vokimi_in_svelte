using System.Text.RegularExpressions;

namespace vokimi_api.Src.constants_store_classes
{
    public static class TestTagsConsts
    {
        public const int MaxTagLength = 30;
        public const int MaxTagsForTestCount = 30;
        public static readonly Regex TagRegex = new Regex(@"^[a-zA-Zа-яА-Я0-9]{1," + MaxTagLength + "}$");
    }
}
