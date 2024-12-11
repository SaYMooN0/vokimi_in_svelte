using Microsoft.EntityFrameworkCore;
using vokimi_api.Src.db_related;

namespace vokimi_api.Endpoints.pages
{
    internal class CreatorsEndpoints
    {
        internal static async Task<IResult> GetCreatorsIds(IDbContextFactory<AppDbContext> dbFactory) {
            using (var db = await dbFactory.CreateDbContextAsync()) {

                string[] userIds = db.AppUsers.Select(x => x.Id.ToString()).ToArray();
                return Results.Ok(userIds);
            }
        }
    }
}
