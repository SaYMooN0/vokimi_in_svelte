
using vokimi_api.Endpoints.pages.test_creation.general_template;

namespace vokimi_api.EndpointsMappers.pages.test_creation
{
    internal static class GeneralTestCreationEndpointsMapper
    {
        internal static void MapAll(WebApplication app) {

            //questions
            app.MapGet("/testCreation/general/getGeneralDraftTestQuestionsData/{testId}",
                GeneralTestQuestionCreationEndpoints.GetGeneralDraftTestQuestionsData);
            app.MapPost("/testCreation/general/createGeneralTestQuestion",
                GeneralTestQuestionCreationEndpoints.CreateGeneralTestQuestion);
            app.MapGet("/testCreation/general/getDraftGeneralTestQuestionDataToEdit/{questionId}",
                GeneralTestQuestionCreationEndpoints.GetDraftGeneralTestQuestionDataToEdit);
            app.MapDelete("/testCreation/general/deleteGeneralDraftTestQuestion/{questionId}",
                GeneralTestQuestionCreationEndpoints.DeleteDraftGeneralTestQuestion);
            app.MapPost("/testCreation/general/saveChangesForDraftGeneralTestQuestion/{answersType}",
                GeneralTestQuestionCreationEndpoints.UpdateDraftGeneralTestQuestion);
            app.MapPost("/testCreation/general/moveQuestionUpInOrder/{questionId}",
                GeneralTestQuestionCreationEndpoints.MoveQuestionUpInOrder);
            app.MapPost("/testCreation/general/moveQuestionDownInOrder/{questionId}",
                GeneralTestQuestionCreationEndpoints.MoveQuestionDownInOrder);
            
            //results
            app.MapGet("/testCreation/general/getResultsIdNameDictionary/{testId}",
                GeneralTestResultsCreationEndpoints.GetResultsIdNameDictionary);
            app.MapPost("/testCreation/general/createNewResult",
                GeneralTestResultsCreationEndpoints.CreateNewResultForTest);
            app.MapGet("/testCreation/general/getGeneralDraftTestResultsData/{testId}",
                GeneralTestResultsCreationEndpoints.GetDraftGeneralTestResultsData);
            app.MapGet("/testCreation/general/getDraftGeneralTestResultDataToEdit/{resultId}",
                GeneralTestResultsCreationEndpoints.GetDraftGeneralTestResultDataToEdit);
            app.MapDelete("/testCreation/general/deleteDraftGeneralTestResult/{resultId}",
                GeneralTestResultsCreationEndpoints.DeleteGeneralDraftTestResult);
            app.MapPost("/testCreation/general/saveChangesForDraftGeneralTestResult",
                GeneralTestResultsCreationEndpoints.SaveChangesForDraftGeneralTestResult);
        }
    }

}
