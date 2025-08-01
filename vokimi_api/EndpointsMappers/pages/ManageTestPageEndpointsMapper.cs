﻿using vokimi_api.Endpoints.pages.manage_test;

namespace vokimi_api.EndpointsMappers.pages
{
    internal static class ManageTestPageEndpointsMapper
    {
        internal static void MapAll(WebApplication app) {
            app.MapGet("/manageTest/overall/checkTestAccess/{testIdString}", ManageTestOverallEndpoints.CheckUserAccessToPage);
            app.MapGet("/manageTest/overall/tabData/{testIdString}", ManageTestOverallEndpoints.GetTabData);
            app.MapPost("/manageTest/overall/changeTestPrivacy/{testIdString}", ManageTestOverallEndpoints.ChangeTestPrivacy);
            app.MapPost("/manageTest/overall/changeTestDescription/{testIdString}", ManageTestOverallEndpoints.ChangeTestDescription);

            app.MapGet("/manageTest/tags/tabData/{testIdString}", ManageTestTagsEndpoints.GetTabData);
            app.MapPost("/manageTest/tags/setTestTags/{testIdString}", ManageTestTagsEndpoints.UpdateTestTags);
            app.MapPost("/manageTest/tags/enableTestTagsSuggestions/{testIdString}", ManageTestTagsEndpoints.EnableTestTagsSuggestions);
            app.MapPost("/manageTest/tags/disableTestTagsSuggestions/{testIdString}", ManageTestTagsEndpoints.DisableTestTagsSuggestions);
            app.MapPost("/manageTest/tags/acceptSuggestedTag", ManageTestTagsEndpoints.AcceptSuggestedTag);
            app.MapPost("/manageTest/tags/declineSuggestedTag", ManageTestTagsEndpoints.DeclineSuggestedTag);
            app.MapPost("/manageTest/tags/banSuggestedTag", ManageTestTagsEndpoints.BanSuggestedTag);

            app.MapGet("/manageTest/conclusion/tabData/{testIdString}", ManageTestConclusionEndpoints.GetTabData);
            app.MapDelete("/manageTest/conclusion/deleteConclusion/{testIdString}", ManageTestConclusionEndpoints.DeleteConclusion);
            app.MapPost("/manageTest/conclusion/createConclusionForTest/{testIdString}", ManageTestConclusionEndpoints.CreateConclusionForTest);
            app.MapPost("/manageTest/conclusion/updateTestConlusion", ManageTestConclusionEndpoints.UpdateConclusionForTest);
            app.MapPost("/manageTest/conclusion/enableTestFeedback/{testIdString}", ManageTestConclusionEndpoints.EnableTestFeedback);
            app.MapPost("/manageTest/conclusion/disableTestFeedback/{testIdString}", ManageTestConclusionEndpoints.DisableTestFeedback);
            app.MapGet("/manageTest/conclusion/feedbackRecords/{testIdString}", ManageTestConclusionEndpoints.GetTestFeedbackRecords);
            app.MapPost("/manageTest/conclusion/filteredFeedbackRecords/{testIdString}", ManageTestConclusionEndpoints.GetTestFeedbackFilteredRecords);

            app.MapGet("/manageTest/statistics/tabData/{testIdString}", ManageTestStatisticsEndpoints.GetTabData);


        }
    }
}
