using Amazon.S3.Model;
using Amazon.S3;
using vokimi_api.Src;

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
        public async Task<PutObjectResponse> PutObjectIntoStorage(string objKey, Stream fileStream) {
            PutObjectRequest putRequest = new() {
                BucketName = _bucketName,
                Key = objKey,
                InputStream = fileStream
            };

            return await _s3Client.PutObjectAsync(putRequest);
        }
        public async Task<IResult?> GetImgFromStorage(string fileKey) {
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
        public async Task<Err> ClearUnusedImages(string prefix, IEnumerable<string>? reservedKeys = null) {
            try {
                var listRequest = new ListObjectsV2Request {
                    BucketName = _bucketName,
                    Prefix = prefix
                };

                ListObjectsV2Response listResponse;
                do {
                    listResponse = await _s3Client.ListObjectsV2Async(listRequest);

                    List<KeyVersion> objectsToDelete;

                    if (reservedKeys is null || reservedKeys.Count() == 0) {
                        objectsToDelete = listResponse.S3Objects
                            .Select(o => new KeyVersion { Key = o.Key })
                            .ToList();
                    } else {
                        objectsToDelete = listResponse.S3Objects
                            .Where(o => (reservedKeys == null || !reservedKeys.Contains(o.Key)))
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
    }
}
