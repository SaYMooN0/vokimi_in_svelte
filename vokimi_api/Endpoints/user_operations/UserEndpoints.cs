using Microsoft.EntityFrameworkCore;
using vokimi_api.Helpers;
using vokimi_api.Src.db_related;
using vokimi_api.Src.db_related.db_entities.users;
using vokimi_api.Src.db_related.db_entities_ids;

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
    }
}
