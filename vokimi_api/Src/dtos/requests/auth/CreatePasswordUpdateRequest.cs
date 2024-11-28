using System.Net.Mail;
using vokimi_api.Helpers;

namespace vokimi_api.Src.dtos.requests.auth
{
    public record class CreatePasswordUpdateRequest(
        string Email,
        string FrontendBaseUrl
    )
    {
        public Err CheckForErr() {
            if (string.IsNullOrWhiteSpace(Email)) {
                return new Err("Please provide an email");
            }
            if (!MailAddress.TryCreate(Email, out var _)) {
                return new Err("Invalid email");
            }
            if (string.IsNullOrWhiteSpace(FrontendBaseUrl)) {
                return new Err("An error has occurred. Please try again later");
            }
            return Err.None;
        }
    }
}
