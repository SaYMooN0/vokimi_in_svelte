
using vokimi_api.Endpoints.pages.test_creation;

namespace vokimi_api.EndpointsMappers.pages.test_creation
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
