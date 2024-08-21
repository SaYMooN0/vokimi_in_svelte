namespace vokimi_api.Src.dtos.requests.auth_requests
{
    public record ConfirmRegistrationRequest(string ConfirmationString, string UserId);
}
