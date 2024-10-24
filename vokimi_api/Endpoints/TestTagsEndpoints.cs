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
        public static IResult GetDraftTestTagsData(
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext,
            string testId
        ) {
            DraftTestId draftTestId;
            if (!Guid.TryParse(testId, out _)) {
                return ResultsHelper.BadRequestServerError();
            }
            draftTestId = new(new(testId));

            using (var db = dbFactory.CreateDbContext()) {
                BaseDraftTest? test = db.DraftTestsSharedInfo.FirstOrDefault(t => t.Id == draftTestId);
                if (test is null) {
                    return ResultsHelper.BadRequestUnknownTest();
                }
                return httpContext.IfAuthenticatedUserIdIsTestCreator(test) ?
                     Results.Ok(DraftTestTagsDataResponse.FromDraftTest(test)) :
                     ResultsHelper.BadRequestNotCreator();
            }
        }
        public static IResult SearchTags(IDbContextFactory<AppDbContext> dbFactory,
                                         string tagToSearch) {




            if (string.IsNullOrEmpty(tagToSearch)) {
                return Results.Ok(Array.Empty<string>());
            }
            if (!TestTagsConsts.TagRegex.IsMatch(tagToSearch)) {
                return ResultsHelper.BadRequestWithErr($"Invalid tag. Tag must contain only Cyrillic, Latin letters or digits and be no longer than {TestTagsConsts.MaxTagLength} characters.");
            }
            using (var db = dbFactory.CreateDbContext()) {
                var tags = db.TestTags
                    .Where(t => t.Value.Contains(tagToSearch) && t.Value != tagToSearch)
                    .Take(16)
                    .Select(t => t.Value)
                    .ToList();
                tags.Insert(0, tagToSearch);
                return Results.Ok(tags);
            }

        }
        public static IResult UpdateDraftTestTags(
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext,
            string testId,
            [FromBody] List<string> tags
        ) {
            if (tags.Count > TestTagsConsts.MaxTagsForTestCount) {
                return ResultsHelper.BadRequestWithErr($"Tags count must not exceed {TestTagsConsts.MaxTagsForTestCount}");
            }

            foreach (var tag in tags) {
                if (!TestTagsConsts.TagRegex.IsMatch(tag)) {
                    return ResultsHelper.BadRequestWithErr(TestTagsConsts.InvalidTagMessage(tag));
                }
            }

            if (!Guid.TryParse(testId, out var _)) {
                return ResultsHelper.BadRequestServerError();
            }

            DraftTestId draftTestId = new(new(testId));

            try {
                using (var db = dbFactory.CreateDbContext()) {
                    var test = db.DraftTestsSharedInfo.FirstOrDefault(t => t.Id == draftTestId);

                    if (test is null) {
                        return ResultsHelper.BadRequestUnknownTest();
                    }

                    if (!httpContext.IfAuthenticatedUserIdIsTestCreator(test)) {
                        return ResultsHelper.BadRequestNotCreator();
                    }

                    test.SetTags(tags);
                    db.SaveChanges();

                    return Results.Ok(DraftTestTagsDataResponse.FromDraftTest(test));
                }
            } catch (Exception) {
                return ResultsHelper.BadRequestServerError();
            }
        }

    }
}
