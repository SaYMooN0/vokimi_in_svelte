using System.Text.RegularExpressions;

namespace vokimi_api.Src.constants_store_classes
{
    public static class SharedConsts
    {
        public static readonly Regex HexColorRegex = new("^#[0-9A-Fa-f]{6}$");
        public const string PrimaryColor = "#796cfa";
    }
}
