using System.Text.RegularExpressions;
using vokimi_api.Src.constants_store_classes;
using vokimi_api.Src.db_related.db_entities.user_page;
using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.db_related.db_entities.users
{
    public class UserPageSettings
    {
        public UserPageSettingsId Id { get; init; }
        public string AboutMe { get; private set; } = string.Empty;
        public string BannerColor { get; private set; }
        //icons or chosen badges
        public UserPagePrivacySettings PrivacySettings { get; private set; } = UserPagePrivacySettings.Default;
        public Err ChangeUserPageColor(string color) {
            if (string.IsNullOrWhiteSpace(color) || !Regex.IsMatch(color, "^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$")) {
                return new Err("Invalid color format. Please use a valid hex code.");
            }
            BannerColor = color;
            return Err.None;
        }
        public static UserPageSettings CreateNew() => new() {
            Id = new(),
            AboutMe = string.Empty,
            BannerColor = BaseTestCreationConsts.DefaultAccentColor,
            PrivacySettings = UserPagePrivacySettings.Default,
        };
    }
}
