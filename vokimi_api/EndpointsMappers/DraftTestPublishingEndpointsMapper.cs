using vokimi_api.Endpoints.tests_operations;

namespace vokimi_api.EndpointsMappers
{
    public class DraftTestPublishingEndpointsMapper
    {
        public static void MapAll(WebApplication app) {

            app.MapGet("/testCreation/checkDraftTestForPublishingErrors/{testId}",
                DraftTestPublishingEndpoints.CheckDraftTestForPublishingProblems);
            app.MapPost("/testCreation/publishTest/{testId}",
                DraftTestPublishingEndpoints.PublishDraftTest);
        }
    }
}
