using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
using vokimi_api.Src.dtos.responses.test_creation_responses.shared;
using vokimi_api.Src.dtos.shared;
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
            if (!httpContext.TryGetUserId(out var userId)) {
                return ResultsHelper.BadRequest.LogOutLogIn();
            }

            using (var db = dbFactory.CreateDbContext()) {
                using (var transaction = await db.Database.BeginTransactionAsync()) {
                    try {
                        AppUser? user = db.AppUsers.FirstOrDefault(u => u.Id == userId);
                        if (user is null) {
                            return ResultsHelper.BadRequest.LogOutLogIn();
                        }

                        DraftTestId? testId = template switch {
                            TestTemplate.General => await CreateNewGeneralTest(db, userId),
                            _ => null
                        };
                        if (testId is null) { throw new Exception(); }

                        await transaction.CommitAsync();
                        return Results.Ok(new { TestId = testId.ToString() });
                    } catch {
                        await transaction.RollbackAsync();
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

            await db.DraftTestMainInfo.AddAsync(mainInfo);
            await db.TestStyles.AddAsync(styles);
            await db.DraftTestsSharedInfo.AddAsync(test);

            await db.SaveChangesAsync();
            return test.Id;
        }

        public static async Task<IResult> GetDraftTestMainInfoData(
            IDbContextFactory<AppDbContext> dbFactory,
            string testId,
            HttpContext httpContext
        ) {
            if (string.IsNullOrEmpty(testId)) { return ResultsHelper.BadRequest.ServerError(); }

            DraftTestId draftTestId;
            if (!Guid.TryParse(testId, out _)) {
                return ResultsHelper.BadRequest.ServerError();
            }
            draftTestId = new(new(testId));

            using (var db = await dbFactory.CreateDbContextAsync()) {
                BaseDraftTest? test = await db.DraftTestsSharedInfo
                        .Include(t => t.MainInfo)
                        .FirstOrDefaultAsync(t => t.Id == draftTestId);

                if (test is null || test.MainInfo is null) {
                    return ResultsHelper.BadRequest.WithErr("Unknown test");
                }
                if (!httpContext.IsAuthenticatedUserIsTestCreator(test)) {
                    return ResultsHelper.BadRequest.NotCreator();
                }
                return Results.Ok(DraftTestMainInfoDataResponse.FromDraftTest(test));
            }
        }
        public static async Task<IResult> UpdateDraftTestMainInfo(
            IDbContextFactory<AppDbContext> dbFactory,
            [FromBody] DraftTestMainInfoUpdateRequest request,
            HttpContext httpContext
        ) {
            Err validationErr = request.CheckForErr();
            if (validationErr.NotNone()) {
                return ResultsHelper.BadRequest.WithErr(validationErr.Message);
            }
            ParsedDraftTestMainInfoUpdateRequest? newData = request.ParseToObjWithTypes();
            if (newData is null) {
                return ResultsHelper.BadRequest.WithErr("Error occurred during saving. Please try again");
            }
            try {
                using (var db = await dbFactory.CreateDbContextAsync()) {
                    BaseDraftTest? test = await db.DraftTestsSharedInfo
                        .Include(t => t.MainInfo)
                        .FirstOrDefaultAsync(t => t.Id == newData.TestId);
                    if (test is null) {
                        return ResultsHelper.BadRequest.UnknownTest();
                    }
                    if (!httpContext.IsAuthenticatedUserIsTestCreator(test)) {
                        return ResultsHelper.BadRequest.NotCreator();
                    }
                    test.MainInfo.Update(newData.Name, newData.Description, newData.Language);
                    await db.SaveChangesAsync();
                    return Results.Ok();
                }
            } catch {
                return ResultsHelper.BadRequest.ServerError();
            }
        }
        public static async Task<IResult> SetDraftTestCoverToDefault(
            string testId,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {
            DraftTestId draftTestId;
            if (Guid.TryParse(testId, out var testGuid)) {
                draftTestId = new(testGuid);
            } else {
                return ResultsHelper.BadRequest.UnknownTest();
            }
            try {
                using (var db = await dbFactory.CreateDbContextAsync()) {
                    BaseDraftTest? test = await db.DraftTestsSharedInfo
                        .Include(t => t.MainInfo)
                        .FirstOrDefaultAsync(t => t.Id == draftTestId);
                    if (test is null) {
                        return ResultsHelper.BadRequest.UnknownTest();
                    }
                    if (!httpContext.IsAuthenticatedUserIsTestCreator(test)) {
                        return ResultsHelper.BadRequest.NotCreator();
                    }

                    string imgPath = ImgOperationsConsts.DefaultTestCoverImg;
                    test.MainInfo.UpdateCoverImage(imgPath);
                    await db.SaveChangesAsync();
                    return ResultsHelper.Ok.WithImgPath(imgPath);
                }
            } catch {
                return ResultsHelper.BadRequest.ServerError();
            }
        }

        public static async Task<IResult> GetDraftTestConclusionData(
            IDbContextFactory<AppDbContext> dbFactory,
            string testId,
            HttpContext httpContext
        ) {

            DraftTestId draftTestId;
            if (!Guid.TryParse(testId, out _)) {
                return ResultsHelper.BadRequest.ServerError();
            }
            draftTestId = new(new(testId));

            using (var db = await dbFactory.CreateDbContextAsync()) {
                BaseDraftTest? test = await db.DraftTestsSharedInfo
                        .Include(t => t.Conclusion)
                        .FirstOrDefaultAsync(t => t.Id == draftTestId);
                if (test is null) { return ResultsHelper.BadRequest.ServerError(); }
                if (!httpContext.IsAuthenticatedUserIsTestCreator(test)) {
                    return ResultsHelper.BadRequest.NotCreator();
                }
                return Results.Ok(TestConclusionData.FromConclusion(test.Conclusion));
            }
        }
        public static async Task<IResult> CreateDraftTestConclusion(
            IDbContextFactory<AppDbContext> dbFactory,
            string testId,
            HttpContext httpContext
        ) {
            DraftTestId draftTestId;
            if (!Guid.TryParse(testId, out _)) {
                return ResultsHelper.BadRequest.ServerError();
            }
            draftTestId = new(new(testId));
            using (var db = await dbFactory.CreateDbContextAsync()) {
                BaseDraftTest? test = await db.DraftTestsSharedInfo
                    .Include(t => t.Conclusion)
                    .FirstOrDefaultAsync(t => t.Id == draftTestId);
                if (test is null) {
                    return ResultsHelper.BadRequest.ServerError();
                }
                if (!httpContext.IsAuthenticatedUserIsTestCreator(test)) {
                    return ResultsHelper.BadRequest.NotCreator();
                }
                if (test.Conclusion is not null) {
                    return Results.Ok();
                } else {
                    try {

                        TestConclusion conclusion = TestConclusion.CreateNew();
                        db.TestConclusions.Add(conclusion);
                        test.SetConclusion(conclusion);
                        await db.SaveChangesAsync();
                        return Results.Ok();
                    } catch {
                        return ResultsHelper.BadRequest.ServerError();
                    }
                }
            }
        }
        public static async Task<IResult> UpdateDraftTestConclusion(
            IDbContextFactory<AppDbContext> dbFactory,
            [FromBody] TestConclusionData data,
            VokimiStorageService vokimiStorage,
            HttpContext httpContext,
            string testId
        ) {
            DraftTestId draftTestId;
            if (!Guid.TryParse(testId, out var _)) {
                return ResultsHelper.BadRequest.WithErr("Unable to update conclusion. Please try again later");
            }
            draftTestId = new(new(testId));

            using (var db = await dbFactory.CreateDbContextAsync()) {
                BaseDraftTest? test = await db.DraftTestsSharedInfo
                    .Include(t => t.Conclusion)
                    .FirstOrDefaultAsync(t => t.Id == draftTestId);
                if (test is null) {
                    return ResultsHelper.BadRequest.UnknownTest();
                }
                if (!httpContext.IsAuthenticatedUserIsTestCreator(test)) {
                    return ResultsHelper.BadRequest.NotCreator();
                }
                Err validationErr = data.CheckForErr();
                if (validationErr.NotNone()) {
                    return ResultsHelper.BadRequest.WithErr(validationErr.Message);
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
                    data.MaxFeedbackLength
                );
                string unusedImgPrefix = $"{ImgOperationsConsts.TestConclusionsFolder}/{test.Id.Value.ToString()}/";
                string[] reservedKeys =
                    string.IsNullOrWhiteSpace(data.AdditionalImage) ?
                    Array.Empty<string>() :
                    [data.AdditionalImage];
                Err imgClearingErr = await vokimiStorage.ClearFolder(unusedImgPrefix, reservedKeys);
                if (imgClearingErr.NotNone()) {
                    return ResultsHelper.BadRequest.ServerError();
                }
                db.Update(test.Conclusion);
                await db.SaveChangesAsync();
                return Results.Ok();
            }

        }
        public static async Task<IResult> DeleteDraftTestConclusion(
            IDbContextFactory<AppDbContext> dbFactory,
            string testId,
            HttpContext httpContext
        ) {
            DraftTestId draftTestId;
            if (!Guid.TryParse(testId, out _)) {
                return ResultsHelper.BadRequest.ServerError();
            }
            draftTestId = new(new(testId));
            using (var db = await dbFactory.CreateDbContextAsync()) {
                BaseDraftTest? test = await db.DraftTestsSharedInfo
                     .Include(t => t.Conclusion)
                     .FirstOrDefaultAsync(c => c.Id == draftTestId);
                if (test is null) {
                    return ResultsHelper.BadRequest.UnknownTest();
                }
                if (!httpContext.IsAuthenticatedUserIsTestCreator(test)) {
                    return ResultsHelper.BadRequest.NotCreator();
                }
                if (test.Conclusion is null) {
                    return Results.Ok();
                }

                db.TestConclusions.Remove(test.Conclusion);
                test.SetConclusion(null);
                db.Update(test);
                await db.SaveChangesAsync();
                return Results.Ok();
            }
        }
        public static async Task<IResult> GetDraftTestSettingsData(
            IDbContextFactory<AppDbContext> dbFactory,
            string testId,
            HttpContext httpContext
        ) {
            DraftTestId draftTestId;
            if (!Guid.TryParse(testId, out Guid testGuid)) {
                return ResultsHelper.BadRequest.ServerError();
            }
            draftTestId = new(testGuid);

            using (var db = await dbFactory.CreateDbContextAsync()) {
                BaseDraftTest? test = await db.DraftTestsSharedInfo.FindAsync(draftTestId);

                if (test is null) {
                    return ResultsHelper.BadRequest.WithErr("Unknown test");
                }
                if (!httpContext.IsAuthenticatedUserIsTestCreator(test)) {
                    return ResultsHelper.BadRequest.NotCreator();
                }
                return Results.Ok(DraftTestSettingsDataResponse.FromTestSettings(test.Settings));
            }
        }
        public static async Task<IResult> UpdateDraftTestSettings(
            IDbContextFactory<AppDbContext> dbFactory,
            [FromBody] DraftTestSettingsUpdateRequest request,
            HttpContext httpContext
        ) {
            Err validationErr = request.CheckForErr();
            if (validationErr.NotNone()) {
                return ResultsHelper.BadRequest.WithErr(validationErr.Message);
            }

            DraftTestId testId = request.GetParsedTestId().Value;
            PrivacyValues privacy = request.GetParsedPrivacy() ?? PrivacyValues.Anyone;

            try {
                using (var db = await dbFactory.CreateDbContextAsync()) {
                    BaseDraftTest? test = await db.DraftTestsSharedInfo.FindAsync(testId);
                    if (test is null) {
                        return ResultsHelper.BadRequest.UnknownTest();
                    }
                    if (!httpContext.IsAuthenticatedUserIsTestCreator(test)) {
                        return ResultsHelper.BadRequest.NotCreator();
                    }
                    TestSettings newSettings = new(
                        privacy,
                        request.DiscussionsOpen,
                        request.TestTakenPostsAllowed,
                        request.EnableTestRatings,
                        request.TagsSuggestionsAllowed
                    );
                    test.UpdateTestSettings(newSettings);
                    await db.SaveChangesAsync();
                    return Results.Ok();
                }
            } catch {
                return ResultsHelper.BadRequest.ServerError();
            }
        }
    }
}