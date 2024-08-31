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
        private async Task<PutObjectResponse> PutObjectIntoStorage(string objKey, Stream fileStream) {
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
    }
}
