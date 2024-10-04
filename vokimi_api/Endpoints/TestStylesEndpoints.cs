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

namespace vokimi_api.Endpoints
{
    public static class TestStylesEndpoints
    {
        public static IResult GetDraftTestStylesData(IDbContextFactory<AppDbContext> dbFactory, string testId) {

            DraftTestId draftTestId;
            if (!Guid.TryParse(testId, out _)) {
                return ResultsHelper.BadRequestServerError();
            }
            draftTestId = new(new(testId));

            using (var db = dbFactory.CreateDbContext()) {
                BaseDraftTest? test = db.DraftTestsSharedInfo
                        .Include(t => t.StylesSheet)
                        .FirstOrDefault(t => t.Id == draftTestId);
                if (test is null) { return ResultsHelper.BadRequestServerError(); }
                return Results.Ok(TestStylesDataDto.FromTestStylesSheet(test.StylesSheet));
            }
        }
        public static IResult UpdateDraftTestStylesData(IDbContextFactory<AppDbContext> dbFactory,
                                                        [FromBody] TestStylesDataDto data,
                                                        string testId) {
            Err validationErr = data.CheckForErr();
            if (validationErr.NotNone()) {
                return ResultsHelper.BadRequestWithErr(validationErr);
            }
            (string accentColor, ArrowIconType arrowType)? dataWithTypes = data.GetDataWithTypes();
            if (dataWithTypes is null) {
                return ResultsHelper.BadRequestServerError();
            }
            DraftTestId draftTestId;
            if (!Guid.TryParse(testId, out _)) {
                return ResultsHelper.BadRequestServerError();
            }
            draftTestId = new(new(testId));
            using (var db = dbFactory.CreateDbContext()) {
                BaseDraftTest? test = db.DraftTestsSharedInfo
                        .Include(t => t.StylesSheet)
                        .FirstOrDefault(t => t.Id == draftTestId);
                if (test is null) {
                    return ResultsHelper.BadRequestServerError();
                }
                test.StylesSheet.Update(
                    dataWithTypes.Value.accentColor,
                    dataWithTypes.Value.arrowType
                );
                db.TestStyles.Update(test.StylesSheet);
                db.SaveChanges();
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
