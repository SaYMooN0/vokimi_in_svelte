namespace vokimi_api.Src.db_related
{

    public class DbInitializer
    {
        public static async Task InitializeDbAsync(AppDbContext dbContext) {
            //await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();

        }
    }
}
