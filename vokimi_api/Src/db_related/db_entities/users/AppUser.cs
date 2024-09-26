using vokimi_api.Src.constants_store_classes;
using vokimi_api.Src.db_related.db_entities.draft_tests.draft_tests_shared;
using vokimi_api.Src.db_related.db_entities_ids;
using VokimiShared.src.models.db_classes.test.test_types;

namespace vokimi_api.Src.db_related.db_entities.users
{
    public class AppUser
    {
        public AppUserId Id { get; init; }
        public string Username { get; private set; }
        public string RealName { get; private set; } = string.Empty;
        public string ProfilePicturePath { get; private set; }
        public bool IsAccountPrivate { get; private set; } = false;

        public UserAdditionalInfoId UserAdditionalInfoId { get; init; }
        public virtual UserAdditionalInfo UserAdditionalInfo { get; private set; }

        public LoginInfoId LoginInfoId { get; init; }
        public virtual LoginInfo LoginInfo { get; private set; }

        public virtual ICollection<BaseDraftTest> DraftTests { get; private set; } = [];
        public virtual ICollection<BaseTest> PublishedTests { get; private set; } = [];
        public virtual ICollection<AppUser> Friends { get; private set; } = [];
        public virtual ICollection<AppUser> Followers { get; private set; } = [];
        public static AppUser CreateNew(string username, LoginInfoId loginInfoId, UserAdditionalInfoId userAdditionalInfoId) =>
            new() {
                Id = new AppUserId(),
                Username = username,
                RealName = string.Empty,
                ProfilePicturePath = ImgOperationsConsts.DefaultProfilePicture,
                IsAccountPrivate = false,
                LoginInfoId = loginInfoId,
                UserAdditionalInfoId = userAdditionalInfoId
            };

    }
}
