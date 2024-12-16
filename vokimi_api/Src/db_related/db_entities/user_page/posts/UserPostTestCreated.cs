using vokimi_api.Src.db_related.db_entities.published_tests.published_tests_shared;
using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.db_related.db_entities.user_page.posts
{
    public class UserPostTestCreated : BaseUserPost
    {
        public TestId RelatedTestId { get; init; }
        public virtual BaseTest RelatedTest { get; private set; }
        public static UserPostTestCreated CreateNew(AppUserId userId, string additionalText, TestId testId) => new() {
            Id = new(),
            UserId = userId,
            AdditionalText = additionalText,
            CreatedAt = DateTime.Now,
            RelatedTestId = testId
        };

    }
}
