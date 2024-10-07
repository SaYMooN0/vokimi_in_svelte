using Microsoft.EntityFrameworkCore;
using vokimi_api.Helpers;
using vokimi_api.Src.db_related.db_entities.users;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.db_related;

namespace vokimi_api.Endpoints.user_operations
{
    public static class UserEndpoints
    {
        public static IResult GetUserPageInfo(string userId, IDbContextFactory<AppDbContext> dbFactory) {
            AppUserId appUserId;
            if (!Guid.TryParse(userId, out _)) {
                return ResultsHelper.BadRequestServerError();
            }
            appUserId = new(new(userId));

            using (var db = dbFactory.CreateDbContext()) {
                AppUser? user = db.AppUsers.FirstOrDefault(u => u.Id == appUserId);
                if (user is null) {
                    return ResultsHelper.BadRequestUserDoesnotExist();
                }
                return null;
            }
        }
    }
}
