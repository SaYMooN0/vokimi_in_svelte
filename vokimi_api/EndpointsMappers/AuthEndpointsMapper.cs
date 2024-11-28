using vokimi_api.Endpoints;

namespace vokimi_api.EndpointsMappers
{
    internal class AuthEndpointsMapper
    {
        internal static void MapAll(WebApplication app) {
            app.MapGet("/auth/ping", AuthEndpoints.PingAuth);
            app.MapGet("/getUsernameWithProfilePicture", AuthEndpoints.GetUsernameWithProfilePicture);
            app.MapPost("/signup", AuthEndpoints.Signup);
            app.MapPost("/confirmRegistration", AuthEndpoints.ConfirmRegistration);
            app.MapPost("/login", AuthEndpoints.Login);
            app.MapPost("/logout", AuthEndpoints.Logout);
            app.MapPost(
                "/auth/createPasswordUpdateRequest/{email}",
                AuthEndpoints.CreatePasswordUpdateRequest
            );
            app.MapPost(
                "/auth/confirmPasswordUpdateRequest",
                AuthEndpoints.SetNewPasswordRequest
            );
        }
    }
}
