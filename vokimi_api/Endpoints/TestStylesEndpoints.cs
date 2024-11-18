using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vokimi_api.Helpers;
using vokimi_api.Src.db_related.db_entities.draft_tests.draft_tests_shared;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.db_related;
using vokimi_api.Src.dtos.shared;
using vokimi_api.Src.db_related.db_entities.draft_published_tests_shared;
using vokimi_api.Src;
using vokimi_api.Src.enums;
using vokimi_api.Src.extension_classes;

namespace vokimi_api.Endpoints
{
    public static class TestStylesEndpoints
    {
        public static async Task<IResult> GetDraftTestStylesData(
            IDbContextFactory<AppDbContext> dbFactory,
            string testId
        ) {
            if (!Guid.TryParse(testId, out var testGuid)) {
                return ResultsHelper.BadRequest.ServerError();
            }
            DraftTestId draftTestId = new(testGuid);

            using (var db = await dbFactory.CreateDbContextAsync()) {
                BaseDraftTest? test = await db.DraftTestsSharedInfo
                        .Include(t => t.StylesSheet)
                        .FirstOrDefaultAsync(t => t.Id == draftTestId);
                if (test is null) {
                    return ResultsHelper.BadRequest.UnknownTest();
                }
                return Results.Ok(TestStylesDataDto.FromTestStylesSheet(test.StylesSheet));
            }
        }
        public static async Task<IResult> UpdateDraftTestStylesData(
            IDbContextFactory<AppDbContext> dbFactory,
            [FromBody] TestStylesDataDto data,
            string testId,
            HttpContext httpContext
        ) {
            Err validationErr = data.CheckForErr();
            if (validationErr.NotNone()) {
                return ResultsHelper.BadRequest.WithErr(validationErr);
            }
            (string accentColor, ArrowIconType arrowType)? dataWithTypes = data.GetDataWithTypes();
            if (dataWithTypes is null) {
                return ResultsHelper.BadRequest.ServerError();
            }
            if (!Guid.TryParse(testId, out var testGuid)) {
                return ResultsHelper.BadRequest.ServerError();
            }
            DraftTestId draftTestId = new(testGuid);
            using (var db = await dbFactory.CreateDbContextAsync()) {
                BaseDraftTest? test = await db.DraftTestsSharedInfo
                        .Include(t => t.StylesSheet)
                        .FirstOrDefaultAsync(t => t.Id == draftTestId);
                if (test is null) {
                    return ResultsHelper.BadRequest.ServerError();
                }
                if (!httpContext.IsAuthenticatedUserIsTestCreator(test)) {
                    return ResultsHelper.BadRequest.NotCreator();
                }
                test.StylesSheet.Update(
                    dataWithTypes.Value.accentColor,
                    dataWithTypes.Value.arrowType
                );
                db.TestStyles.Update(test.StylesSheet);
                await db.SaveChangesAsync();
                return Results.Ok();
            }
        }
        public static IResult GetDefaultStylesData(IDbContextFactory<AppDbContext> dbFactory) {
            var stylesWithDefaultVals = TestStylesSheet.CreateNew();
            var dto = TestStylesDataDto.FromTestStylesSheet(stylesWithDefaultVals);
            return Results.Ok(dto);
        }
    }
}
