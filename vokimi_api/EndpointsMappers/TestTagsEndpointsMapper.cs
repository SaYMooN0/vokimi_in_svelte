using vokimi_api.Endpoints;

namespace vokimi_api.EndpointsMappers
{
    public static class TestTagsEndpointsMapper
    {
        public static void MapAll(WebApplication app) {
            app.MapGet("/tags/getDraftTestTagsData/{testId}", TestTagsEndpoints.GetDraftTestTagsData);
            app.MapGet("/tags/searchTags/{*tagToSearch}", TestTagsEndpoints.SearchTags);
            app.MapPost("/tags/updateDraftTestTags/{testId}", TestTagsEndpoints.UpdateDraftTestTags);
        }
    }

}
