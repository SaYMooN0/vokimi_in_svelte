using vokimi_api.Endpoints;

namespace vokimi_api.EndpointsMappers.tests_related
{
    internal static class TestStylesEndpointsMapper
    {
        internal static void MapAll(WebApplication app) {
            app.MapGet("/testStyles/getDraftTestStylesData/{testId}", TestStylesEndpoints.GetDraftTestStylesData);
            app.MapGet("/testStyles/getDefaultStylesData", TestStylesEndpoints.GetDefaultStylesData);
            app.MapPost("/testStyles/updateDraftTestStyles/{testId}", TestStylesEndpoints.UpdateDraftTestStylesData);
        }
    }

}
