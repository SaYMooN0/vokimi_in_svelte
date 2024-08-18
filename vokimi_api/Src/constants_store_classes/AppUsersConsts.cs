using System.Text.RegularExpressions;

namespace vokimi_api.Src.constants_store_classes
{
    public class AppUsersConsts
    {
        public const int
            MinPasswordLength = 8,
            MaxPasswordLength = 30;
        public static readonly Regex UsernameRegex = new(@"^[a-zA-Z0-9_.,><~^А-Яа-яЁё]*$");
        public const int 
            MinUsernameLength = 4, 
            MaxUsernameLength = 30;
    }
}
