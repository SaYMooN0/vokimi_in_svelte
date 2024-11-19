using vokimi_api.Endpoints;

namespace vokimi_api.EndpointsMappers
{
    internal class UserCollectionsEndpointsMapper
    {
        internal static void MapAll(WebApplication app) {
            app.MapGet("/api/userCollections/getCollectionsInfoForTest/${testId}",
                UserCollectionsEndpoints.GetCollectionsInfoForTest);
            app.MapPost("/api/userCollections/testEntriesInCollectionsChanged",
                UserCollectionsEndpoints.HandleTestEntriesInCollectionsChanged);  
            app.MapPost("/api/userCollections/createNewCollection",
                UserCollectionsEndpoints.CreateNewCollection);
        }
    }
}
