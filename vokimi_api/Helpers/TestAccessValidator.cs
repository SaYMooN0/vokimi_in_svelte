using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.db_related;
using vokimi_api.Src.enums;

namespace vokimi_api.Helpers
{
    public class TestAccessValidator
    {
        public static bool CheckUserAccessToTest(
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
                var creatorFriendIds = db.AppUsers
                    .Where(u => u.Id == testCreatorId)
                    .Select(u => u.Friends.Select(f => f.Id))
                    .FirstOrDefault();
                if (creatorFriendIds is null) {
                    return false;
                }

                return creatorFriendIds.Contains(viewerId);
            }

            if (testPrivacy == PrivacyValues.FriendsAndFollowers) {
                var creatorFriendAndFollowerIds = db.AppUsers
                    .Where(u => u.Id == testCreatorId)
                    .Select(u => u.Friends.Select(f => f.Id)
                    .Union(u.Followers.Select(f => f.Id)))
                    .FirstOrDefault();

                if (creatorFriendAndFollowerIds is null) {
                    return false;
                }

                return creatorFriendAndFollowerIds.Contains(viewerId);
            }
            return false;
        }
    }
}
