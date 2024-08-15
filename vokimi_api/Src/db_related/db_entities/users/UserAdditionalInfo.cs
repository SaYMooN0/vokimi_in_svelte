using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.db_related.db_entities.users
{
    public class UserAdditionalInfo
    {
        public UserAdditionalInfoId Id { get; private set; }
        public DateTime RegistrationDate { get; init; }
        public DateTime? BirthDate { get; init; }
        public static UserAdditionalInfo CreateNew(DateTime registrationDate) =>
            new()
            {
                Id = new UserAdditionalInfoId(),
                RegistrationDate = registrationDate,
                BirthDate = null
            };
    }
}
