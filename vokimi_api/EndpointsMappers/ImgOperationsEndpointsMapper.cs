using vokimi_api.Endpoints;

namespace vokimi_api.EndpointsMappers
{
    public static class ImgOperationsEndpointsMapper
    {
        public static void MapAll(WebApplication app) {
            app.MapGet("/vokimiimgs/{*fileKey}", ImgOperationsEndpoints.GetImgFromStorage);
            //rewrite to check for creator
            app.MapPost("/testCreation/updateDraftTestQuestionCover/{testId}", ImgOperationsEndpoints.UpdateDraftTestQuestionCover).DisableAntiforgery();
            app.MapPost("/saveimg/saveDraftGeneralTestAnswerImage/{questionId}", ImgOperationsEndpoints.SaveDraftGeneralTestAnswerImage).DisableAntiforgery();
            app.MapPost("/saveimg/saveDraftGeneralTestQuestionImage/{questionId}", ImgOperationsEndpoints.SaveDraftGeneralTestQuestionImage).DisableAntiforgery();
            app.MapPost("/saveimg/saveDraftGeneralTestResultImage/{resultId}", ImgOperationsEndpoints.SaveDraftGeneralTestResultImage).DisableAntiforgery();
            app.MapPost("/saveimg/saveTestConclusionImage/{conclusionId}", ImgOperationsEndpoints.SaveTestConclusionImage).DisableAntiforgery();
        }
    }

}
