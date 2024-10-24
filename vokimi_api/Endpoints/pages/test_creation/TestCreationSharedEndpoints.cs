using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Security.Claims;
using vokimi_api.Helpers;
using vokimi_api.Services;
using vokimi_api.Src;
using vokimi_api.Src.constants_store_classes;
using vokimi_api.Src.db_related;
using vokimi_api.Src.db_related.db_entities.draft_published_tests_shared;
using vokimi_api.Src.db_related.db_entities.draft_tests.draft_general_test;
using vokimi_api.Src.db_related.db_entities.draft_tests.draft_tests_shared;
using vokimi_api.Src.db_related.db_entities.users;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.dtos.requests.test_creation.templates_shared;
using vokimi_api.Src.dtos.responses;
using vokimi_api.Src.dtos.responses.test_creation_responses.shared;
using vokimi_api.Src.dtos.shared;
using vokimi_api.Src.dtos.shared.general_test_creation;
using vokimi_api.Src.dtos.shared.test_creation_shared;
using vokimi_api.Src.enums;
using vokimi_api.Src.extension_classes;

namespace vokimi_api.Endpoints.pages.test_creation
{
    public static class TestCreationSharedEndpoints
    {
        public static async Task<IResult> CreateNewTest(
            HttpContext httpContext,
            IDbContextFactory<AppDbContext> dbFactory,
            TestTemplate template
        ) {

            IResult authErrResponse = Results.BadRequest(new { Error = "Please log out and log in again" });

            var cntxUser = httpContext.User;

            if (cntxUser.Identity == null || !cntxUser.Identity.IsAuthenticated) {
                return authErrResponse;
            }
            string? userIdStr = cntxUser.FindFirstValue(PingAuthResponse.ClaimKeyUserId);
            if (string.IsNullOrEmpty(userIdStr)) { return authErrResponse; }

            AppUserId userId;

            if (Guid.TryParse(userIdStr, out Guid userGuid)) {
                userId = new(userGuid);
            } else { return authErrResponse; }

            using (var db = dbFactory.CreateDbContext()) {
                using (var transaction = await db.Database.BeginTransactionAsync()) {
                    try {
                        AppUser? user = db.AppUsers.FirstOrDefault(u => u.Id == userId);
                        if (user is null) { return authErrResponse; }

                        DraftTestId? testId = template switch {
                            TestTemplate.General => await CreateNewGeneralTest(db, userId),
                            _ => null
                        };
                        if (testId is null) { throw new Exception(); }

                        await transaction.CommitAsync();
                        return Results.Ok(new { TestId = testId.ToString() });
                    } catch {
                        transaction.Rollback();
                        return Results.BadRequest(new { Error = "Something went wrong. Please try again later" });
                    }
                }
            }
        }

        private static async Task<DraftTestId> CreateNewGeneralTest(
            AppDbContext db,
            AppUserId creatorId
        ) {
            DraftTestMainInfo mainInfo = DraftTestMainInfo.CreateNewFromName("New Draft General Test");
            TestStylesSheet styles = TestStylesSheet.CreateNew();
            BaseDraftTest test = DraftGeneralTest.CreateNew(creatorId, mainInfo.Id, styles.Id);

            db.DraftTestMainInfo.Add(mainInfo);
            db.TestStyles.Add(styles);
            db.DraftTestsSharedInfo.Add(test);

            await db.SaveChangesAsync();
            return test.Id;
        }

        public static IResult GetDraftTestMainInfoData(
            IDbContextFactory<AppDbContext> dbFactory,
            string testId,
            HttpContext httpContext
        ) {
            if (string.IsNullOrEmpty(testId)) { return ResultsHelper.BadRequestServerError(); }

            DraftTestId draftTestId;
            if (!Guid.TryParse(testId, out _)) {
                return ResultsHelper.BadRequestServerError();
            }
            draftTestId = new(new(testId));

            using (var db = dbFactory.CreateDbContext()) {
                BaseDraftTest? test = db.DraftTestsSharedInfo
                        .Include(t => t.MainInfo)
                        .FirstOrDefault(t => t.Id == draftTestId);

                if (test is null || test.MainInfo is null) {
                    return ResultsHelper.BadRequestWithErr("Unknown test");
                }
                if (!httpContext.IfAuthenticatedUserIdIsTestCreator(test)) {
                    return ResultsHelper.BadRequestNotCreator();
                }
                return Results.Ok(DraftTestMainInfoDataResponse.FromDraftTest(test));
            }
        }
        public static IResult UpdateDraftTestMainInfo(
            IDbContextFactory<AppDbContext> dbFactory,
            [FromBody] DraftTestMainInfoUpdateRequest request,
            HttpContext httpContext
        ) {
            Err validationErr = request.CheckForErr();
            if (validationErr.NotNone()) {
                return ResultsHelper.BadRequestWithErr(validationErr.Message);
            }
            ParsedDraftTestMainInfoUpdateRequest? newData = request.ParseToObjWithTypes();
            if (newData is null) {
                return ResultsHelper.BadRequestWithErr("Error occurred during saving. Please try again");
            }
            try {
                using (var db = dbFactory.CreateDbContext()) {
                    BaseDraftTest? test = db.DraftTestsSharedInfo
                        .Include(t => t.MainInfo)
                        .FirstOrDefault(t => t.Id == newData.TestId);
                    if (test is null) {
                        return ResultsHelper.BadRequestUnknownTest();
                    }
                    if (!httpContext.IfAuthenticatedUserIdIsTestCreator(test)) {
                        return ResultsHelper.BadRequestNotCreator();
                    }
                    test.MainInfo.Update(newData.Name, newData.Description, newData.Language, newData.Privacy);
                    db.SaveChanges();
                    return Results.Ok();
                }
            } catch {
                return ResultsHelper.BadRequestServerError();
            }
        }
        public static IResult SetDraftTestCoverToDefault(
            string testId,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {
            DraftTestId draftTestId;
            if (Guid.TryParse(testId, out var testGuid)) {
                draftTestId = new(testGuid);
            } else {
                return ResultsHelper.BadRequestUnknownTest();
            }
            try {
                using (var db = dbFactory.CreateDbContext()) {
                    BaseDraftTest? test = db.DraftTestsSharedInfo
                        .Include(t => t.MainInfo)
                        .FirstOrDefault(t => t.Id == draftTestId);
                    if (test is null) {
                        return ResultsHelper.BadRequestUnknownTest();
                    }
                    if (!httpContext.IfAuthenticatedUserIdIsTestCreator(test)) {
                        return ResultsHelper.BadRequestNotCreator();
                    }

                    string imgPath = ImgOperationsConsts.DefaultTestCoverImg;
                    test.MainInfo.UpdateCoverImage(imgPath);
                    db.SaveChanges();
                    return ResultsHelper.OkResultWithImgPath(imgPath);
                }
            } catch {
                return ResultsHelper.BadRequestServerError();
            }
        }

        public static IResult GetDraftTestConclusionData(
            IDbContextFactory<AppDbContext> dbFactory,
            string testId,
            HttpContext httpContext
        ) {

            DraftTestId draftTestId;
            if (!Guid.TryParse(testId, out _)) {
                return ResultsHelper.BadRequestServerError();
            }
            draftTestId = new(new(testId));

            using (var db = dbFactory.CreateDbContext()) {
                BaseDraftTest? test = db.DraftTestsSharedInfo
                        .Include(t => t.Conclusion)
                        .FirstOrDefault(t => t.Id == draftTestId);
                if (test is null) { return ResultsHelper.BadRequestServerError(); }
                if (!httpContext.IfAuthenticatedUserIdIsTestCreator(test)) {
                    return ResultsHelper.BadRequestNotCreator();
                }
                return Results.Ok(DraftTestConclusionData.FromConclusion(test.Conclusion));
            }
        }
        public static IResult CreateDraftTestConclusion(
            IDbContextFactory<AppDbContext> dbFactory,
            string testId,
            HttpContext httpContext
        ) {
            DraftTestId draftTestId;
            if (!Guid.TryParse(testId, out _)) {
                return ResultsHelper.BadRequestServerError();
            }
            draftTestId = new(new(testId));
            using (var db = dbFactory.CreateDbContext()) {
                BaseDraftTest? test = db.DraftTestsSharedInfo
                    .Include(t => t.Conclusion)
                    .FirstOrDefault(t => t.Id == draftTestId);
                if (test is null) {
                    return ResultsHelper.BadRequestServerError();
                }
                if (!httpContext.IfAuthenticatedUserIdIsTestCreator(test)) {
                    return ResultsHelper.BadRequestNotCreator();
                }
                if (test.Conclusion is not null) {
                    return Results.Ok();
                } else {
                    try {

                        TestConclusion conclusion = TestConclusion.CreateNew();
                        db.TestConclusions.Add(conclusion);
                        test.SetConclusion(conclusion);
                        db.SaveChanges();
                        return Results.Ok();
                    } catch {
                        return ResultsHelper.BadRequestServerError();
                    }
                }
            }
        }
        public static async Task<IResult> UpdateDraftTestConclusion(
            IDbContextFactory<AppDbContext> dbFactory,
            [FromBody] DraftTestConclusionData data,
            VokimiStorageService vokimiStorage,
            HttpContext httpContext,
            string testId
        ) {
            DraftTestId draftTestId;
            if (!Guid.TryParse(testId, out var _)) {
                return ResultsHelper.BadRequestWithErr("Unable to update conclusion. Please try again later");
            }
            draftTestId = new(new(testId));

            using (var db = dbFactory.CreateDbContext()) {
                BaseDraftTest? test = db.DraftTestsSharedInfo
                    .Include(t => t.Conclusion)
                    .FirstOrDefault(t => t.Id == draftTestId);
                if (test is null) {
                    return ResultsHelper.BadRequestUnknownTest();
                }
                if (!httpContext.IfAuthenticatedUserIdIsTestCreator(test)) {
                    return ResultsHelper.BadRequestNotCreator();
                }
                Err validationErr = data.CheckForErr();
                if (validationErr.NotNone()) {
                    return ResultsHelper.BadRequestWithErr(validationErr.Message);
                }
                if (test.Conclusion is null) {
                    TestConclusion newConclusion = TestConclusion.CreateNew();
                    db.TestConclusions.Add(newConclusion);
                    test.SetConclusion(newConclusion);
                }
                test.Conclusion.Update(
                    data.Text,
                    data.AdditionalImage,
                    data.AnyFeedback,
                    data.FeedbackText,
                    data.maxFeedbackLength
                );
                string unusedImgPrefix = $"{ImgOperationsConsts.TestConclusionsFolder}/{test.Id.Value.ToString()}/";
                string[] reservedKeys =
                    string.IsNullOrWhiteSpace(data.AdditionalImage) ?
                    Array.Empty<string>() :
                    [data.AdditionalImage];
                Err imgClearingErr = await vokimiStorage.ClearUnusedObjectsInFolder(unusedImgPrefix, reservedKeys);
                if (imgClearingErr.NotNone()) {
                    return ResultsHelper.BadRequestServerError();
                }
                db.Update(test.Conclusion);
                db.SaveChanges();
                return Results.Ok();
            }

        }
        public static IResult DeleteDraftTestConclusion(
            IDbContextFactory<AppDbContext> dbFactory,
            string testId,
            HttpContext httpContext
        ) {
            DraftTestId draftTestId;
            if (!Guid.TryParse(testId, out _)) {
                return ResultsHelper.BadRequestServerError();
            }
            draftTestId = new(new(testId));
            using (var db = dbFactory.CreateDbContext()) {
                BaseDraftTest? test = db.DraftTestsSharedInfo
                     .Include(t => t.Conclusion)
                     .FirstOrDefault(c => c.Id == draftTestId);
                if (test is null) {
                    return ResultsHelper.BadRequestUnknownTest();
                }
                if (!httpContext.IfAuthenticatedUserIdIsTestCreator(test)) {
                    return ResultsHelper.BadRequestNotCreator();
                }
                if (test.Conclusion is null) {
                    return Results.Ok();
                }

                db.TestConclusions.Remove(test.Conclusion);
                test.SetConclusion(null);
                db.Update(test);
                db.SaveChanges();
                return Results.Ok();
            }
        }
    }
}