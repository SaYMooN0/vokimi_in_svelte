namespace vokimi_api.Src.dtos.responses.users_page
{
    public record class UserRelationsResponse(
        bool ViewerFollowsUser,
        bool UserFollowsViewer
    );

}
