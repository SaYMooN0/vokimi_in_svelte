using Microsoft.EntityFrameworkCore;
using vokimi_api.Helpers;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.db_related;
using VokimiShared.src.models.db_classes.test.test_types;
using vokimi_api.Src.extension_classes;
using vokimi_api.Src.enums;
using vokimi_api.Src.db_related.db_entities.users;
using vokimi_api.Src.dtos.responses;
using static System.Net.Mime.MediaTypeNames;
using vokimi_api.Src.dtos.responses.view_test_page;
using vokimi_api.Src.dtos.requests;
using Microsoft.AspNetCore.Mvc;
using vokimi_api.Src.db_related.db_entities.tests_related;

namespace vokimi_api.Endpoints.pages
{
    public static class ViewTestPageEndpoints
    {
        public static IResult CheckTestViewPermission(
            string testId,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {
            TestId tId;
            if (!Guid.TryParse(testId, out var testGuid)) {
                return ResultsHelper.BadRequestServerError();
            }

            tId = new(testGuid);

            using (var db = dbFactory.CreateDbContext()) {
                BaseTest? test = db.TestsSharedInfo.Find(tId);
                if (test is null) {
                    return Results.Ok(ViewTestAccessCheckResponse.TestNotFound());
                }
                bool haveAccess;
                if (httpContext.TryGetUserId(out AppUserId viewerId)) {
                    haveAccess = TestAccessValidator.CheckUserAccessToTest(db, test.CreatorId, test.Settings.Privacy, viewerId);
                } else {
                    haveAccess = test.Settings.Privacy == PrivacyValues.Anyone;
                }
                if (haveAccess) {
                    return Results.Ok(ViewTestAccessCheckResponse.Success());
                } else {
                    AppUser creator = db.AppUsers.Find(test.CreatorId);
                    ViewTestAccessCheckResponse returnRes = test.Settings.Privacy switch {
                        PrivacyValues.FriendsAndFollowers => ViewTestAccessCheckResponse.FollowingNeeded(creator),
                        PrivacyValues.FriendsOnly => ViewTestAccessCheckResponse.FriendshipNeeded(creator),
                        _ => ViewTestAccessCheckResponse.Denied(),
                    };
                    return Results.Ok(returnRes);
                }

            }
        }
        public static IResult GetBasicTestInfo(
            string testId,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {
            TestId tId;
            if (!Guid.TryParse(testId, out var testGuid)) {
                return ResultsHelper.BadRequestServerError();
            }

            tId = new(testGuid);
            using (var db = dbFactory.CreateDbContext()) {
                BaseTest? test = db.TestsSharedInfo
                    .Include(t => t.Tags)
                    .Include(t => t.Creator)
                    .FirstOrDefault(t => t.Id == tId);
                if (test is null) {
                    return ResultsHelper.BadRequestUnknownTest();
                }
                bool haveAccess;
                if (httpContext.TryGetUserId(out AppUserId viewerId)) {
                    haveAccess = TestAccessValidator.CheckUserAccessToTest(db, test.CreatorId, test.Settings.Privacy, viewerId);
                } else {
                    haveAccess = test.Settings.Privacy == PrivacyValues.Anyone;
                }
                if (!haveAccess) {
                    return ResultsHelper.BadRequestNoTestAccess();
                }
                return Results.Ok(ViewTestBasicTestInfoResponse.FromTest(test));
            }
        }
        public static IResult GetTestRatingsInfo(
            string testId,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {
            TestId tId;
            if (!Guid.TryParse(testId, out var testGuid)) {
                return ResultsHelper.BadRequestServerError();
            }

            tId = new(testGuid);
            using (var db = dbFactory.CreateDbContext()) {
                BaseTest? test = db.TestsSharedInfo
                    .Include(t => t.Ratings)
                    .FirstOrDefault(t => t.Id == tId);
                if (test is null) {
                    return ResultsHelper.BadRequestUnknownTest();
                }
                bool haveAccess;
                if (httpContext.TryGetUserId(out AppUserId viewerId)) {
                    haveAccess = TestAccessValidator.CheckUserAccessToTest(db, test.CreatorId, test.Settings.Privacy, viewerId);
                } else {
                    haveAccess = test.Settings.Privacy == PrivacyValues.Anyone;
                }
                if (!haveAccess) {
                    return ResultsHelper.BadRequestNoTestAccess();
                }
                return ResultsHelper.BadRequestWithErr("Not implemented");
            }
        }
        public static IResult UpdateTestRating(
            [FromBody] TestRatingUpdateRequest request,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {

            var requestErr = request.CheckForErr();
            if (requestErr.NotNone()) {
                return ResultsHelper.BadRequestWithErr(requestErr);
            }
            try {
                using (var db = dbFactory.CreateDbContext()) {
                    TestId testId = request.GetParsedTestId().Value;
                    BaseTest? test = db.TestsSharedInfo
                        .Include(t => t.Ratings)
                        .FirstOrDefault(t => t.Id == testId);
                    if (test is null) {
                        return ResultsHelper.BadRequestUnknownTest();
                    }
                    if (!test.Settings.EnableTestRatings) {
                        return ResultsHelper.BadRequestWithErr("Ratings for this test are disabled");
                    }
                    bool haveAccess;
                    if (httpContext.TryGetUserId(out AppUserId viewerId)) {
                        haveAccess = TestAccessValidator.CheckUserAccessToTest(db, test.CreatorId, test.Settings.Privacy, viewerId);
                    } else {
                        haveAccess = test.Settings.Privacy == PrivacyValues.Anyone;
                    }
                    if (!haveAccess) {
                        return ResultsHelper.BadRequestNoTestAccess();
                    }
                    AppUser? viewer = db.AppUsers
                        .Include(u => u.TestRatings)
                        .FirstOrDefault(u => u.Id == viewerId);
                    if (viewer is null) {
                        return ResultsHelper.BadRequestWithErr("An error has occurred. Please log out, login and try again");
                    }
                    TestRating? existingRating = viewer.TestRatings.FirstOrDefault(r => r.TestId == testId);
                    if (existingRating is null) {
                        db.TestRatings.Add(TestRating.CreateNew(testId, viewerId, request.Rating));
                    } else {
                        existingRating.UpdateRatingValue(request.Rating);
                        db.Update(existingRating);
                    }
                    db.SaveChanges();
                    return Results.Ok();
                }
            } catch {
                return ResultsHelper.BadRequestServerError();
            }
        }
    }
}
