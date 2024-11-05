using vokimi_api.Endpoints;

namespace vokimi_api.EndpointsMappers
{
    internal static class PostsCreationEndpointsMapper
    {
        internal static void MapAll(WebApplication app) {
            app.MapGet("/api/postsCreation/getTestTakenPostCreationData/${testId}/${receivedResultId}",
                PostsCreationEndpoints.GetTestTakenPostCreationData);
        }
    }
}
