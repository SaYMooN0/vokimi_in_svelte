namespace vokimi_api.Src.dtos.requests.auth
{
    public record ConfirmRegistrationRequest(string ConfirmationString, string UserId);
}
