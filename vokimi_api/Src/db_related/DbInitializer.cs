namespace vokimi_api.Src.db_related
{

    public class DbInitializer
    {
        public static async Task InitializeDb(AppDbContext dbContext) {

            await RecreateDb(dbContext);
            await dbContext.Database.EnsureCreatedAsync();
        }
        public static async Task RecreateDb(AppDbContext db) {
            await db.Database.EnsureDeletedAsync();
            await db.Database.EnsureCreatedAsync();

            DbDataFaker faker = new(db);
            await faker.AddAdmin();
            await faker.AddFakeUsers(10);
            await faker.AddFakeDraftGeneralTestToAdmin();
        }

    }
}
