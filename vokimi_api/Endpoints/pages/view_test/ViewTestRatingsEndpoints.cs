using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vokimi_api.Helpers;
using vokimi_api.Src.db_related.db_entities.tests_related;
using vokimi_api.Src.db_related.db_entities.users;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.db_related;
using vokimi_api.Src.enums;
using vokimi_api.Src.extension_classes;
using vokimi_api.Src;
using vokimi_api.Src.dtos.requests.view_test_page.ratings;
using vokimi_api.Src.dtos.responses.view_test_page;
using vokimi_api.Src.db_related.db_entities.published_tests.published_tests_shared;

namespace vokimi_api.Endpoints.pages.view_test
{
    internal static class ViewTestRatingsEndpoints
    {
        internal static async Task<IResult> UpdateTestRating(
            [FromBody] TestRatingUpdateRequest request,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {

            var requestErr = request.CheckForErr();
            if (requestErr.NotNone()) {
                return ResultsHelper.BadRequest.WithErr(requestErr);
            }
            try {
                using (var db = await dbFactory.CreateDbContextAsync()) {
                    TestId testId = request.GetParsedTestId().Value;
                    BaseTest? test = await db.TestsSharedInfo
                        .Include(t => t.Ratings)
                        .FirstOrDefaultAsync(t => t.Id == testId);
                    if (test is null) {
                        return ResultsHelper.BadRequest.UnknownTest();
                    }
                    if (!test.Settings.EnableTestRatings) {
                        return ResultsHelper.BadRequest.WithErr("Ratings for this test are disabled");
                    }
                    bool haveAccess;
                    if (httpContext.TryGetUserId(out AppUserId viewerId)) {
                        haveAccess = await TestAccessValidator.CheckUserAccessToTest(
                            db,
                            test.CreatorId,
                            test.Settings.Privacy,
                            viewerId
                        );
                    } else {
                        haveAccess = test.Settings.Privacy == PrivacyValues.Anyone;
                    }
                    if (!haveAccess) {
                        return ResultsHelper.BadRequest.NoTestAccess();
                    }
                    AppUser? viewer = await db.AppUsers
                        .Include(u => u.TestRatings)
                        .FirstOrDefaultAsync(u => u.Id == viewerId);
                    if (viewer is null) {
                        return ResultsHelper.BadRequest.WithErr("An error has occurred. Please log out, login and try again");
                    }
                    TestRating? existingRating = viewer.TestRatings.FirstOrDefault(r => r.TestId == testId);
                    if (existingRating is null) {
                        db.TestRatings.Add(TestRating.CreateNew(testId, viewerId, request.Rating));
                    } else {
                        existingRating.UpdateRatingValue(request.Rating);
                        db.Update(existingRating);
                    }
                    await db.SaveChangesAsync();
                    return Results.Ok();
                }
            } catch {
                return ResultsHelper.BadRequest.ServerError();
            }
        }
        internal static async Task<IResult> GetFilteredRatings(
            [FromBody] GetFilteredRatingsRequest request,
            HttpContext httpContext,
            IDbContextFactory<AppDbContext> dbFactory
        ) {
            Err requestErr = request.CheckForErr();
            if (requestErr.NotNone()) {
                return ResultsHelper.BadRequest.WithErr(requestErr);
            }
            ParsedRatingsFilter? parsedRequest = request.ToParsedObject();
            if (parsedRequest is null) {
                return ResultsHelper.BadRequest.ServerError();
            }
            using (var db = await dbFactory.CreateDbContextAsync()) {
                try {

                    BaseTest? test = await db.TestsSharedInfo
                        .Include(t => t.Ratings)
                            .ThenInclude(dc => dc.User)
                        .FirstOrDefaultAsync(t => t.Id == parsedRequest.TestId);
                    if (test is null) {
                        return ResultsHelper.BadRequest.UnknownTest();
                    }
                    bool haveAccess;

                    HashSet<AppUserId> viewersFriends = [];
                    HashSet<AppUserId> viewersFollowers = [];

                    if (httpContext.TryGetUserId(out AppUserId viewerId)) {
                        haveAccess = await TestAccessValidator.CheckUserAccessToTest(db, test.CreatorId, test.Settings.Privacy, viewerId);
                        AppUser? viewer = await db.AppUsers
                            .Include(u => u.Friends)
                            .Include(u => u.Followers)
                            .FirstOrDefaultAsync(u => u.Id == viewerId);
                        if (viewer is not null) {
                            viewersFriends = viewer.Friends.Select(u => u.Id).ToHashSet();
                            viewersFollowers = viewer.Followers.Select(u => u.Id).ToHashSet();
                        }
                    } else {
                        haveAccess = test.Settings.Privacy == PrivacyValues.Anyone;
                    }
                    if (!haveAccess) {
                        return ResultsHelper.BadRequest.NoTestAccess();
                    }

                    HashSet<TestRating> filteredRatings = test.Ratings
                        .Where(r => parsedRequest.IsRatingValueFilterPassed(r.Rating))
                        .Where(r => parsedRequest.IsDateFilterPassed(DateOnly.FromDateTime(r.LastUpdate)))
                        .Where(c => parsedRequest.IsFollowersAndFriendsFilterPassed(
                            isFriend: viewersFriends.Contains(c.UserId),
                            isFollower: viewersFollowers.Contains(c.UserId)
                            ))
                        .ToHashSet();
                    var response = filteredRatings
                        .Select(TestRatingVm.FromTestRating)
                        .ToArray();
                    return Results.Ok(response);
                } catch {
                    return ResultsHelper.BadRequest.ServerError();
                }
            }
        }
    }
}
