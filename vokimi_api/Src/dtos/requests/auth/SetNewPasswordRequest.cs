using vokimi_api.Src.constants_store_classes;

namespace vokimi_api.Src.dtos.requests.auth
{
    public record class SetNewPasswordRequest(
        string updateRequestRecordId,
        string confirmationCode,
        string newPassword
    )
    {
        public Err CheckForErr() {
            int passwordLength = string.IsNullOrEmpty(newPassword) ? 0 : newPassword.Length;
            if (passwordLength < AppUsersConsts.MinPasswordLength
                || passwordLength > AppUsersConsts.MaxPasswordLength
            ) {
                return new($"Password must be between {AppUsersConsts.MinPasswordLength} and {AppUsersConsts.MaxPasswordLength} characters");
            }
            if (string.IsNullOrEmpty(confirmationCode) || string.IsNullOrEmpty(updateRequestRecordId)) {
                return new($"Invalid link");
            }
            return Err.None;
        }   
    }
}
