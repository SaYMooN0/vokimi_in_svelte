using Amazon.S3.Model;
using Microsoft.AspNetCore.Components.Forms;
using System.Net;
using vokimi_api.Helpers;
using vokimi_api.Services;
using vokimi_api.Src.constants_store_classes;
using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Endpoints
{
    public static class ImgOperationsEndpoints
    {
        public static async Task<IResult> GetImgFromStorage(string fileKey, VokimiStorageService storageService) =>
            (await storageService.GetImgFromStorage(fileKey)) ?? Results.Problem("Unable to get image");
        public static async Task<IResult> SaveDraftGeneralTestAnswerImage(string questionId,
                                                                          IFormFile file,
                                                                          VokimiStorageService storageService) {
            DraftGeneralTestQuestionId qId;
            if (Guid.TryParse(questionId, out Guid guid)) {
                qId = new(guid);
            } else {
                return ResultsHelper.BadRequestWithErr(
                    "Server error. Save existing changes and try to refresh the page");
            }
            string key = $"{ImgOperationsConsts.DraftGeneralTestAnswersFolder}/" +
                         $"{qId.Value.ToString()}" +
                         $"/{Guid.NewGuid()}.{ImgOperationsHelper.ExtractFileExtension(file)}";
            return await ImgOperationsHelper.IResultSaveImgToStorage(key, file, storageService);
        }


    }
}
