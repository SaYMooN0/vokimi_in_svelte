using Microsoft.EntityFrameworkCore;
using vokimi_api.Helpers;
using vokimi_api.Services;
using vokimi_api.Src.constants_store_classes;
using vokimi_api.Src.db_related.db_entities.draft_tests.draft_tests_shared;
using vokimi_api.Src.db_related;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.extension_classes;
using vokimi_api.Src.db_related.db_entities.users;

namespace vokimi_api.Endpoints
{
    public static class ImgOperationsEndpoints
    {
        public static async Task<IResult> GetImgFromStorage(string fileKey, VokimiStorageService storageService) =>
            (await storageService.GetObjectFromStorage(fileKey)) ?? Results.Problem("Unable to get image");

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
                return ResultsHelper.BadRequest.WithErr("Unable to update cover. Please refresh the page.");
            }
            using (var db = await dbFactory.CreateDbContextAsync()) {
                BaseDraftTest? test = await db.DraftTestsSharedInfo
                    .Include(t => t.MainInfo)
                    .FirstOrDefaultAsync(t => t.Id == id);

                if (test is null) {
                    return ResultsHelper.BadRequest.UnknownTest();
                }
                if (!httpContext.IsAuthenticatedUserIsTestCreator(test)) {
                    return ResultsHelper.BadRequest.NotCreator();
                }
                string extension = ImgOperationsHelper.ExtractFileExtension(file);
                string key = ImgOperationsHelper.GetDraftTestCoverImgKey(test.Id, extension);

                using (var transaction = await db.Database.BeginTransactionAsync()) {
                    try {
                        string? savedKey = await vokimiStorage.SaveImgToStorage(key, file);
                        if (savedKey is null) {
                            throw new Exception();
                        }
                        string oldPath = test.MainInfo.CoverImagePath;
                        test.MainInfo.UpdateCoverImage(savedKey);
                        await db.SaveChangesAsync();
                        await transaction.CommitAsync();
                        if (oldPath != ImgOperationsConsts.DefaultTestCoverImg && oldPath != savedKey) {
                            await vokimiStorage.DeleteFiles([oldPath]);
                        }
                        return ResultsHelper.Ok.WithImgPath(key);
                    } catch {
                        await transaction.RollbackAsync();
                        return ResultsHelper.BadRequest.ServerError();
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
            if (!Guid.TryParse(questionId, out Guid questionGuid)) {
                return ResultsHelper.BadRequest.SaveChangesTryAgain();
            }
            DraftGeneralTestQuestionId qId = new(questionGuid);
            using (var db = await dbFactory.CreateDbContextAsync()) {
                DraftTestId? testId = (await db.DraftGeneralTestQuestions.FindAsync(qId))?.TestId ?? null;
                if (testId is null) {
                    return ResultsHelper.BadRequest.WithErr("Unable to update answer image. Unknown question");
                }
                AppUserId? creatorId = (await db.DraftTestsSharedInfo.FindAsync(testId))?.CreatorId ?? null;
                if (!httpContext.IfAuthenticatedUserIdEquals(creatorId)) {
                    return ResultsHelper.BadRequest.NotCreator();
                }
                string extension = ImgOperationsHelper.ExtractFileExtension(file);
                string key = ImgOperationsHelper.GetDraftGeneralTestAnswerImgKey(testId.Value, qId, extension);
                return await storageService.IResultSaveImgToStorage(key, file);
            }

        }

        public static async Task<IResult> SaveDraftGeneralTestQuestionImage(
            string questionId,
            IFormFile file,
            VokimiStorageService storageService,
            HttpContext httpContext,
            IDbContextFactory<AppDbContext> dbFactory
        ) {
            if (!Guid.TryParse(questionId, out Guid questionGuid)) {
                return ResultsHelper.BadRequest.SaveChangesTryAgain();
            }
            DraftGeneralTestQuestionId qId = new(questionGuid);
            using (var db = await dbFactory.CreateDbContextAsync()) {
                DraftTestId? testId = (await db.DraftGeneralTestQuestions.FindAsync(qId))?.TestId ?? null;
                if (testId is null) {
                    return ResultsHelper.BadRequest.WithErr("Unable to update question image. Unknown question");
                }
                AppUserId? creatorId = (await db.DraftTestsSharedInfo.FindAsync(testId))?.CreatorId ?? null;
                if (!httpContext.IfAuthenticatedUserIdEquals(creatorId)) {
                    return ResultsHelper.BadRequest.NotCreator();
                }

                string extension = ImgOperationsHelper.ExtractFileExtension(file);
                string key = ImgOperationsHelper.GetDraftGeneralTestQuestionImgKey(testId.Value, qId, extension);
                return await storageService.IResultSaveImgToStorage(key, file);
            }
        }
        public static async Task<IResult> SaveDraftGeneralTestResultImage(
            string resultId,
            IFormFile file,
            VokimiStorageService storageService,
            HttpContext httpContext,
            IDbContextFactory<AppDbContext> dbFactory
        ) {
            if (!Guid.TryParse(resultId, out Guid resultGuid)) {
                return ResultsHelper.BadRequest.SaveChangesTryAgain();
            }
            DraftGeneralTestResultId rId = new(resultGuid);
            using (var db = await dbFactory.CreateDbContextAsync()) {
                DraftTestId? testId = (await db.DraftGeneralTestResults.FindAsync(rId))?.TestId ?? null;
                if (testId is null) {
                    return ResultsHelper.BadRequest.WithErr("Unable to update result image. Unknown result");
                }
                AppUserId? creatorId = (await db.DraftTestsSharedInfo.FindAsync(testId))?.CreatorId ?? null;
                if (!httpContext.IfAuthenticatedUserIdEquals(creatorId)) {
                    return ResultsHelper.BadRequest.NotCreator();
                }
                string extension = ImgOperationsHelper.ExtractFileExtension(file);
                string key = ImgOperationsHelper.GetDraftGeneralTestResultImgKey(testId.Value, rId, extension);
                return await storageService.IResultSaveImgToStorage(key, file);
            }
        }
        public static async Task<IResult> SaveDraftTestConclusionImage(
            string testId,
            IFormFile file,
            VokimiStorageService storageService,
            HttpContext httpContext,
            IDbContextFactory<AppDbContext> dbFactory
        ) {
            if (!Guid.TryParse(testId, out Guid testGuid)) {
                return ResultsHelper.BadRequest.SaveChangesTryAgain();
            }
            DraftTestId tId = new(testGuid);
            using (var db = await dbFactory.CreateDbContextAsync()) {
                BaseDraftTest? test = await db.DraftTestsSharedInfo.FindAsync(tId);
                if (test is null) {
                    return ResultsHelper.BadRequest.WithErr("Unable to update conclusion image. Test not found");
                }
                if (!httpContext.IsAuthenticatedUserIsTestCreator(test)) {
                    return ResultsHelper.BadRequest.NotCreator();
                }
            }
            string key =
                $"{ImgOperationsConsts.TestConclusionsFolder}/" +
                $"{tId.Value.ToString()}" +
                $"/{Guid.NewGuid()}{ImgOperationsHelper.ExtractFileExtension(file)}";
            return await storageService.IResultSaveImgToStorage(key, file);
        }
        public static async Task<IResult> UpdateUserProfilePic(
           IFormFile file,
           IDbContextFactory<AppDbContext> dbFactory,
           VokimiStorageService vokimiStorage,
           HttpContext httpContext
        ) {
            if (!httpContext.TryGetUserId(out var userId)) {
                return ResultsHelper.BadRequest.LogOutLogIn();
            }
            using (var db = await dbFactory.CreateDbContextAsync()) {
                AppUser? user = await db.AppUsers.FindAsync(userId);
                if (user is null) {
                    return ResultsHelper.BadRequest.LogOutLogIn();
                }
                string extension = ImgOperationsHelper.ExtractFileExtension(file);
                string key = ImgOperationsHelper.GetProfilePicImgKey(userId, extension);

                using (var transaction = await db.Database.BeginTransactionAsync()) {
                    try {
                        string? savedKey = await vokimiStorage.SaveImgToStorage(key, file);
                        if (savedKey is null) {
                            throw new Exception();
                        }
                        string oldPath = user.ProfilePicturePath;
                        user.UpdateProfilePicture(savedKey);
                        await db.SaveChangesAsync();
                        await transaction.CommitAsync();
                        if (oldPath != ImgOperationsConsts.DefaultProfilePicture && oldPath != savedKey) {
                            await vokimiStorage.DeleteFiles([oldPath]);
                        }
                        return ResultsHelper.Ok.WithImgPath(key);
                    } catch {
                        await transaction.RollbackAsync();
                        return ResultsHelper.BadRequest.ServerError();
                    }
                }
            }
        }
        internal static async Task<IResult> SetUserProfilePicToDefault(
           IDbContextFactory<AppDbContext> dbFactory,
           HttpContext httpContext,
           VokimiStorageService vokimiStorageService
       ) {
            if (!httpContext.TryGetUserId(out var userId)) {
                return ResultsHelper.BadRequest.LogOutLogIn();
            }
            try {
                using (var db = await dbFactory.CreateDbContextAsync()) {
                    AppUser? user = await db.AppUsers.FindAsync(userId);
                    if (user is null) {
                        return ResultsHelper.BadRequest.LogOutLogIn();
                    }
                    if (user.ProfilePicturePath == ImgOperationsConsts.DefaultProfilePicture) {
                        return ResultsHelper.Ok.WithImgPath(user.ProfilePicturePath);
                    }
                    string oldPath = user.ProfilePicturePath;
                    string newPath = ImgOperationsConsts.DefaultProfilePicture;
                    user.UpdateProfilePicture(newPath);
                    await db.SaveChangesAsync();
                    if (oldPath != ImgOperationsConsts.DefaultProfilePicture && oldPath != newPath) {
                        await vokimiStorageService.DeleteFiles([oldPath]);
                    }
                    return ResultsHelper.Ok.WithImgPath(newPath);
                }
            } catch {
                return ResultsHelper.BadRequest.ServerError();
            }
        }
    }
}
