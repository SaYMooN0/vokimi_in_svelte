using vokimi_api.Endpoints.pages.manage_test;

namespace vokimi_api.EndpointsMappers.pages
{
    internal static class ManageTestPageEndpointsMapper
    {
        internal static void MapAll(WebApplication app) {
            app.MapGet("/manageTest/overall/checkTestAccess/{testIdString}", ManageTestOverallEndpoints.CheckUserAccessToPage);
            app.MapGet("/manageTest/overall/tabData/{testIdString}", ManageTestOverallEndpoints.GetTabData);

            app.MapGet("/manageTest/tags/tabData/{testIdString}", ManageTestTagsEndpoints.GetTabData);
            app.MapPost("/manageTest/tags/setTestTags/{testIdString}", ManageTestTagsEndpoints.UpdateTestTags);
            app.MapPost("/manageTest/tags/enableTestTagsSuggestions/{testIdString}", ManageTestTagsEndpoints.EnableTestTagsSuggestions);
            app.MapPost("/manageTest/tags/disableTestTagsSuggestions/{testIdString}", ManageTestTagsEndpoints.DisableTestTagsSuggestions);
            app.MapPost("/manageTest/tags/acceptSuggestedTag", ManageTestTagsEndpoints.AcceptSuggestedTag);
            app.MapPost("/manageTest/tags/declineSuggestedTag", ManageTestTagsEndpoints.DeclineSuggestedTag);
            app.MapPost("/manageTest/tags/banSuggestedTag", ManageTestTagsEndpoints.BanSuggestedTag);

            app.MapGet("/manageTest/conclusion/tabData/{testIdString}", ManageTestConclusionEndpoints.GetTabData);
            app.MapPost("/manageTest/conclusion/enableTestFeedback/{testIdString}", ManageTestConclusionEndpoints.EnableTestFeedback);
            app.MapPost("/manageTest/conclusion/disableTestFeedback/{testIdString}", ManageTestConclusionEndpoints.DisableTestFeedback);

            app.MapGet("/manageTest/statistics/tabData/{testIdString}", ManageTestStatisticsEndpoints.GetTabData);


        }
    }
}
  