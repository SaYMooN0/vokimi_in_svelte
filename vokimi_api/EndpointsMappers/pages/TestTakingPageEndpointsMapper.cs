using vokimi_api.Endpoints.pages;

namespace vokimi_api.EndpointsMappers.pages
{
    internal static class TestTakingPageEndpointsMapper
    {
        internal static void MapAll(WebApplication app) {
            app.MapGet("/testTaking/loadTestTakingData/{testId}", TestTakingPageEndpoints.LoadTestTakingData);
            app.MapPost("/testTaking/generalTestTakenRequest", TestTakingPageEndpoints.GeneralTestTakenRequest);
            app.MapGet("/testTaking/getGeneralTestReceivedResultData/{resultId}",
                TestTakingPageEndpoints.GetGeneralTestReceivedResultData);

        }
    }
}
