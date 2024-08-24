using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using vokimi_api.Src.db_related;
using vokimi_api.Src.db_related.db_entities.draft_published_tests_shared;
using vokimi_api.Src.db_related.db_entities.draft_tests.draft_general_test;
using vokimi_api.Src.db_related.db_entities.draft_tests.draft_tests_shared;
using vokimi_api.Src.db_related.db_entities.users;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.dtos.responses;
using vokimi_api.Src.enums;

namespace vokimi_api.Endpoints
{
    public class UserTestsEndpoints
    {
        private static readonly IResult authErrResponse =
            Results.BadRequest(new { Error = "Please log out and log in again" });


        //TODO: Rewrite to first package and remaining
        public static async Task<IResult> GetUsersDraftTestsVms(
            HttpContext httpContext, IDbContextFactory<AppDbContext> dbFactory) {
            var cntxUser = httpContext.User;

            if (cntxUser.Identity?.IsAuthenticated ?? false) {
                string? userIdStr = cntxUser.FindFirstValue(PingAuthResponse.ClaimKeyUserId);

                AppUserId? userId = null;
                if (!string.IsNullOrEmpty(userIdStr)) {
                    userId = new(new Guid(userIdStr));
                } else {
                    return Results.Unauthorized();
                }
                using (var db = dbFactory.CreateDbContext()) {
                    AppUser? user = await db.AppUsers
                        .Include(u => u.DraftTests)
                        .FirstOrDefaultAsync(u => u.Id == userId);
                    if (user is null) {
                        return Results.BadRequest(new { Err = "Unknown user" });
                    }
                    return Results.Ok(user.DraftTests.Select(UsersTestsVm.FromDraftTest));
                }
            } else {
                return Results.BadRequest();
            }
        }
        public static async Task<IResult> GetUsersPublishedTestsVms(
            HttpContext httpContext, IDbContextFactory<AppDbContext> dbFactory) {
            var user = httpContext.User;

            if (user.Identity?.IsAuthenticated ?? false) {
                string? userIdStr = user.FindFirstValue(PingAuthResponse.ClaimKeyUserId);

                AppUserId? userId = null;
                if (!string.IsNullOrEmpty(userIdStr)) {
                    userId = new(new Guid(userIdStr));
                }


                return TypedResults.Ok(new UsersTestsVm[] { });
            } else {
                return TypedResults.BadRequest();
            }
        }

        public static async Task<IResult> CreateNewTest(
            HttpContext httpContext,
            IDbContextFactory<AppDbContext> dbFactory,
            TestTemplate template) {
            var cntxUser = httpContext.User;
            if (cntxUser.Identity?.IsAuthenticated ?? false) {
                string? userIdStr = cntxUser.FindFirstValue(PingAuthResponse.ClaimKeyUserId);

                if (string.IsNullOrEmpty(userIdStr)) {
                    return authErrResponse;
                }
                AppUserId userId;
                if (Guid.TryParse(userIdStr, out Guid userGuid)) {
                    userId = new(userGuid);
                } else { return authErrResponse; }

                using (var db = dbFactory.CreateDbContext()) {
                    using (var transaction = await db.Database.BeginTransactionAsync()) {
                        try {
                            AppUser? user = db.AppUsers.FirstOrDefault(u => u.Id == userId);
                            if (user is null) {
                                return authErrResponse;
                            }

                            DraftTestMainInfo mainInfo = DraftTestMainInfo.CreateNewFromName("Draft General Test");
                            TestStylesSheet styles = TestStylesSheet.CreateNew();

                            BaseDraftTest test = template switch {
                                TestTemplate.General => DraftGeneralTest.CreateNew(user.Id, mainInfo.Id, styles.Id),
                                TestTemplate.Knowledge =>
                                throw new NotImplementedException("Knowledge type not implemented yet"),
                                _ => throw new ArgumentException("incorrect type")
                            };
                            db.DraftTestMainInfo.Add(mainInfo);
                            db.TestStyles.Add(styles);
                            db.DraftTestsSharedInfo.Add(test);
                            await db.SaveChangesAsync();
                            return Results.Ok(new { TestId = test.Id.ToString() });
                        } catch {
                            transaction.Rollback();
                            return Results.BadRequest(new { Error = "Something went wrong. Please try again later" });
                        }
                    }
                }


            } else {
                return authErrResponse;
            }
        }
    }
}
