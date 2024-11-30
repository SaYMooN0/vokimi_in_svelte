using vokimi_api.Endpoints.pages;

namespace vokimi_api.EndpointsMappers.pages
{
    internal class ProfileEditingEndpointsMapper
    {
        internal static void MapAll(WebApplication app) {
            app.MapGet("/profileEditing/getEditProfileData", ProfileEditingEndpoints.GetProfileData);
            app.MapPost("/profileEditing/updateUsername", ProfileEditingEndpoints.UpdateUsername);
        }
    }
}
