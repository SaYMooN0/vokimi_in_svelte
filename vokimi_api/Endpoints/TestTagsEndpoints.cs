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
                var responseTags = test.SuggestedTags
                    .OrderByDescending(tag => tag.SuggestionsCount)
                    .ToArray();

                int count = responseTags.Length;
                int middleSuggestionsCount = count > 0 ? responseTags[count / 2].SuggestionsCount : 0;

                int responseTagsCount = (count, middleSuggestionsCount) switch {
                    ( > 15, > 20) => 7,
                    ( > 12, > 15) => 5,
                    ( > 7, > 10) => 4,
                    ( > 3, > 5) => 3,
                    _ => 0
                };
                return Results.Ok(responseTags.Take(responseTagsCount));
            }
        }
    }
}