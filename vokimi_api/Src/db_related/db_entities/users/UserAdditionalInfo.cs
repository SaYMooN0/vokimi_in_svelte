using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.enums;

namespace vokimi_api.Src.db_related.db_entities.users
{
    public class UserAdditionalInfo
    {
        public UserAdditionalInfoId Id { get; private set; }
        public string RealName { get; private set; }
        public DateOnly RegistrationDate { get; init; }
        public DateOnly? BirthDate { get; init; }
        public UserAdditionalInfoLinks Links { get; init; }
        public UserAdditionalInfoPrivacySettings PrivacySettings { get; init; }
        public static UserAdditionalInfo CreateNew(DateOnly registrationDate) =>
            new() {
                Id = new UserAdditionalInfoId(),
                RealName = string.Empty,
                RegistrationDate = registrationDate,
                BirthDate = null,
                Links = new(),
                PrivacySettings = UserAdditionalInfoPrivacySettings.Default
            };
    }
    public class UserAdditionalInfoLinks
    {
        public string? Telegram { get; set; }
        public string? YouTube { get; set; }
        public string? Facebook { get; set; }
        public string? X { get; set; }
        public string? Other1 { get; set; }
        public string? Other2 { get; set; }
        public Dictionary<string, string?> ToDictionary() => new Dictionary<string, string?> {
            ["Telegram"] = Telegram,
            ["YouTube"] = YouTube,
            ["Facebook"] = Facebook,
            ["X"] = X,
            ["Other1"] = Other1,
            ["Other2"] = Other2
        };
    }
    public class UserAdditionalInfoPrivacySettings
    {
        public PrivacyValues RealNamePrivacy { get; private set; }
        public PrivacyValues RegistrationDatePrivacy { get; private set; }
        public PrivacyValues BirthDatePrivacy { get; private set; }
        public PrivacyValues LinksPrivacy { get; private set; }
        public static UserAdditionalInfoPrivacySettings Default => new() {
            RealNamePrivacy = PrivacyValues.Anyone,
            RegistrationDatePrivacy = PrivacyValues.Anyone,
            BirthDatePrivacy = PrivacyValues.Anyone,
            LinksPrivacy = PrivacyValues.Anyone,
        };
    }
}
