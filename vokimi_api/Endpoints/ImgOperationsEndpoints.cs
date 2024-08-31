using vokimi_api.Services;

namespace vokimi_api.Endpoints
{
    public static class ImgOperationsEndpoints
    {
        public static async Task<IResult> GetImgFromStorage(string fileKey, VokimiStorageService storageService) =>
            (await storageService.GetImgFromStorage(fileKey)) ?? Results.Problem("Unable to get image");
    }
}
