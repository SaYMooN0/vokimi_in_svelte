using vokimi_api.Endpoints.pages;

namespace vokimi_api.EndpointsMappers.pages
{
    internal static class ViewTestPageEndpointsMapper
    {
        internal static void MapAll(WebApplication app) {
            app.MapGet("/viewTest/checkTestViewPermission/{testId}", ViewTestPageEndpoints.CheckTestViewPermission);
            app.MapGet("/viewTest/getBasicTestInfo/{testId}", ViewTestPageEndpoints.GetBasicTestInfo);
        }
    }
}
