using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vokimi_api.Helpers;
using vokimi_api.Src.db_related.db_entities.draft_tests.draft_general_test;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.db_related;
using vokimi_api.Src.dtos.requests.test_creation.general_template;
using vokimi_api.Src.dtos.shared.general_test_creation;
using vokimi_api.Src;
using vokimi_api.Services;
using vokimi_api.Src.constants_store_classes;
using vokimi_api.Src.extension_classes;

namespace vokimi_api.Endpoints.pages.test_creation.general_template
{
    public static class GeneralTestResultsCreationEndpoints
    {
        public static async Task<IResult> GetResultsIdNameDictionary(
            string testId,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {
            DraftTestId draftTestId;
            if (!Guid.TryParse(testId, out Guid draftTestGuid)) {
                return ResultsHelper.BadRequestUnknownTest();
            }
            draftTestId = new(draftTestGuid);
            using (var db = await dbFactory.CreateDbContextAsync()) {
                DraftGeneralTest? test = await db.DraftGeneralTests
                    .Include(t => t.PossibleResults)
                    .FirstOrDefaultAsync(t => t.Id == draftTestId);
                if (test is null) {
                    return ResultsHelper.BadRequestUnknownTest();
                }
                if (!httpContext.IsAuthenticatedUserIsTestCreator(test)) {
                    return ResultsHelper.BadRequestNotCreator();
                }
                var results = test.PossibleResults.ToDictionary(
                    r => r.Id.Value.ToString(),
                    r => r.Name
                );
                return Results.Ok(results);
            }
        }
        public static async Task<IResult> CreateNewResultForTest(
            [FromBody] GeneralTestResultCreationRequest request,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {

            Err formValidatingErr = request.GetError();
            if (formValidatingErr.NotNone()) {
                return ResultsHelper.BadRequestWithErr(formValidatingErr);
            }
            DraftTestId testId = new(new(request.TestId));
            try {
                using (var db = await dbFactory.CreateDbContextAsync()) {
                    DraftGeneralTest? test = await db.DraftGeneralTests
                        .Include(t => t.PossibleResults)
                        .FirstOrDefaultAsync(t => t.Id == testId);
                    if (test is null) {
                        return ResultsHelper.BadRequestUnknownTest();
                    }
                    if (!httpContext.IsAuthenticatedUserIsTestCreator(test)) {
                        return ResultsHelper.BadRequestNotCreator();
                    }
                    if (test.PossibleResults.Count >= GeneralTestCreationConsts.MaxResultsForTestCount) {
                        return ResultsHelper.BadRequestWithErr(
                            $"Test cannot have more than {GeneralTestCreationConsts.MaxResultsForTestCount} results"
                        );
                    }
                    DraftGeneralTestResult result = DraftGeneralTestResult.CreateNew(testId, request.ResultName);
                    await db.DraftGeneralTestResults.AddAsync(result);
                    await db.SaveChangesAsync();
                    return Results.Ok();
                }
            } catch {
                return ResultsHelper.BadRequestServerError();
            }
        }
        public static async Task<IResult> GetDraftGeneralTestResultsData(
            string testId,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {
            DraftTestId draftTestId;
            if (!Guid.TryParse(testId, out Guid testGuid)) {
                return ResultsHelper.BadRequestUnknownTest();
            }
            draftTestId = new(testGuid);

            using (var db = await dbFactory.CreateDbContextAsync()) {
                DraftGeneralTest? test = await db.DraftGeneralTests
                    .Include(t => t.PossibleResults)
                    .FirstOrDefaultAsync(t => t.Id == draftTestId);
                if (test is null) {
                    return ResultsHelper.BadRequestUnknownTest();
                }
                if (!httpContext.IsAuthenticatedUserIsTestCreator(test)) {
                    return ResultsHelper.BadRequestNotCreator();
                }
                var results = test.PossibleResults
                    .Select(DraftGeneralTestResultData.FromResult)
                    .ToArray();
                return Results.Ok(results);
            }
        }
        public static async Task<IResult> GetDraftGeneralTestResultDataToEdit(
            string resultId,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {
            DraftGeneralTestResultId resId;
            if (!Guid.TryParse(resultId, out Guid resultGuid)) {
                return ResultsHelper.BadRequestWithErr("Unknown test result");
            }
            resId = new(resultGuid);
            using (var db = await dbFactory.CreateDbContextAsync()) {
                DraftGeneralTestResult? result = db.DraftGeneralTestResults.Find(resId);
                if (result is null) {
                    return ResultsHelper.BadRequestWithErr("Unknown test result");
                }
                DraftGeneralTest? test = db.DraftGeneralTests.Find(result.TestId);
                if (test is null) {
                    return ResultsHelper.BadRequestUnknownTest();
                }
                if (!httpContext.IsAuthenticatedUserIsTestCreator(test)) {
                    return ResultsHelper.BadRequestNotCreator();
                }
                return Results.Ok(DraftGeneralTestResultData.FromResult(result));
            }
        }
        public static async Task<IResult> DeleteGeneralDraftTestResult(
            string resultId,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {
            DraftGeneralTestResultId resultToDeleteId;
            if (!Guid.TryParse(resultId, out Guid resultGuid)) {
                return ResultsHelper.BadRequestWithErr("An error has occurred. Please refresh the page and try again");
            }
            resultToDeleteId = new(resultGuid);
            using (var db = await dbFactory.CreateDbContextAsync()) {
                try {
                    DraftGeneralTestResult? res = await db.DraftGeneralTestResults.FindAsync(resultToDeleteId);
                    if (res is null) {
                        return ResultsHelper.BadRequestWithErr("Unknown result");
                    }
                    DraftGeneralTest? test = await db.DraftGeneralTests.FindAsync(res.TestId);
                    if (test is null) {
                        return ResultsHelper.BadRequestUnknownTest();
                    }
                    if (!httpContext.IsAuthenticatedUserIsTestCreator(test)) {
                        return ResultsHelper.BadRequestNotCreator();
                    }
                    db.DraftGeneralTestResults.Remove(res);
                    await db.SaveChangesAsync();
                    return Results.Ok();
                } catch {
                    return ResultsHelper.BadRequestServerError();
                }

            }

        }
        public async static Task<IResult> SaveChangesForDraftGeneralTestResult(
            [FromBody] DraftGeneralTestResultData newResData,
            IDbContextFactory<AppDbContext> dbFactory,
            VokimiStorageService vokimiStorage,
            HttpContext httpContext
        ) {

            DraftGeneralTestResultId resId;
            if (Guid.TryParse(newResData.Id, out var guid)) {
                resId = new(guid);
            } else {
                return ResultsHelper.BadRequestWithErr("Unable to save changes. Please try again later");
            }
            using (var db = await dbFactory.CreateDbContextAsync()) {
                DraftGeneralTestResult? res = await db.DraftGeneralTestResults.FindAsync(resId);
                if (res is null) {
                    return ResultsHelper.BadRequestWithErr("Unknown result");
                }
                DraftGeneralTest? test = await db.DraftGeneralTests.FindAsync(res.TestId);
                if (test is null) {
                    return ResultsHelper.BadRequestUnknownTest();
                }
                if (!httpContext.IsAuthenticatedUserIsTestCreator(test)) {
                    return ResultsHelper.BadRequestNotCreator();
                }
                Err validationErr = newResData.CheckForErr();
                if (validationErr.NotNone()) {
                    return ResultsHelper.BadRequestWithErr(validationErr);
                }
                res.Update(newResData.Name, newResData.Text, newResData.ImagePath);
                string unusedImgPrefix = ImgOperationsHelper.DraftGeneralTestResultsFolder(res.TestId, resId);
                Err imgClearingErr = await vokimiStorage.ClearUnusedObjectsInFolder(unusedImgPrefix, newResData.ImagePath);
                if (imgClearingErr.NotNone()) {
                    return ResultsHelper.BadRequestServerError();
                }
                db.Update(res);
                await db.SaveChangesAsync();
                return Results.Ok();
            }
        }
    }
}
