using Amazon.S3.Model;
using Amazon.S3;
using vokimi_api.Src;
using System.Net;
using vokimi_api.Helpers;
using vokimi_api.Src.constants_store_classes;
using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Services
{
    public class VokimiStorageService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;


        public VokimiStorageService(IAmazonS3 s3Client, string bucketName) {
            _s3Client = s3Client;
            _bucketName = bucketName;
        }
        private readonly Err
                             fileUploadingErr = new Err("Failed to upload the file"),
                             serverErr = new Err("Server error. Please try again later");
        private async Task<PutObjectResponse> PutObjectIntoStorage(string objKey, Stream fileStream) {
            PutObjectRequest putRequest = new() {
                BucketName = _bucketName,
                Key = objKey,
                InputStream = fileStream
            };

            return await _s3Client.PutObjectAsync(putRequest);
        }
        public async Task<IResult?> GetObjectFromStorage(string fileKey) {
            GetObjectRequest request = new() {
                BucketName = _bucketName,
                Key = fileKey
            };

            try {
                using (var response = await _s3Client.GetObjectAsync(request)) {
                    using (var responseStream = response.ResponseStream) {
                        MemoryStream memoryStream = new();
                        await responseStream.CopyToAsync(memoryStream);
                        memoryStream.Seek(0, SeekOrigin.Begin);
                        return Results.File(memoryStream, response.Headers.ContentType, response.Key);
                    }
                }
            } catch {
                return null;
            }
        }
        public async Task<Err> ClearUnusedObjectsInFolder(string folder, string? reservedKey = null) =>
            string.IsNullOrEmpty(reservedKey) ?
            await ClearUnusedObjectsInFolder(folder, Array.Empty<string>()) :
            await ClearUnusedObjectsInFolder(folder, [reservedKey]);
        public async Task<Err> ClearUnusedObjectsInFolder(string folder, IEnumerable<string> reservedKeys) {
            try {
                var listRequest = new ListObjectsV2Request {
                    BucketName = _bucketName,
                    Prefix = folder
                };

                ListObjectsV2Response listResponse;
                do {
                    listResponse = await _s3Client.ListObjectsV2Async(listRequest);

                    List<KeyVersion> objectsToDelete;

                    if (!folder.EndsWith('/')) {
                        folder = folder + "/";
                    }
                    var objectsInFolderOnly = listResponse.S3Objects
                        .Where(
                            o => o.Key == folder
                            || !o.Key.Substring(folder.Length).Contains("/")
                            //checking if object is in the current folder and not subfolders
                        );

                    if (reservedKeys is null || reservedKeys.Count() == 0) {
                        objectsToDelete = objectsInFolderOnly
                            .Select(o => new KeyVersion { Key = o.Key })
                            .ToList();
                    } else {
                        HashSet<string> keysToSave = reservedKeys.ToHashSet();
                        objectsToDelete = objectsInFolderOnly
                            .Where(o => !keysToSave.Contains(o.Key))
                            .Select(o => new KeyVersion { Key = o.Key })
                            .ToList();
                    }

                    if (objectsToDelete.Any()) {
                        var deleteRequest = new DeleteObjectsRequest {
                            BucketName = _bucketName,
                            Objects = objectsToDelete
                        };

                        var deleteResponse = await _s3Client.DeleteObjectsAsync(deleteRequest);

                        if (deleteResponse.HttpStatusCode != System.Net.HttpStatusCode.OK) {
                            return new Err("Failed to delete some unused images");
                        }
                    }

                    listRequest.ContinuationToken = listResponse.NextContinuationToken;
                } while (listResponse.IsTruncated);
            } catch {
                return serverErr;
            }
            return Err.None;
        }

        public async Task<Err> DeleteFiles(IEnumerable<string> keys) {
            try {
                var keysToDelete = keys.Select(key => new KeyVersion { Key = key }).ToList();

                if (keysToDelete.Any()) {
                    var deleteRequest = new DeleteObjectsRequest {
                        BucketName = _bucketName,
                        Objects = keysToDelete
                    };

                    var deleteResponse = await _s3Client.DeleteObjectsAsync(deleteRequest);

                    if (deleteResponse.HttpStatusCode != System.Net.HttpStatusCode.OK) {
                        return new Err("Failed to delete some files");
                    }
                }
                return Err.None;
            } catch {
                return new Err("Server error. Please try again later");
            }
        }
        public async Task<Err> CopyImageFile(string oldFileKey, string newFileKey) {
            try {
                var copyRequest = new CopyObjectRequest {
                    SourceBucket = _bucketName,
                    SourceKey = oldFileKey,
                    DestinationBucket = _bucketName,
                    DestinationKey = newFileKey
                };

                CopyObjectResponse copyResponse = await _s3Client.CopyObjectAsync(copyRequest);

                if (copyResponse.HttpStatusCode == System.Net.HttpStatusCode.OK) {
                    return Err.None;
                } else {
                    return fileUploadingErr;
                }
            } catch {
                return serverErr;
            }
        }
        public async Task<string?> SaveImgToStorage(
            string key,
            IFormFile file
        ) {
            if (file is null) {
                return null;
            }
            if (file.Length > ImgOperationsConsts.MaxImageSizeInBytes) {
                return null;
            }
            try {
                var stream = file.OpenReadStream();
                PutObjectResponse response = await PutObjectIntoStorage(key, stream);

                if (response.HttpStatusCode == HttpStatusCode.OK) {
                    return key;
                } else { return null; }

            } catch {
                return null;
            }
        }
        public async Task<IResult> IResultSaveImgToStorage(
            string key,
            IFormFile file
        ) => string.IsNullOrEmpty(await SaveImgToStorage(key, file)) ?
                ResultsHelper.BadRequestWithErr("An error occurred during file saving. Please try again later") :
                ResultsHelper.OkResultWithImgPath(key);
        public async Task ClearUnusedQuestionImages(
            DraftGeneralTestQuestionId questionId,
            DraftTestId testId,
            string? questionImagePath,
            List<string> answerImgs
        ) {
            string questionImgPref = ImgOperationsHelper.DraftGeneralTestQuestionsFolder(testId, questionId);
            await ClearUnusedObjectsInFolder(questionImgPref, questionImagePath);
            string answerImgPref = ImgOperationsHelper.DraftGeneralTestAnswersFolder(testId, questionId);
            await ClearUnusedObjectsInFolder(answerImgPref, answerImgs);
        }
    }
}
