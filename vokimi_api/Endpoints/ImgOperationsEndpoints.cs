using Microsoft.EntityFrameworkCore;
using vokimi_api.Helpers;
using vokimi_api.Services;
using vokimi_api.Src.constants_store_classes;
using vokimi_api.Src.db_related.db_entities.draft_tests.draft_tests_shared;
using vokimi_api.Src.db_related;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.extension_classes;

namespace vokimi_api.Endpoints
{
    public static class ImgOperationsEndpoints
    {
        public static async Task<IResult> GetImgFromStorage(string fileKey, VokimiStorageService storageService) =>
            (await storageService.GetImgFromStorage(fileKey)) ?? Results.Problem("Unable to get image");

        public static async Task<IResult> UpdateDraftTestCover(
            string testId,
            IFormFile file,
            IDbContextFactory<AppDbContext> dbFactory,
            VokimiStorageService vokimiStorage,
            HttpContext httpContext
        ) {
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
                if (!httpContext.IfAuthenticatedUserIdIsTestCreator(test)) {
                    return ResultsHelper.BadRequestNotCreator();
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
        public static async Task<IResult> SaveDraftGeneralTestAnswerImage(
            string questionId,
            IFormFile file,
            VokimiStorageService storageService,
            HttpContext httpContext,
            IDbContextFactory<AppDbContext> dbFactory
        ) {
            DraftGeneralTestQuestionId qId;
            if (Guid.TryParse(questionId, out Guid guid)) {
                qId = new(guid);
            } else {
                return ResultsHelper.BadRequestSaveChangesTryAgain();
            }
            using (var db = await dbFactory.CreateDbContextAsync()) {
                DraftTestId? testId = db.DraftGeneralTestQuestions
                    .FirstOrDefault(q => q.Id == qId)?.TestId ?? null;
                if (testId is null) {
                    return ResultsHelper.BadRequestWithErr("Unable to update answer image. Unknown question");
                }
                AppUserId? creatorId = db.DraftTestsSharedInfo
                    .FirstOrDefault(t => t.Id == testId)?.CreatorId ?? null;
                if (!httpContext.IfAuthenticatedUserIdEquals(creatorId)) {
                    return ResultsHelper.BadRequestNotCreator();
                }
            }
            string key = $"{ImgOperationsConsts.DraftGeneralTestAnswersFolder}/" +
                         $"{qId.Value.ToString()}" +
                         $"/{Guid.NewGuid()}{ImgOperationsHelper.ExtractFileExtension(file)}";
            return await ImgOperationsHelper.IResultSaveImgToStorage(key, file, storageService);
        }

        public static async Task<IResult> SaveDraftGeneralTestQuestionImage(
            string questionId,
            IFormFile file,
            VokimiStorageService storageService,
            HttpContext httpContext,
            IDbContextFactory<AppDbContext> dbFactory
        ) {
            DraftGeneralTestQuestionId qId;
            if (Guid.TryParse(questionId, out Guid guid)) {
                qId = new(guid);
            } else {
                return ResultsHelper.BadRequestSaveChangesTryAgain();
            }
            using (var db = await dbFactory.CreateDbContextAsync()) {
                DraftTestId? testId = db.DraftGeneralTestQuestions
                    .FirstOrDefault(q => q.Id == qId)?.TestId ?? null;
                if (testId is null) {
                    return ResultsHelper.BadRequestWithErr("Unable to update question image. Unknown question");
                }
                AppUserId? creatorId = db.DraftTestsSharedInfo
                    .FirstOrDefault(t => t.Id == testId)?.CreatorId ?? null;
                if (!httpContext.IfAuthenticatedUserIdEquals(creatorId)) {
                    return ResultsHelper.BadRequestNotCreator();
                }

            }
            string key = $"{ImgOperationsConsts.DraftGeneralTestQuestionsFolder}/" +
                         $"{qId.Value.ToString()}" +
                         $"/{Guid.NewGuid()}{ImgOperationsHelper.ExtractFileExtension(file)}";
            return await ImgOperationsHelper.IResultSaveImgToStorage(key, file, storageService);
        }
        public static async Task<IResult> SaveDraftGeneralTestResultImage(
            string resultId,
            IFormFile file,
            VokimiStorageService storageService,
            HttpContext httpContext,
            IDbContextFactory<AppDbContext> dbFactory
        ) {
            DraftGeneralTestResultId rId;
            if (Guid.TryParse(resultId, out Guid guid)) {
                rId = new(guid);
            } else {
                return ResultsHelper.BadRequestSaveChangesTryAgain();
            }
            using (var db = await dbFactory.CreateDbContextAsync()) {
                DraftTestId? testId = db.DraftGeneralTestResults
                    .FirstOrDefault(r => r.Id == rId)?.TestId ?? null;
                if (testId is null) {
                    return ResultsHelper.BadRequestWithErr("Unable to update result image. Unknown result");
                }
                AppUserId? creatorId = db.DraftTestsSharedInfo
                    .FirstOrDefault(t => t.Id == testId)?.CreatorId ?? null;
                if (!httpContext.IfAuthenticatedUserIdEquals(creatorId)) {
                    return ResultsHelper.BadRequestNotCreator();
                }
            }
            string key = $"{ImgOperationsConsts.DraftGeneralTestResultsFolder}/" +
                         $"{rId.Value.ToString()}" +
                         $"/{Guid.NewGuid()}{ImgOperationsHelper.ExtractFileExtension(file)}";
            return await ImgOperationsHelper.IResultSaveImgToStorage(key, file, storageService);
        }
        public static async Task<IResult> SaveDraftTestConclusionImage(
            string testId,
            IFormFile file,
            VokimiStorageService storageService,
            HttpContext httpContext,
            IDbContextFactory<AppDbContext> dbFactory
        ) {
            DraftTestId tId;
            if (Guid.TryParse(testId, out Guid guid)) {
                tId = new(guid);
            } else {
                return ResultsHelper.BadRequestSaveChangesTryAgain();
            }
            using (var db = await dbFactory.CreateDbContextAsync()) {
                BaseDraftTest? test = db.DraftTestsSharedInfo.FirstOrDefault(t => t.Id == tId);
                if (test is null) {
                    return ResultsHelper.BadRequestWithErr("Unable to update conclusion image. Test not found");
                }
                if (httpContext.IfAuthenticatedUserIdIsTestCreator(test)) {
                    return ResultsHelper.BadRequestNotCreator();
                }
            }
            string key = $"{ImgOperationsConsts.TestConclusionsFolder}/" +
                   $"{tId.Value.ToString()}" +
                   $"/{Guid.NewGuid()}{ImgOperationsHelper.ExtractFileExtension(file)}";
            return await ImgOperationsHelper.IResultSaveImgToStorage(key, file, storageService);

        }

    }
}
