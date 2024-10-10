using Microsoft.EntityFrameworkCore;
using vokimi_api.Helpers;
using vokimi_api.Src.db_related;
using vokimi_api.Src.db_related.db_entities.users;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.dtos.responses.users_page;

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
                    .Include(u=>u.UserPageSettings)
                    .FirstOrDefault(u => u.Id == appUserId);
                if (user is null) {
                    return ResultsHelper.BadRequestWithErr("Unknown user");
                }
                return Results.Ok(PageTopInfoDataResponse.FromUser(user));

            }
        }
    }
}
