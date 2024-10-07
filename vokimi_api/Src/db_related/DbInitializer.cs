using vokimi_api.Src.db_related.db_entities.users;

namespace vokimi_api.Src.db_related
{

    public class DbInitializer
    {
        public static async Task InitializeDb(AppDbContext dbContext) {
            //await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();
            if (dbContext.AppUsers.Any() || dbContext.UnconfirmedAppUsers.Any()) {
                return;
            } else {
                await AddNewFakeUser(dbContext, "admin@admin.admin", "admin@admin.admin", "adminUser");
                await AddNewFakeUser(dbContext, "fake@fake.fake", "fake@fake.fake", "fakeUser");
            }
        }
        private async static Task AddNewFakeUser(
            AppDbContext dbContext,
            string email,
            string password,
            string username
        ) {
            using (var transaction = await dbContext.Database.BeginTransactionAsync()) {

                try {
                    var loginInfo = LoginInfo.CreateNew(email, BCrypt.Net.BCrypt.HashPassword(password));
                    var additionalInfo = UserAdditionalInfo.CreateNew(DateTime.UtcNow);
                    var pageSettings = UserPageSettings.CreateNew();
                    var user = AppUser.CreateNew(username, loginInfo.Id, additionalInfo.Id, pageSettings.Id);

                    dbContext.UserAdditionalInfo.Add(additionalInfo);
                    dbContext.LoginInfo.Add(loginInfo);
                    dbContext.AppUsers.Add(user);
                    dbContext.UserPageSettings.Add(pageSettings);

                    await dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                } catch {
                    await transaction.RollbackAsync();
                    throw;
                }
            }

        }
    }
}
