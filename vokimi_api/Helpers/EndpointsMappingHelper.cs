using Microsoft.EntityFrameworkCore;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.db_related;
using vokimi_api.Src.extension_classes;

namespace vokimi_api.Helpers
{
    public class EndpointsMappingHelper
    {
        public static RequestDelegate CreatorOnlyDefault(
       Func<HttpContext, string, Task<IResult>> handler,
       IDbContextFactory<AppDbContext> dbFactory) {
            return async context =>
            {
                // Extract testId from route values
                if (!context.Request.RouteValues.TryGetValue("testId", out var testIdObj) || testIdObj is not string testIdStr) {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("Test ID is required.");
                    return;
                }

                if (!Guid.TryParse(testIdStr, out Guid testGuid)) {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("Invalid Test ID format.");
                    return;
                }

                var testId = new DraftTestId(testGuid);

                // Fetch the test from the database
                using (var dbContext = dbFactory.CreateDbContext()) {
                    var test = dbContext.DraftTestsSharedInfo.FirstOrDefault(t => t.Id == testId);
                    if (test == null) {
                        context.Response.StatusCode = StatusCodes.Status404NotFound;
                        await context.Response.WriteAsync("Test not found.");
                        return;
                    }

                    if (!context.IfAuthenticatedUserIdEqualsStr(test.CreatorId.ToString())) {
                        await ResultsHelper.BadRequestNotCreator().ExecuteAsync(context);
                        return;
                    }
                }

                var result = await handler(context, testIdStr);
                await result.ExecuteAsync(context);
            };
        }
    }
}
