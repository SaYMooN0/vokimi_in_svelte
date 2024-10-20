using vokimi_api.Endpoints;

namespace vokimi_api.EndpointsMappers
{
    public static class ImgOperationsEndpointsMapper
    {
        public static void MapAll(WebApplication app) {
            app.MapGet("/vokimiimgs/{*fileKey}", ImgOperationsEndpoints.GetImgFromStorage);
            app.MapPost("/saveimg/updateDraftTestCover/{testId}", ImgOperationsEndpoints.UpdateDraftTestCover)
                .DisableAntiforgery();
            app.MapPost("/saveimg/saveDraftGeneralTestAnswerImage/{questionId}", ImgOperationsEndpoints.SaveDraftGeneralTestAnswerImage)
                .DisableAntiforgery();
            app.MapPost("/saveimg/saveDraftGeneralTestQuestionImage/{questionId}", ImgOperationsEndpoints.SaveDraftGeneralTestQuestionImage)
                .DisableAntiforgery();
            app.MapPost("/saveimg/saveDraftGeneralTestResultImage/{resultId}", ImgOperationsEndpoints.SaveDraftGeneralTestResultImage)
                .DisableAntiforgery();
            app.MapPost("/saveimg/saveDraftTestConclusionImage/{testId}", ImgOperationsEndpoints.SaveDraftTestConclusionImage)
                .DisableAntiforgery();
        }
    }

}
