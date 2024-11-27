using Microsoft.EntityFrameworkCore;
using vokimi_api.Helpers;
using vokimi_api.Src.db_related;
using vokimi_api.Src.db_related.db_entities.users;
using vokimi_api.Src.dtos.responses.profile_editing_page;
using vokimi_api.Src.extension_classes;

namespace vokimi_api.Endpoints.pages
{
    internal static class ProfileEditingEndpoints
    {
        internal static async Task<IResult> GetProfileData(
            HttpContext httpContext,
            IDbContextFactory<AppDbContext> dbFactory
        ) {
            if (!httpContext.TryGetUserId(out var userId)) {
                return ResultsHelper.BadRequest.LogOutLogIn();
            }
            using (var db = await dbFactory.CreateDbContextAsync()) {
                AppUser? user = await db.AppUsers
                    .Include(u => u.UserAdditionalInfo)
                    .Include(u => u.LoginInfo)
                    .Include(u => u.UserPageSettings)
                    .FirstOrDefaultAsync(u => u.Id == userId);
                if (user is null) {
                    return ResultsHelper.BadRequest.LogOutLogIn();
                }
                return Results.Ok(ProfileEditPageData.FromUser(user));
            }
        }
    }
}
