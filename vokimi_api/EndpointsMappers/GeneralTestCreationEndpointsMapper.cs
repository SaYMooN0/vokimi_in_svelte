using vokimi_api.Endpoints.tests_operations.test_creation;

namespace vokimi_api.EndpointsMappers
{
    public static class GeneralTestCreationEndpointsMapper
    {
        public static void MapAll(WebApplication app) {
            //rewrite to check for creator
            app.MapGet("/testCreation/general/getGeneralDraftTestQuestionsData/{testId}", GeneralTestCreationEndpoints.GetGeneralDraftTestQuestionsData);
            app.MapPost("/testCreation/general/createGeneralTestQuestion", GeneralTestCreationEndpoints.CreateGeneralTestQuestion);
            app.MapGet("/testCreation/general/getDraftGeneralTestQuestionDataToEdit/{questionId}", GeneralTestCreationEndpoints.GetDraftGeneralTestQuestionDataToEdit);
            app.MapDelete("/testCreation/general/deleteGeneralDraftTestQuestion/{questionId}", GeneralTestCreationEndpoints.DeleteGeneralDraftTestQuestion);
            app.MapPost("/testCreation/general/saveChangesForDraftGeneralTestQuestion/{answersType}", GeneralTestCreationEndpoints.UpdateDraftGeneralTestQuestion);
            app.MapPost("/testCreation/general/moveQuestionUpInOrder/{questionId}", GeneralTestCreationEndpoints.MoveQuestionUpInOrder);
            app.MapPost("/testCreation/general/moveQuestionDownInOrder/{questionId}", GeneralTestCreationEndpoints.MoveQuestionDownInOrder);
            app.MapGet("/testCreation/general/getResultsIdNameDictionary/{testId}", GeneralTestCreationEndpoints.GetResultsIdNameDictionary);
            app.MapPost("/testCreation/general/createNewResult", GeneralTestCreationEndpoints.CreateNewResultForTest);
            app.MapGet("/testCreation/general/getGeneralDraftTestResultsData/{testId}", GeneralTestCreationEndpoints.GetDraftGeneralTestResultsData);
            app.MapGet("/testCreation/general/getDraftGeneralTestResultDataToEdit/{resultId}", GeneralTestCreationEndpoints.GetDraftGeneralTestResultDataToEdit);
            app.MapDelete("/testCreation/general/deleteDraftGeneralTestResult/{resultId}", GeneralTestCreationEndpoints.DeleteGeneralDraftTestResult);
            app.MapPost("/testCreation/general/saveChangesForDraftGeneralTestResult", GeneralTestCreationEndpoints.SaveChangesForDraftGeneralTestResult);
        }
    }

}
