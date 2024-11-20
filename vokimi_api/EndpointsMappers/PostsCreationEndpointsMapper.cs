using vokimi_api.Endpoints;

namespace vokimi_api.EndpointsMappers
{
    internal static class PostsCreationEndpointsMapper
    {
        internal static void MapAll(WebApplication app) {
            app.MapGet("/postsCreation/getTestTakenPostCreationData/{testId}/{receivedResultId}",
                PostsCreationEndpoints.GetTestTakenPostCreationData);
        }
    }
}
