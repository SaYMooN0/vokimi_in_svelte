using vokimi_api.Endpoints.pages;

namespace vokimi_api.EndpointsMappers.pages
{
    internal class ProfileEditingEndpointsMapper
    {
        internal static void MapAll(WebApplication app) {
            app.MapGet("/profileEditing/getEditProfileData", ProfileEditingEndpoints.GetProfileData);
            app.MapPost("/profileEditing/updateUsername", ProfileEditingEndpoints.UpdateUsername);
            app.MapPost("/profileEditing/updateBannerColor", ProfileEditingEndpoints.UpdateBannerColor);
            app.MapPost("/profileEditing/updateRealName", ProfileEditingEndpoints.UpdateRealName);
            app.MapPost("/profileEditing/updateBirthdate", ProfileEditingEndpoints.UpdateBirthdate);
            app.MapPost("/profileEditing/updateAboutMe", ProfileEditingEndpoints.UpdateAboutMe);
        }
    }
}
