using vokimi_api.Endpoints.pages;

namespace vokimi_api.EndpointsMappers.pages
{
    public static class ViewTestPageEndpointsMapper
    {
        public static void MapAll(WebApplication app) {
            app.MapGet("/viewTest/checkTestViewPermission/{testId}", ViewTestPageEndpoints.CheckTestViewPermission);
            app.MapGet("/viewTest/getBasicTestInfo/{testId}", ViewTestPageEndpoints.GetBasicTestInfo);
        }
    }
}
