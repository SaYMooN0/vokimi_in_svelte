using vokimi_api.Endpoints.pages;

namespace vokimi_api.EndpointsMappers.pages
{
    public static class UserPageEndpointsMapper
    {
        public static void MapAll(WebApplication app)
        {
            app.MapGet("/userPage/doesUserExist/{userId}", UserEndpoints.DoesUserExist);
            app.MapGet("/userPage/getUserPageTopInfoData/{userId}", UserEndpoints.GetUserPageTopInfoData);
            app.MapGet("/userPage/getUserPageAdditionalInfo/{userId}", UserEndpoints.GetUserPageAdditionalInfoData);
        }
    }
}
