using Amazon.S3.Model;
using System.Net;
using vokimi_api.Services;
using vokimi_api.Src.constants_store_classes;

namespace vokimi_api.Helpers
{
    public class ImgOperationsHelper
    {
        public static string ExtractFileExtension(IFormFile file) => Path.GetExtension(file.FileName);
        public static async Task<IResult> IResultSaveImgToStorage(string key,
                                                                  IFormFile file,
                                                                  VokimiStorageService storageService) {
            if (file is null) {
                return ResultsHelper.BadRequestWithErr("File is not selected");
            }
            if (file.Length > ImgOperationsConsts.MaxImageSizeInBytes) {
                return ResultsHelper.BadRequestWithErr(
                    $"File is too big. Max allowed size: {ImgOperationsConsts.MaxImageSizeInMB}MB");
            }
            try {
                var stream = file.OpenReadStream();
                PutObjectResponse response = await storageService.PutObjectIntoStorage(key, stream);

                if (response.HttpStatusCode == HttpStatusCode.OK) {
                    return ResultsHelper.OkResultWithImgPath(key);
                } else { throw new Exception(); }

            } catch {
                return ResultsHelper.BadRequestWithErr(
                    "An error occurred during file saving. Please try again later");
            }
        }
        public static async Task<string?> SaveImgToStorage(string key,
                                                          IFormFile file,
                                                          VokimiStorageService storageService) {
            if (file is null) {
                return null;
            }
            if (file.Length > ImgOperationsConsts.MaxImageSizeInBytes) {
                return null;
            }
            try {
                var stream = file.OpenReadStream();
                PutObjectResponse response = await storageService.PutObjectIntoStorage(key, stream);

                if (response.HttpStatusCode == HttpStatusCode.OK) {
                    return key;
                } else { return null; }

            } catch {
                return null;
            }
        }
    }
}
