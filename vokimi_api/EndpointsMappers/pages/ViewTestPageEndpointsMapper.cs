using vokimi_api.Endpoints.pages;
using vokimi_api.Endpoints.pages.view_test;

namespace vokimi_api.EndpointsMappers.pages
{
    internal static class ViewTestPageEndpointsMapper
    {
        internal static void MapAll(WebApplication app) {
            //base
            app.MapGet("/viewTest/checkTestViewPermission/{testId}", ViewTestPageEndpoints.CheckTestViewPermission);
            app.MapGet("/viewTest/getBasicTestInfo/{testId}", ViewTestPageEndpoints.GetBasicTestInfo);
            app.MapGet("/viewTest/getTestRatingsInfo/{testId}", ViewTestPageEndpoints.GetTestRatingsInfo);
            app.MapGet("/viewTest/getTestDiscussionsInfo/{testId}", ViewTestPageEndpoints.GetTestDiscussionsInfo);

            //ratings
            app.MapPost("/viewTest/ratings/updateTestRating", ViewTestRatingsEndpoints.UpdateTestRating);
            app.MapPost("/viewTest/ratings/getFilteredRatings", ViewTestRatingsEndpoints.GetFilteredRatings);

            //discussions
            app.MapPost("/viewTest/discussions/startNewDiscussion", ViewTestDiscussionsEndpoints.StartNewDiscussion);
            app.MapPost("/viewTest/discussions/saveAnswerToComment", ViewTestDiscussionsEndpoints.SaveAnswerToComment);
            app.MapPost("/viewTest/discussions/getFilteredDiscussions", ViewTestDiscussionsEndpoints.GetFilteredDiscussions);
            app.MapPost("/viewTest/discussions/handleCommentVoteChanged", ViewTestDiscussionsEndpoints.HandleCommentVoteChanged);

        }
    }
}
