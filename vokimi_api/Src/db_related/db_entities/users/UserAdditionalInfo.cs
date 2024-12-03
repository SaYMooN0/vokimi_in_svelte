using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.enums;

namespace vokimi_api.Src.db_related.db_entities.users
{
    public class UserAdditionalInfo
    {
        public UserAdditionalInfoId Id { get; private set; }
        public string RealName { get; private set; } = string.Empty;
        public DateOnly RegistrationDate { get; init; }
        public DateOnly? BirthDate { get; private set; }
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
        public void UpdateRealName(string newRealName) {
            RealName = newRealName;
        }
        public void UpdateBirthDate(DateOnly? newBirthDate) {
            BirthDate = newBirthDate;
        }
    }

    public class UserAdditionalInfoLinks
    {
        public string? Telegram { get; set; }
        public string? YouTube { get; set; }
        public string? Facebook { get; set; }
        public string? X { get; set; }
        public string? Instagram { get; set; }
        public string? GitHub { get; set; }
        public string? Other1 { get; set; }
        public string? Other2 { get; set; }
        public Dictionary<string, string?> ToDictionary() => new Dictionary<string, string?> {
            ["Telegram"] = Telegram,
            ["YouTube"] = YouTube,
            ["Facebook"] = Facebook,
            ["X"] = X,
            ["Instagram"] = Instagram,
            ["GitHub"] = GitHub,
            ["Other1"] = Other1,
            ["Other2"] = Other2
        };
        public void UpdateFromDictionary(IReadOnlyDictionary<string, string?> dictionary) {
            Telegram = dictionary.GetValueOrDefault("Telegram", defaultValue: null);
            YouTube = dictionary.GetValueOrDefault("YouTube", defaultValue: null);
            throw new NotImplementedException();
        }
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
