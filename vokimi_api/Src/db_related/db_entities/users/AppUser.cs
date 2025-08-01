﻿using vokimi_api.Src.constants_store_classes;
using vokimi_api.Src.db_related.db_entities.draft_tests.draft_tests_shared;
using vokimi_api.Src.db_related.db_entities.published_tests.published_tests_shared;
using vokimi_api.Src.db_related.db_entities.test_collections;
using vokimi_api.Src.db_related.db_entities.tests_related;
using vokimi_api.Src.db_related.db_entities.tests_related.discussions;
using vokimi_api.Src.db_related.db_entities.user_page.posts;
using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.db_related.db_entities.users
{
    public class AppUser
    {
        public AppUserId Id { get; init; }
        public string Username { get; private set; }
        public string ProfilePicturePath { get; private set; }

        public UserAdditionalInfoId UserAdditionalInfoId { get; init; }
        public virtual UserAdditionalInfo UserAdditionalInfo { get; private set; }

        public LoginInfoId LoginInfoId { get; init; }
        public virtual LoginInfo LoginInfo { get; private set; }

        public UserPageSettingsId UserPageSettingsId { get; init; }
        public virtual UserPageSettings UserPageSettings { get; private set; }

        public virtual ICollection<BaseDraftTest> DraftTests { get; private set; } = [];
        public virtual ICollection<BaseTest> PublishedTests { get; private set; } = [];
        public virtual ICollection<AppUser> Friends { get; private set; } = [];
        public virtual ICollection<AppUser> Followers { get; private set; } = [];
        public virtual ICollection<AppUser> Followings { get; private set; } = [];
        public virtual ICollection<BaseUserPost> PagePosts { get; private set; } = [];
        public virtual ICollection<BaseTestTakenRecord> TestTakings { get; private set; } = [];
        public virtual ICollection<TestRating> TestRatings { get; protected set; } = [];
        public virtual ICollection<TestDiscussionsComment> DiscussionsComments { get; protected set; } = [];
        public virtual ICollection<DiscussionsCommentVote> DiscussionsCommentVotes { get; protected set; } = [];
        public virtual ICollection<TestCollection> TestCollections { get; protected set; } = [];


        public static AppUser CreateNew(
            string username,
            LoginInfoId loginInfoId,
            UserAdditionalInfoId userAdditionalInfoId,
            UserPageSettingsId userPageSettingsId
        ) => new() {
            Id = new AppUserId(),
            Username = username,
            ProfilePicturePath = ImgOperationsConsts.DefaultProfilePicture,
            LoginInfoId = loginInfoId,
            UserAdditionalInfoId = userAdditionalInfoId,
            UserPageSettingsId = userPageSettingsId
        };
        public void UpdateUsername(string newUsername) =>
            this.Username = newUsername;
        public void UpdateProfilePicture(string newProfilePic) =>
            this.ProfilePicturePath = newProfilePic;

    }
}
