using vokimi_api.Endpoints.pages;

namespace vokimi_api.EndpointsMappers.pages
{
    public static class TestTakingPageEndpointsMapper
    {
        public static void MapAll(WebApplication app) {
            app.MapGet("/testTaking/loadTestTakingData/{testId}", TestTakingPageEndpoints.LoadTestTakingData);
            app.MapPost("/testTaking/generalTestTakenRequest", TestTakingPageEndpoints.GeneralTestTakenRequest);

        }
    }
}
