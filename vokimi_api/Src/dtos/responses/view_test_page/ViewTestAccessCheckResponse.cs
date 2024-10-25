using vokimi_api.Src.db_related.db_entities.users;

namespace vokimi_api.Src.dtos.responses.view_test_page
{
    public record class ViewTestAccessCheckResponse(
        string AccessStringValue,
        string TestCreatorId,
        string TestCreatorUsername,
        string TestCreatorProfilePath
    )
    {
        public static ViewTestAccessCheckResponse Denied() =>
            new("denied", string.Empty, string.Empty, string.Empty);
        public static ViewTestAccessCheckResponse TestNotFound() =>
            new("test_not_found", string.Empty, string.Empty, string.Empty);
        public static ViewTestAccessCheckResponse Success() =>
            new("success", string.Empty, string.Empty, string.Empty);
        public static ViewTestAccessCheckResponse FriendshipNeeded(AppUser creator)=> new(
            "friendship_needed", 
            creator.Id.ToString(), 
            creator.Username, 
            creator.ProfilePicturePath
        );
        public static ViewTestAccessCheckResponse FollowingNeeded(AppUser creator) => new(
           "following_needed",
           creator.Id.ToString(),
           creator.Username,
           creator.ProfilePicturePath
       );


    }
}
