using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using vokimi_api.Helpers;
using vokimi_api.Src.constants_store_classes;
using vokimi_api.Src.db_related;
using vokimi_api.Src.db_related.db_entities.draft_tests.draft_tests_shared;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.dtos.responses.test_creation_responses.shared;
using vokimi_api.Src.dtos.shared.test_creation_shared;

namespace vokimi_api.Endpoints
{
    public static class TestTagsEndpoints
    {
        public static IResult GetDraftTestTagsData(IDbContextFactory<AppDbContext> dbFactory,
                                                   string testId) {
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
                return Results.Ok(DraftTestTagsDataResponse.FromDraftTest(test));
            }
        }
        public static IResult SearchTags(IDbContextFactory<AppDbContext> dbFactory,
                                         string tagToSearch) {

            Regex tagRegex = new Regex(@"^[a-zA-Zа-яА-Я0-9]{1," + TestTagsConsts.MaxTagLength + "}$");


            if (string.IsNullOrEmpty(tagToSearch)) {
                return Results.Ok(Array.Empty<string>());
            }
            if (!tagRegex.IsMatch(tagToSearch)) {
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
        public static IResult UpdateDraftTestTags(IDbContextFactory<AppDbContext> dbFactory,
                                                  string testId,
                                                  [FromBody] List<string> tags) {
            DraftTestId draftTestId;
            if (!Guid.TryParse(testId, out _)) {
                return ResultsHelper.BadRequestServerError();
            }
            draftTestId = new(new(testId));

            try {
                using (var db = dbFactory.CreateDbContext()) {
                    BaseDraftTest? test = db.DraftTestsSharedInfo.FirstOrDefault(t => t.Id == draftTestId);
                    if (test is null) {
                        return ResultsHelper.BadRequestUnknownTest();
                    }
                    test.SetTags(tags);
                    db.SaveChanges();
                    return Results.Ok(DraftTestTagsDataResponse.FromDraftTest(test));
                }
            } catch {
                return ResultsHelper.BadRequestServerError();
            }
        }
    }
}
