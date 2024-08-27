using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Security.Claims;
using vokimi_api.Src.db_related;
using vokimi_api.Src.db_related.db_entities.draft_published_tests_shared;
using vokimi_api.Src.db_related.db_entities.draft_tests.draft_general_test;
using vokimi_api.Src.db_related.db_entities.draft_tests.draft_tests_shared;
using vokimi_api.Src.db_related.db_entities.users;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.dtos.responses;
using vokimi_api.Src.dtos.responses.test_creation_responses.shared;
using vokimi_api.Src.enums;

namespace vokimi_api.Endpoints.tests_operations.test_creation
{
    public static class TestCreationSharedEndpoints
    {
        public static async Task<IResult> CreateNewTest(HttpContext httpContext,
                                                        IDbContextFactory<AppDbContext> dbFactory,
                                                        TestTemplate template) {

            IResult authErrResponse = Results.BadRequest(new { Error = "Please log out and log in again" });

            var cntxUser = httpContext.User;

            if (cntxUser.Identity == null || !cntxUser.Identity.IsAuthenticated) {
                return authErrResponse;
            }
            string? userIdStr = cntxUser.FindFirstValue(PingAuthResponse.ClaimKeyUserId);
            if (string.IsNullOrEmpty(userIdStr)) { return authErrResponse; }

            AppUserId userId;

            if (Guid.TryParse(userIdStr, out Guid userGuid)) {
                userId = new(userGuid);
            } else { return authErrResponse; }

            using (var db = dbFactory.CreateDbContext()) {
                using (var transaction = await db.Database.BeginTransactionAsync()) {
                    try {
                        AppUser? user = db.AppUsers.FirstOrDefault(u => u.Id == userId);
                        if (user is null) { return authErrResponse; }

                        DraftTestId? testId = template switch {
                            TestTemplate.General => await CreateNewGeneralTest(db, userId),
                            _ => null
                        };
                        if (testId is null) { throw new Exception(); }

                        await transaction.CommitAsync();
                        return Results.Ok(new { TestId = testId.ToString() });
                    } catch {
                        transaction.Rollback();
                        return Results.BadRequest(new { Error = "Something went wrong. Please try again later" });
                    }
                }
            }
        }

        private static async Task<DraftTestId> CreateNewGeneralTest(AppDbContext db, AppUserId creatorId) {
            DraftTestMainInfo mainInfo = DraftTestMainInfo.CreateNewFromName("New Draft General Test");
            TestStylesSheet styles = TestStylesSheet.CreateNew();
            BaseDraftTest test = DraftGeneralTest.CreateNew(creatorId, mainInfo.Id, styles.Id);

            db.DraftTestMainInfo.Add(mainInfo);
            db.TestStyles.Add(styles);
            db.DraftTestsSharedInfo.Add(test);

            await db.SaveChangesAsync();
            return test.Id;
        }

        public static IResult GetDraftTestMainInfoData(IDbContextFactory<AppDbContext> dbFactory, string testId) {
            if (string.IsNullOrEmpty(testId)) { return Results.BadRequest(); }

            DraftTestId draftTestId;
            if (!Guid.TryParse(testId, out _)) {
                return Results.BadRequest();
            }
            draftTestId = new(new(testId));

            using (var db = dbFactory.CreateDbContext()) {
                BaseDraftTest? test = db.DraftTestsSharedInfo
                        .Include(t => t.MainInfo)
                        .FirstOrDefault(t => t.Id == draftTestId);
                if (test is null || test.MainInfo is null) { return Results.BadRequest(); }
                return Results.Ok(DraftTestMainInfoDataResponse.FromDraftTest(test));
            }
        }
    }
}