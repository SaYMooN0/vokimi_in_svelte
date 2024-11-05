using vokimi_api.Endpoints;

namespace vokimi_api.EndpointsMappers.tests_related
{
    internal static class TestTagsEndpointsMapper
    {
        internal static void MapAll(WebApplication app) {
            app.MapGet("/tags/getDraftTestTagsData/{testId}", TestTagsEndpoints.GetDraftTestTagsData);
            app.MapGet("/tags/searchTags/{*tagToSearch}", TestTagsEndpoints.SearchTags);
            app.MapPost("/tags/updateDraftTestTags/{testId}", TestTagsEndpoints.UpdateDraftTestTags);
        }
    }

}
