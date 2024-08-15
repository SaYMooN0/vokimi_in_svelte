using vokimi_api.Src.db_related.db_entities.users;
using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.db_related.context_configuration.db_entities_relations_classes
{
    public class RelationsAppUserWithFollower
    {
        public AppUserId UserId { get; init; }
        public AppUserId FollowerId { get; init; }

        virtual public AppUser User { get; init; }
        virtual public AppUser Follower { get; init; }
    }
}
