using vokimi_api.Endpoints;

namespace vokimi_api.EndpointsMappers
{
    internal class TestCollectionsEndpointsMapper
    {
        internal static void MapAll(WebApplication app) {
            app.MapGet("/testCollections/getCollectionsInfoForTest/{testId}",
                TestCollectionsEndpoints.GetCollectionsInfoForTest);
            app.MapPost("/testCollections/testEntriesInCollectionsChanged",
                TestCollectionsEndpoints.HandleTestEntriesInCollectionsChanged);
            app.MapPost("/testCollections/createNewCollection",
                TestCollectionsEndpoints.CreateNewCollection);
        }
    }
}
