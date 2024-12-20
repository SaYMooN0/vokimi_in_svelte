using Microsoft.EntityFrameworkCore;
using vokimi_api.Helpers;
using vokimi_api.Src.db_related;
using vokimi_api.Src.db_related.db_entities.published_tests.published_tests_shared;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.extension_classes;

namespace vokimi_api.Endpoints.pages.manage_test
{
    internal static class ManageTestTagsEndpoints
    {
        public static async Task<IResult> AcceptSuggestedTag() {
            throw new NotImplementedException();
        }
        internal static async Task<IResult> GetTabData() {

        }
        internal static async Task<IResult> EnableTestTagsSuggestions(
            string testIdString,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {
            if (!Guid.TryParse(testIdString, out var testGuid)) {
                return ResultsHelper.BadRequest.UnknownTest();
            }
            TestId testId = new(testGuid);
            using (var db = await dbFactory.CreateDbContextAsync()) {
                BaseTest? t = await db.TestsSharedInfo
                    .Include(t => t.SuggestedTags)
                    .FirstOrDefaultAsync(t => t.Id == testId);
                if (t is null) {
                    return ResultsHelper.BadRequest.UnknownTest();
                }
                if (!httpContext.IsAuthenticatedUserIsTestCreator(t)) {
                    return ResultsHelper.BadRequest.WithErr("You don't have access to this page");
                }
                if (t.Settings.TagsSuggestionsAllowed) {
                    return ResultsHelper.BadRequest.WithErr("Tags suggestions for this test are already enabled");
                }
                t.UpdateSettings(t.Settings with { TagsSuggestionsAllowed = true });
                return Results.Ok(
                    new {
                        TagsSuggestions = t.,
                        TagsSuggestionsAllowed = t.Settings.TagsSuggestionsAllowed,
                    }
                );

            }
        }
        internal static async Task<IResult> DisableTestTagsSuggestions(
            string testIdString,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {
            if (!Guid.TryParse(testIdString, out var testGuid)) {
                return ResultsHelper.BadRequest.UnknownTest();
            }
            TestId testId = new(testGuid);
            using (var db = await dbFactory.CreateDbContextAsync()) {
                BaseTest? t = await db.TestsSharedInfo.FindAsync(testId);
                if (t is null) {
                    return ResultsHelper.BadRequest.UnknownTest();
                }
                if (!httpContext.IsAuthenticatedUserIsTestCreator(t)) {
                    return ResultsHelper.BadRequest.WithErr("You don't have access to this page");
                }
                if (!t.Settings.TagsSuggestionsAllowed) {
                    return ResultsHelper.BadRequest.WithErr("Tags suggestions for this test are already disabled");
                }
                t.UpdateSettings(t.Settings with { TagsSuggestionsAllowed = false });
                return Results.Ok(new { TagsSuggestionsAllowed = t.Settings.TagsSuggestionsAllowed });

            }
        }
    }
}
