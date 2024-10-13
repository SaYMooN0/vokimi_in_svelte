using Microsoft.EntityFrameworkCore;
using vokimi_api.Helpers;
using vokimi_api.Src.db_related;
using vokimi_api.Src.db_related.db_entities.users;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.dtos.responses.users_page;
using vokimi_api.Src.extension_classes;

namespace vokimi_api.Endpoints
{
    public static class UserEndpoints
    {
        public static IResult DoesUserExist(string userId, IDbContextFactory<AppDbContext> dbFactory) {
            AppUserId appUserId;
            if (!Guid.TryParse(userId, out _)) {
                return ResultsHelper.BadRequestServerError();
            }
            appUserId = new(new(userId));

            using (var db = dbFactory.CreateDbContext()) {
                AppUser? user = db.AppUsers.FirstOrDefault(u => u.Id == appUserId);
                bool doesExist = user is not null;
                return Results.Ok(new { Exists = doesExist });
            }
        }
        public static IResult GetUserPageTopInfoData(string userId, IDbContextFactory<AppDbContext> dbFactory) {
            AppUserId appUserId;
            if (!Guid.TryParse(userId, out _)) {
                return ResultsHelper.BadRequestServerError();
            }
            appUserId = new(new(userId));

            using (var db = dbFactory.CreateDbContext()) {
                AppUser? user = db.AppUsers
                    .Include(u => u.UserPageSettings)
                    .FirstOrDefault(u => u.Id == appUserId);
                if (user is null) {
                    return ResultsHelper.BadRequestWithErr("Unknown user");
                }
                return Results.Ok(PageTopInfoDataResponse.FromUser(user));

            }
        }
        public static IResult GetUserPageAdditionalInfoData(
            string userId,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {
            if (!Guid.TryParse(userId, out _)) {
                return ResultsHelper.BadRequestServerError();
            }
            var appUserId = new AppUserId(new Guid(userId));

            using var db = dbFactory.CreateDbContext();
            var user = db.AppUsers
                .Include(u => u.UserAdditionalInfo)
                //.Include(u => u.Friends)
                //.Include(u => u.Followers)
                .FirstOrDefault(u => u.Id == appUserId);

            if (user is null) {
                return ResultsHelper.BadRequestWithErr("Unknown user");
            }

            if (!httpContext.TryGetUserId(out AppUserId viewerId)) {
                return Results.Ok(UserPageAdditionalInfoData.ForAnyOne(user.UserAdditionalInfo));
            }

            if (viewerId == user.Id) {
                return Results.Ok(UserPageAdditionalInfoData.ForMyself(user.UserAdditionalInfo));
            }

            if (user.Friends.Any(friend => friend.Id == viewerId)) {
                return Results.Ok(UserPageAdditionalInfoData.ForFriends(user.UserAdditionalInfo));
            }

            if (user.Followers.Any(follower => follower.Id == viewerId)) {
                return Results.Ok(UserPageAdditionalInfoData.ForFollowers(user.UserAdditionalInfo));
            }

            return Results.Ok(UserPageAdditionalInfoData.ForAnyOne(user.UserAdditionalInfo));
        }

    }
}
