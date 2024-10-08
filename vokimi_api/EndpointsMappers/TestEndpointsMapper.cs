using vokimi_api.Endpoints.tests_operations;

namespace vokimi_api.EndpointsMappers
{
    public class TestEndpointsMapper
    {
        public static void MapAll(WebApplication app) {
            app.MapGet("/tests/getUserDraftTestsBriefInfo", TestEndpoints.GetUserDraftTestsBriefInfo);
            app.MapGet("/tests/getUserPublieshedTestsBriefInfo", TestEndpoints.GetUserPublishedTestsBriefInfo);
            app.MapPost("/tests/getDraftTestOverviewInfo", TestEndpoints.GetDraftTestOverviewInfo);
        }
    }
}
