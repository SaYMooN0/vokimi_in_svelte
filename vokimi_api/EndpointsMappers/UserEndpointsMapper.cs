using vokimi_api.Endpoints;

namespace vokimi_api.EndpointsMappers
{
    public static class UserEndpointsMapper
    {
        public static void MapAll(WebApplication app) {
            app.MapGet("/users/doesUserExist/{userId}", UserEndpoints.DoesUserExist);
        }
    }
}
