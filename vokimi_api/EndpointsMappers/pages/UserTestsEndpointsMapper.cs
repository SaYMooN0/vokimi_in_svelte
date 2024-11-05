using vokimi_api.Endpoints.pages;

namespace vokimi_api.EndpointsMappers.pages
{
    internal class UserTestsEndpointsMapper
    {
        internal static void MapAll(WebApplication app)
        {
            app.MapGet("/userTests/getUserDraftTestsBriefInfo", UserTestsEndpoints.GetUserDraftTestsBriefInfo);
            app.MapGet("/userTests/getUserPublishedTestsBriefInfo", UserTestsEndpoints.GetUserPublishedTestsBriefInfo);
            app.MapGet("/userTests/getDraftTestOverviewInfo/{testId}", UserTestsEndpoints.GetDraftTestOverviewInfo);
            app.MapDelete("/userTests/deleteDraftTest/{testId}", UserTestsEndpoints.DeleteDraftTest);
        }
    }
}
