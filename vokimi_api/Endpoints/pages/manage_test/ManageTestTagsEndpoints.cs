using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vokimi_api.Helpers;
using vokimi_api.Src.constants_store_classes;
using vokimi_api.Src.db_related;
using vokimi_api.Src.db_related.db_entities.published_tests.published_tests_shared;
using vokimi_api.Src.db_related.db_entities.tests_related.tags;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.dtos.requests.manage_test;
using vokimi_api.Src.dtos.responses.manage_test_page.tags;
using vokimi_api.Src.extension_classes;

namespace vokimi_api.Endpoints.pages.manage_test
{
    internal static class ManageTestTagsEndpoints
    {
        public static async Task<IResult> AcceptSuggestedTag(
            [FromBody] TagSuggestionOperationRequest request,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {
            if (!Guid.TryParse(request.TestId, out var testGuid)) {
                return ResultsHelper.BadRequest.WithErr("Incorrect request. Try again later");
            }
            if (!Guid.TryParse(request.TagSuggestionId, out var tagSuggestionGuid)) {
                return ResultsHelper.BadRequest.WithErr("Incorrect request. Try again later");
            }
            TestId testId = new(testGuid);
            TagSuggestionForTestId tagSuggestionForTestId = new(tagSuggestionGuid);
            using (var db = await dbFactory.CreateDbContextAsync()) {

                BaseTest? test = await db.TestsSharedInfo
                    .Include(t => t.Tags)
                    .Include(t => t.SuggestedTags)
                    .FirstOrDefaultAsync(t => t.Id == testId);
                if (test is null) {
                    return ResultsHelper.BadRequest.UnknownTest();
                }
                if (!httpContext.IsAuthenticatedUserIsTestCreator(test)) {
                    return ResultsHelper.BadRequest.WithErr("You cannot accept tags suggestion. You need to be creator of the test");
                }
                TagSuggestionForTest? suggestion = test.SuggestedTags.FirstOrDefault(t => t.Id == tagSuggestionForTestId);
                if (suggestion is null) {
                    return ResultsHelper.BadRequest.WithErr("This test does not have this suggestion");
                }
                if (test.Tags.Any(t => t.Value == suggestion.Value)) {
                    return ResultsHelper.BadRequest.WithErr($"This test already has '{suggestion.Value}' tag");
                }
                test.SuggestedTags.Remove(suggestion);
                TestTag? tagToAdd = await db.TestTags.FirstOrDefaultAsync(t => t.Value == suggestion.Value);
                if (tagToAdd is null) {
                    tagToAdd = TestTag.CreateNew(suggestion.Value);
                    await db.AddAsync(tagToAdd);
                }
                test.Tags.Add(tagToAdd);
                await db.SaveChangesAsync();
                return Results.Ok(new { AcceptedTagValue = tagToAdd.Value });
            }
        }
        public static async Task<IResult> DeclineSuggestedTag(
            [FromBody] TagSuggestionOperationRequest request,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {
            if (!Guid.TryParse(request.TestId, out var testGuid)) {
                return ResultsHelper.BadRequest.WithErr("Incorrect request. Try again later");
            }
            if (!Guid.TryParse(request.TagSuggestionId, out var tagSuggestionGuid)) {
                return ResultsHelper.BadRequest.WithErr("Incorrect request. Try again later");
            }
            TestId testId = new(testGuid);
            TagSuggestionForTestId tagSuggestionForTestId = new(tagSuggestionGuid);
            using (var db = await dbFactory.CreateDbContextAsync()) {

                BaseTest? test = await db.TestsSharedInfo
                    .Include(t => t.SuggestedTags)
                    .FirstOrDefaultAsync(t => t.Id == testId);
                if (test is null) {
                    return ResultsHelper.BadRequest.UnknownTest();
                }
                if (!httpContext.IsAuthenticatedUserIsTestCreator(test)) {
                    return ResultsHelper.BadRequest.WithErr("You cannot accept tags suggestion. You need to be creator of the test");
                }
                TagSuggestionForTest? suggestionToRemove = test.SuggestedTags.FirstOrDefault(t => t.Id == tagSuggestionForTestId);
                if (suggestionToRemove is null) {
                    return ResultsHelper.BadRequest.WithErr("This test does not have this suggestion");
                }
                test.SuggestedTags.Remove(suggestionToRemove);
                await db.SaveChangesAsync();
                return Results.Ok();
            }
        }
        public static async Task<IResult> BanSuggestedTag([FromBody] TagSuggestionOperationRequest request) {
            return ResultsHelper.BadRequest.WithErr("Not implemented");
        }
        internal static async Task<IResult> UpdateTestTags(
            string testIdString,
            string[] newTags,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {
            if (!Guid.TryParse(testIdString, out var testGuid)) {
                return ResultsHelper.BadRequest.UnknownTest();
            }
            TestId testId = new(testGuid);
            var newTagsSet = newTags.ToHashSet();
            foreach (var tag in newTagsSet) {
                if (!TestTagsConsts.TagRegex.IsMatch(tag)) {
                    return ResultsHelper.BadRequest.WithErr(TestTagsConsts.InvalidTagMessage(tag));
                }
            }
            using (var db = await dbFactory.CreateDbContextAsync()) {

                BaseTest? t = await db.TestsSharedInfo
                    .Include(t => t.Tags)
                    .Include(t => t.SuggestedTags)
                    .FirstOrDefaultAsync(t => t.Id == testId);
                if (t is null) {
                    return ResultsHelper.BadRequest.UnknownTest();
                }
                if (!httpContext.IsAuthenticatedUserIsTestCreator(t)) {
                    return ResultsHelper.BadRequest.WithErr("You don't have access to this page");
                }
                t.Tags.Clear();
                var testSuggestedTags = t.SuggestedTags.ToDictionary(st => st.Value, st => st);
                foreach (var tag in newTagsSet) {
                    TestTag? tagToAdd = await db.TestTags.FirstOrDefaultAsync(t => t.Value == tag);
                    if (tagToAdd is null) {
                        tagToAdd = TestTag.CreateNew(tag);
                        await db.AddAsync(tagToAdd);
                    }
                    t.Tags.Add(tagToAdd);
                    if (testSuggestedTags.TryGetValue(tag, out var suggestedTag)) {
                        t.SuggestedTags.Remove(suggestedTag);
                    }
                }
                await db.SaveChangesAsync();
                return Results.Ok();
            }
        }
        internal static async Task<IResult> GetTabData(
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
                    .Include(t => t.Tags)
                    .Include(t => t.SuggestedTags)
                    .FirstOrDefaultAsync(t => t.Id == testId);
                if (t is null) {
                    return ResultsHelper.BadRequest.UnknownTest();
                }
                if (!httpContext.IsAuthenticatedUserIsTestCreator(t)) {
                    return ResultsHelper.BadRequest.WithErr("You don't have access to this page");
                }
                return Results.Ok(ManageTestTagsTabDataResponse.FromTest(t));
            }
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
                await db.SaveChangesAsync();
                return Results.Ok(
                    new {
                        TagsSuggestions = t.SuggestedTags.Select(TagSuggestionForTestData.FromTagSuggestions),
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
                await db.SaveChangesAsync();
                return Results.Ok(new { TagsSuggestionsAllowed = t.Settings.TagsSuggestionsAllowed, });

            }
        }
    }
}
