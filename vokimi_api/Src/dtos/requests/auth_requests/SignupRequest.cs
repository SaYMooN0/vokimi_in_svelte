namespace vokimi_api.Src.dtos.requests.auth_requests
{
    public record SignupRequest(string Email, string Password, string Username, string FrontendBaseUrl);
}
