using vokimi_api.Endpoints;

namespace vokimi_api.EndpointsMappers
{
    public class AuthEndpointsMapper
    {
        public static void MapAll(WebApplication app) {
            app.MapGet("/pingauth", AuthEndpoints.PingAuth);
            app.MapGet("/getUsernameWithProfilePicture", AuthEndpoints.GetUsernameWithProfilePicture);
            app.MapPost("/signup", AuthEndpoints.Signup);
            app.MapPost("/confirmRegistration", AuthEndpoints.ConfirmRegistration);
            app.MapPost("/login", AuthEndpoints.Login);
            app.MapPost("/logout", AuthEndpoints.Logout);
        }
    }
}
