using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.db_related;
using vokimi_api.Src.enums;
using Microsoft.EntityFrameworkCore;
using vokimi_api.Src.db_related.db_entities.users;

namespace vokimi_api.Helpers
{
    public class TestAccessValidator
    {
        public async static Task<bool> CheckUserAccessToTest(
            AppDbContext db,
            AppUserId testCreatorId,
            PrivacyValues testPrivacy,
            AppUserId viewerId
        ) {
            if (testPrivacy == PrivacyValues.Anyone) {
                return true;
            }

            if (testCreatorId == viewerId) {
                return true;
            }

            if (testPrivacy == PrivacyValues.ForMyself) {
                return false;
            }

            if (testPrivacy == PrivacyValues.FriendsOnly) {
                AppUser? creator = await db.AppUsers
                    .Include(u => u.Friends)
                    .FirstOrDefaultAsync(u => u.Id == testCreatorId);
                if (creator is null || creator.Friends.Count < 1) {
                    return false;
                }
                return creator.Friends.Any(u => u.Id == viewerId);

            }

            if (testPrivacy == PrivacyValues.FriendsAndFollowers) {
                AppUser? creator = await db.AppUsers
                    .Include(u => u.Friends)
                    .Include(u => u.Followers)
                    .FirstOrDefaultAsync(u => u.Id == testCreatorId);
                if (creator is null
                    || creator.Friends.Count + creator.Followers.Count < 1) {

                    return false;
                }
                return creator.Friends.Any(u => u.Id == viewerId)
                    || creator.Followers.Any(u => u.Id == viewerId);
            }
            return false;
        }
    }
}
