using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.db_related.db_entities.users
{
    public record class PasswordUpdateRequest
    {
        public PasswordUpdateRequestId Id { get; init; }
        public LoginInfoId LoginInfoId { get; init; }
        public string ConfirmationCode { get; private set; }
        public void UpdateConfirmationCode() {
            ConfirmationCode = Guid.NewGuid().ToString();
        }
        public static PasswordUpdateRequest CreateNew(LoginInfoId loginInfoId) => new() {
            Id = new(),
            LoginInfoId = loginInfoId,
            ConfirmationCode = Guid.NewGuid().ToString(),
        };
    }
}
