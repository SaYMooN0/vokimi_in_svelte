using System.Text.RegularExpressions;

namespace vokimi_api.Src.constants_store_classes
{
    public static class TestCollectionsConsts
    {
        public const int MinCollectionNameLength = 4;
        public const int MaxCollectionNameLength = 60;
        public static readonly Regex CollectionNameRegex = new Regex(@"^[a-zA-Zа-яА-Я0-9\+\-_<>,.*\s]*$");

    }
}
