using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vokimi_api.Helpers;
using vokimi_api.Src.constants_store_classes;
using vokimi_api.Src.db_related;
using vokimi_api.Src.db_related.db_entities.draft_tests.draft_tests_shared;
using vokimi_api.Src.db_related.db_entities.published_tests.published_tests_shared;
using vokimi_api.Src.db_related.db_entities.tests_related.tags;
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
        public static async Task<IResult> SuggestTagsForTest(
            string testIdString,
            [FromBody] string[] tags,
            IDbContextFactory<AppDbContext> dbFactory
        ) {

            if (!Guid.TryParse(testIdString, out var testGuid)) {
                return ResultsHelper.BadRequest.ServerError();
            }
            foreach (var tag in tags) {
                if (!TestTagsConsts.TagRegex.IsMatch(tag)) {
                    return ResultsHelper.BadRequest.WithErr(TestTagsConsts.InvalidTagMessage(tag));
                }
            }
            TestId testId = new(testGuid);
            using (var db = await dbFactory.CreateDbContextAsync()) {
                BaseTest? test = await db.TestsSharedInfo
                    .Include(t => t.Tags)
                    .Include(t => t.SuggestedTags)
                    .FirstOrDefaultAsync();
                if (test is null) {
                    return ResultsHelper.BadRequest.UnknownTest();
                }

                HashSet<string> tagsInTest = test.Tags
                    .Select(t => t.Value)
                    .ToHashSet();
                HashSet<string> tagsToAdd = tags
                    .Where(t => !tagsInTest.Contains(t))
                    .ToHashSet();

                if (tagsToAdd.Count == 0) {
                    return ResultsHelper.BadRequest.WithErr("Test already has all these tags");
                }
                foreach (var suggested in tagsToAdd) {
                    TagSuggestionForTest? existingSuggestion = test.SuggestedTags.FirstOrDefault(t => t.Value == suggested);
                    if (existingSuggestion is null) {
                        var newSuggestion = TagSuggestionForTest.CreateNew(suggested, testId);
                        await db.AddAsync(newSuggestion);
                        test.SuggestedTags.Add(newSuggestion);
                    } else {
                        existingSuggestion.IncreaseSuggestionsCount();
                    }
                }
                await db.SaveChangesAsync();
                return Results.Ok();
            }

        }
        public static async Task<IResult> GetMostSuggestedTags(
            string testIdString,
            IDbContextFactory<AppDbContext> dbFactory
        ) {

            if (!Guid.TryParse(testIdString, out var testGuid)) {
                return ResultsHelper.BadRequest.ServerError();
            }
            TestId testId = new(testGuid);
            using (var db = await dbFactory.CreateDbContextAsync()) {
                BaseTest? test = await db.TestsSharedInfo
                    .Include(t => t.SuggestedTags)
                    .FirstOrDefaultAsync(t => t.Id == testId);
                if (test is null) {
                    return ResultsHelper.BadRequest.WithErr("Test not found");
                }
                var testTags = test.SuggestedTags
                    .OrderByDescending(tag => tag.SuggestionsCount)
                    .ToArray();

                int count = testTags.Length;

                int midIndex = (int)Math.Floor(Math.Sqrt(count));
                int midCount = count > 0 ? testTags[midIndex].SuggestionsCount : 0;

                int responseTagsCount = (count, midCount) switch {
                    ( > 15, > 2) => 7,
                    ( > 12, > 2) => 5,
                    ( > 7, > 1) => 4,
                    ( > 3, > 1) => 3,
                    _ => 0
                };
                var response = testTags
                    .Take(responseTagsCount)
                    .Select(t => t.Value)
                    .ToArray();
                return Results.Ok(response);
            }
        }
    }
}