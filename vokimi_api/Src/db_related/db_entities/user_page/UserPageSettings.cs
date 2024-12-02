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
        public static UserPageSettings CreateNew() => new() {
            Id = new(),
            AboutMe = string.Empty,
            BannerColor = SharedConsts.PrimaryColor,
            PrivacySettings = UserPagePrivacySettings.Default,
        };
        public Err UpdateBannerColor(string color) {
            if (!SharedConsts.HexColorRegex.IsMatch(color)) {
                return new Err("Invalid color format. Please use a valid hex code.");
            }
            BannerColor = color;
            return Err.None;
        }
        public void UpdateAboutMe(string newAboutMe) {
            AboutMe = newAboutMe;
        }
    }
}
