using vokimi_api.Endpoints.tests_operations;

namespace vokimi_api.EndpointsMappers
{
    public class TestEndpointsMapper
    {
        public static void MapAll(WebApplication app) {
            app.MapGet("/tests/getUserDraftTestsBriefInfo", TestEndpoints.GetUserDraftTestsBriefInfo);
            app.MapGet("/tests/getUserPublishedTestsBriefInfo", TestEndpoints.GetUserPublishedTestsBriefInfo);
            app.MapGet("/tests/getDraftTestOverviewInfo/{testId}", TestEndpoints.GetDraftTestOverviewInfo);
            app.MapDelete("/tests/deleteDraftTest/{testId}", TestEndpoints.DeleteDraftTest);
        }
    }
}
