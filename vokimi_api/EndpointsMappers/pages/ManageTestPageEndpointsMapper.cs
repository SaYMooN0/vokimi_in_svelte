using vokimi_api.Endpoints.pages;
using vokimi_api.Endpoints.pages.manage_test;

namespace vokimi_api.EndpointsMappers.pages
{
    internal static class ManageTestPageEndpointsMapper
    {
        internal static void MapAll(WebApplication app) {
            app.MapGet("/manageTest/overall/checkTestAccess/{testIdString}", ManageTestOverallEndpoints.CheckUserAccessToPage);
            app.MapGet("/manageTest/overall/getBasePageInfo/{testIdString}", ManageTestOverallEndpoints.GetBasePageInfo);
        }
    }
}
