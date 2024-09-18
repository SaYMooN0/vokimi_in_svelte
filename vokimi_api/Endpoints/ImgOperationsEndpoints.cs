using Amazon.S3.Model;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using System.Net;
using vokimi_api.Helpers;
using vokimi_api.Services;
using vokimi_api.Src.constants_store_classes;
using vokimi_api.Src.db_related.db_entities.draft_tests.draft_tests_shared;
using vokimi_api.Src.db_related;
using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Endpoints
{
    public static class ImgOperationsEndpoints
    {
        public static async Task<IResult> GetImgFromStorage(string fileKey, VokimiStorageService storageService) =>
            (await storageService.GetImgFromStorage(fileKey)) ?? Results.Problem("Unable to get image");

        public static async Task<IResult> UpdateDraftTestQuestionCover(string testId,
                                                                       IFormFile file,
                                                                       IDbContextFactory<AppDbContext> dbFactory,
                                                                       VokimiStorageService vokimiStorage) {
            DraftTestId id;
            if (Guid.TryParse(testId, out Guid testGuid)) {
                id = new(testGuid);
            } else {
                return ResultsHelper.BadRequestWithErr("Unable to update cover. Please refresh the page.");
            }
            using (var db = dbFactory.CreateDbContext()) {
                BaseDraftTest? test = db.DraftTestsSharedInfo
                    .Include(t => t.MainInfo)
                    .FirstOrDefault(t => t.Id == id);

                if (test is null) {
                    return ResultsHelper.BadRequestUnknownTest();
                }
                string extension = ImgOperationsHelper.ExtractFileExtension(file);
                string key = $"{ImgOperationsConsts.DraftTestCoversFolder}/{testId}{extension}";

                using (var transaction = await db.Database.BeginTransactionAsync()) {
                    try {
                        string? savedKey = await ImgOperationsHelper.SaveImgToStorage(key, file, vokimiStorage);
                        if (savedKey is null) {
                            throw new Exception();
                        }
                        test.MainInfo.UpdateCoverImage(savedKey);
                        db.SaveChanges();
                        await transaction.CommitAsync();
                        return ResultsHelper.OkResultWithImgPath(key);
                    } catch {
                        await transaction.RollbackAsync();
                        return ResultsHelper.BadRequestServerError();
                    }
                }
            }
        }
        public static async Task<IResult> SaveDraftGeneralTestAnswerImage(string questionId,
                                                                          IFormFile file,
                                                                          VokimiStorageService storageService) {
            DraftGeneralTestQuestionId qId;
            if (Guid.TryParse(questionId, out Guid guid)) {
                qId = new(guid);
            } else {
                return ResultsHelper.BadRequestSaveChangesTryAgain();
            }
            string key = $"{ImgOperationsConsts.DraftGeneralTestAnswersFolder}/" +
                         $"{qId.Value.ToString()}" +
                         $"/{Guid.NewGuid()}{ImgOperationsHelper.ExtractFileExtension(file)}";
            return await ImgOperationsHelper.IResultSaveImgToStorage(key, file, storageService);
        }

        public static async Task<IResult> SaveDraftGeneralTestQuestionImage(string questionId,
                                                                           IFormFile file,
                                                                           VokimiStorageService storageService) {
            DraftGeneralTestQuestionId qId;
            if (Guid.TryParse(questionId, out Guid guid)) {
                qId = new(guid);
            } else {
                return ResultsHelper.BadRequestSaveChangesTryAgain();
            }
            string key = $"{ImgOperationsConsts.DraftGeneralTestQuestionsFolder}/" +
                         $"{qId.Value.ToString()}" +
                         $"/{Guid.NewGuid()}{ImgOperationsHelper.ExtractFileExtension(file)}";
            return await ImgOperationsHelper.IResultSaveImgToStorage(key, file, storageService);
        }
        public static async Task<IResult> SaveDraftGeneralTestResultImage(string resultId,
                                                                          IFormFile file,
                                                                          VokimiStorageService storageService) {
            DraftGeneralTestResultId rId;
            if (Guid.TryParse(resultId, out Guid guid)) {
                rId = new(guid);
            } else {
                return ResultsHelper.BadRequestSaveChangesTryAgain();
            }
            string key = $"{ImgOperationsConsts.DraftGeneralTestResultsFolder}/" +
                         $"{rId.Value.ToString()}" +
                         $"/{Guid.NewGuid()}{ImgOperationsHelper.ExtractFileExtension(file)}";
            return await ImgOperationsHelper.IResultSaveImgToStorage(key, file, storageService);
        }

    }
}
