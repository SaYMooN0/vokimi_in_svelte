using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.db_related.db_entities.user_page.posts
{
    public abstract class BaseUserPost
    {
        public UserPagePostId Id { get; init; }
        public AppUserId UserId { get; init; }
        public string? AdditionalText { get; init; }
        public DateTime? CreatedAt { get; init; }


    }
}
