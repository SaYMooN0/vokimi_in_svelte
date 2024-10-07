using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.db_related.db_entities.users
{
    public class UserAdditionalInfo
    {
        public UserAdditionalInfoId Id { get; private set; }
        public string RealName { get; private set; }
        public DateTime RegistrationDate { get; init; }
        public DateTime? BirthDate { get; init; }
        public UserAdditionalInfoLinks Links { get; init; }
        public static UserAdditionalInfo CreateNew(DateTime registrationDate) =>
            new() {
                Id = new UserAdditionalInfoId(),
                RealName = string.Empty,
                RegistrationDate = registrationDate,
                BirthDate = null,
                Links = new()
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

    }
}
