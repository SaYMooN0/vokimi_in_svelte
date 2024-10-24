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

namespace vokimi_api.Endpoints.pages.test_creation.general_template
{
    public static class GeneralTestResultsCreationEndpoints
    {
        public static IResult GetResultsIdNameDictionary(
            string testId,
            IDbContextFactory<AppDbContext> dbFactory
        ) {
            DraftTestId draftTestId;
            if (!Guid.TryParse(testId, out _)) {
                return ResultsHelper.BadRequestUnknownTest();
            }
            draftTestId = new(new(testId));
            using (var db = dbFactory.CreateDbContext()) {
                var results = db.DraftGeneralTestResults
                    .Where(r => r.TestId == draftTestId)
                    .ToDictionary(r => r.Id.Value.ToString(), r => r.Name);
                return Results.Ok(results);
            }
        }
        public static IResult CreateNewResultForTest(
            [FromBody] GeneralTestResultCreationRequest request,
            IDbContextFactory<AppDbContext> dbFactory
        ) {

            Err formValidatingErr = request.GetError();
            if (formValidatingErr.NotNone()) {
                return ResultsHelper.BadRequestWithErr(formValidatingErr);
            }
            DraftTestId testId = new(new(request.TestId));
            try {
                using (var db = dbFactory.CreateDbContext()) {
                    DraftGeneralTest? test = db.DraftGeneralTests
                        .Include(t => t.PossibleResults)
                        .FirstOrDefault(t => t.Id == testId);
                    if (test is null) {
                        return ResultsHelper.BadRequestUnknownTest();
                    }
                    if (test.PossibleResults.Count >= GeneralTestCreationConsts.MaxResultsForTestCount) {
                        return ResultsHelper.BadRequestWithErr(
                            $"Test cannot have more than {GeneralTestCreationConsts.MaxResultsForTestCount} results"
                        );
                    }
                    DraftGeneralTestResult result = DraftGeneralTestResult.CreateNew(testId, request.ResultName);
                    db.DraftGeneralTestResults.Add(result);
                    db.SaveChanges();
                    return Results.Ok();
                }
            } catch {
                return ResultsHelper.BadRequestServerError();
            }
        }
        public static IResult GetDraftGeneralTestResultsData(
            string testId,
            IDbContextFactory<AppDbContext> dbFactory
        ) {
            DraftTestId draftTestId;
            if (!Guid.TryParse(testId, out _)) {
                return ResultsHelper.BadRequestUnknownTest();
            }
            draftTestId = new(new(testId));
            using (var db = dbFactory.CreateDbContext()) {
                var results = db.DraftGeneralTestResults
                    .Where(r => r.TestId == draftTestId)
                    .Select(DraftGeneralTestResultData.FromResult)
                    .ToArray();
                return Results.Ok(results);
            }
        }
        public static IResult GetDraftGeneralTestResultDataToEdit(
            string resultId,
            IDbContextFactory<AppDbContext> dbFactory
        ) {
            DraftGeneralTestResultId resId;
            if (!Guid.TryParse(resultId, out _)) {
                return ResultsHelper.BadRequestWithErr("Unknown test result");
            }
            resId = new(new(resultId));
            using (var db = dbFactory.CreateDbContext()) {
                DraftGeneralTestResult? result = db.DraftGeneralTestResults.FirstOrDefault(r => r.Id == resId);
                if (result is null) {
                    return ResultsHelper.BadRequestWithErr("Unknown test result");
                }
                return Results.Ok(DraftGeneralTestResultData.FromResult(result));
            }
        }
        public static IResult DeleteGeneralDraftTestResult(
            string resultId,
            IDbContextFactory<AppDbContext> dbFactory
        ) {
            DraftGeneralTestResultId resultToDeleteId;
            if (!Guid.TryParse(resultId, out _)) {
                return ResultsHelper.BadRequestWithErr("An error has occurred. Please refresh the page and try again");
            }
            resultToDeleteId = new(new(resultId));
            using (var db = dbFactory.CreateDbContext()) {
                try {
                    DraftGeneralTestResult? res = db.DraftGeneralTestResults
                                    .FirstOrDefault(q => q.Id == resultToDeleteId);
                    if (res is null) {
                        return ResultsHelper.BadRequestWithErr("Unknown result");
                    }
                    db.DraftGeneralTestResults.Remove(res);
                    db.SaveChanges();
                    return Results.Ok();
                } catch {
                    return ResultsHelper.BadRequestServerError();
                }

            }

        }
        public async static Task<IResult> SaveChangesForDraftGeneralTestResult(
            [FromBody] DraftGeneralTestResultData newResData,
            IDbContextFactory<AppDbContext> dbFactory,
            VokimiStorageService vokimiStorage
        ) {

            DraftGeneralTestResultId resId;
            if (Guid.TryParse(newResData.Id, out var guid)) {
                resId = new(guid);
            } else {
                return ResultsHelper.BadRequestWithErr("Unable to save changes. Please try again later");
            }
            using (var db = dbFactory.CreateDbContext()) {
                DraftGeneralTestResult? res = db.DraftGeneralTestResults.FirstOrDefault(r => r.Id == resId);
                if (res is null) {
                    return ResultsHelper.BadRequestWithErr("Unknown result");
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
                db.SaveChanges();
                return Results.Ok();
            }
        }
    }
}
