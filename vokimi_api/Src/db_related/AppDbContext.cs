using Microsoft.EntityFrameworkCore;
using vokimi_api.Src.db_related.context_configuration.model_builder_extensions;
using vokimi_api.Src.db_related.db_entities.users;

namespace vokimi_api.Src.db_related
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UnconfirmedAppUser> UnconfirmedAppUsers { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<LoginInfo> LoginInfo { get; set; }
        public DbSet<UserAdditionalInfo> UserAdditionalInfo { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            //users
            modelBuilder.ConfigureAppUser();
            modelBuilder.ConfigureLoginInfo();
            modelBuilder.ConfigureUserAdditionalInfo();
            modelBuilder.ConfigureUnconfirmedAppUser();

            //draft and published tests shared
            modelBuilder.ConfigureTestConclusion();
            modelBuilder.ConfigureTestStylesSheet();
            modelBuilder.ConfigureGenerlTestAnswerTypeSpecificInfo();

            //base draft tests
            modelBuilder.ConfigureBaseDraftTest();
            modelBuilder.ConfigureDraftTestMainInfo();

            //draft general tests
            modelBuilder.ConfigureDraftGeneralTest();
            modelBuilder.ConfigureDraftGeneralTestQuestions();
            modelBuilder.ConfigureDraftGeneralTestAnswers();
            modelBuilder.ConfigureDraftGeneralTestResults();


        }

    }
}
