using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vokimi_api.Helpers;
using vokimi_api.Src.constants_store_classes;
using vokimi_api.Src.db_related;
using vokimi_api.Src.db_related.db_entities.draft_tests.draft_tests_shared;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.dtos.responses.test_creation_responses.shared;
using vokimi_api.Src.extension_classes;

namespace vokimi_api.Endpoints
{
    public static class TestTagsEndpoints
    {
        public static async Task<IResult> GetDraftTestTagsData(
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext,
            string testId
        ) {
            if (!Guid.TryParse(testId, out var testGuid)) {
                return ResultsHelper.BadRequest.ServerError();
            }
            DraftTestId draftTestId = new(testGuid);

            using (var db = await dbFactory.CreateDbContextAsync()) {
                BaseDraftTest? test = await db.DraftTestsSharedInfo.FindAsync(draftTestId);
                if (test is null) {
                    return ResultsHelper.BadRequest.UnknownTest();
                }
                return httpContext.IsAuthenticatedUserIsTestCreator(test) ?
                     Results.Ok(DraftTestTagsDataResponse.FromDraftTest(test)) :
                     ResultsHelper.BadRequest.NotCreator();
            }
        }
        public static async Task<IResult> SearchTags(
            IDbContextFactory<AppDbContext> dbFactory,
            string tagToSearch
        ) {
            if (string.IsNullOrEmpty(tagToSearch)) {
                return Results.Ok(Array.Empty<string>());
            }
            if (!TestTagsConsts.TagRegex.IsMatch(tagToSearch)) {
                return ResultsHelper.BadRequest
                    .WithErr($"Invalid tag. Tag must contain only Cyrillic and Latin letters, digits and '-', '+', '_'. Test cannot be longer than {TestTagsConsts.MaxTagLength} characters.");
            }
            using (var db = await dbFactory.CreateDbContextAsync()) {
                var tags = await db.TestTags
                    .Where(t => t.Value.Contains(tagToSearch) && t.Value != tagToSearch)
                    .Take(16)
                    .Select(t => t.Value)
                    .ToListAsync();
                tags.Insert(0, tagToSearch);
                return Results.Ok(tags);
            }

        }
        public static async Task<IResult> UpdateDraftTestTags(
            string testId,
            [FromBody] List<string> tags,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {
            if (tags.Count > TestTagsConsts.MaxTagsForTestCount) {
                return ResultsHelper.BadRequest.WithErr($"Tags count must not exceed {TestTagsConsts.MaxTagsForTestCount}");
            }

            foreach (var tag in tags) {
                if (!TestTagsConsts.TagRegex.IsMatch(tag)) {
                    return ResultsHelper.BadRequest.WithErr(TestTagsConsts.InvalidTagMessage(tag));
                }
            }

            if (!Guid.TryParse(testId, out var testGuid)) {
                return ResultsHelper.BadRequest.ServerError();
            }
            DraftTestId draftTestId = new(testGuid);

            try {
                using (var db = await dbFactory.CreateDbContextAsync()) {
                    var test = await db.DraftTestsSharedInfo.FindAsync(draftTestId);

                    if (test is null) {
                        return ResultsHelper.BadRequest.UnknownTest();
                    }

                    if (!httpContext.IsAuthenticatedUserIsTestCreator(test)) {
                        return ResultsHelper.BadRequest.NotCreator();
                    }

                    test.SetTags(tags);
                    await db.SaveChangesAsync();

                    return Results.Ok(DraftTestTagsDataResponse.FromDraftTest(test));
                }
            } catch {
                return ResultsHelper.BadRequest.ServerError();
            }
        }

    }
}
