using Microsoft.EntityFrameworkCore;
using vokimi_api.Src.db_related.context_configuration.model_builder_extensions;
using vokimi_api.Src.db_related.db_entities.draft_published_tests_shared.general_test_answers;
using vokimi_api.Src.db_related.db_entities.draft_published_tests_shared;
using vokimi_api.Src.db_related.db_entities.draft_tests.draft_general_test;
using vokimi_api.Src.db_related.db_entities.draft_tests.draft_tests_shared;
using vokimi_api.Src.db_related.db_entities.published_tests.general_test_related;
using vokimi_api.Src.db_related.db_entities.users;
using VokimiShared.src.models.db_classes.test.test_types;
using vokimi_api.Src.db_related.db_entities.tests_related;
using vokimi_api.Src.db_related.db_entities.user_page.posts;
using vokimi_api.Src.db_related.db_entities;
using vokimi_api.Src.db_related.db_entities.test_taken_records;

namespace vokimi_api.Src.db_related
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //users
        public DbSet<UnconfirmedAppUser> UnconfirmedAppUsers { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<LoginInfo> LoginInfo { get; set; }
        public DbSet<UserAdditionalInfo> UserAdditionalInfo { get; set; }

        //users page
        public DbSet<UserPageSettings> UserPageSettings { get; set; }
        public DbSet<BaseUserPost> BaseUserPosts { get; set; }
        public DbSet<UserPostTestCreated> UserPostsTestCreated { get; set; }

        //draft tests shared
        public DbSet<BaseDraftTest> DraftTestsSharedInfo { get; set; }
        public DbSet<DraftTestMainInfo> DraftTestMainInfo { get; set; }
        //draft general test
        public DbSet<DraftGeneralTest> DraftGeneralTests { get; set; }
        public DbSet<DraftGeneralTestResult> DraftGeneralTestResults { get; set; }
        public DbSet<DraftGeneralTestQuestion> DraftGeneralTestQuestions { get; set; }
        public DbSet<DraftGeneralTestAnswer> DraftGeneralTestAnswers { get; set; }
        //published and draft tests shared
        public DbSet<TestConclusion> TestConclusions { get; set; }
        public DbSet<TestStylesSheet> TestStyles { get; set; }
        public DbSet<GeneralTestAnswerTypeSpecificInfo> AnswerTypeSpecificInfo { get; set; }
        //published tests only
        public DbSet<BaseTest> TestsSharedInfo { get; set; }
        public DbSet<TestGeneralTemplate> TestsGeneralTemplate { get; set; }
        public DbSet<GeneralTestQuestion> GeneralTestQuestions { get; set; }
        public DbSet<GeneralTestAnswer> GeneralTestAnswers { get; set; }
        public DbSet<GeneralTestResult> GeneralTestResults { get; set; }
        //tests related
        public DbSet<TestTag> TestTags { get; set; }
        public DbSet<TestRating> TestRatings { get; set; }
        //test taken records
        public DbSet<BaseTestTakenRecord> BaseTestTakenRecords { get; set; }
        public DbSet<GeneralTestTakenRecord> GeneralTestTakenRecords { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            //users
            modelBuilder.ConfigureAppUser();
            modelBuilder.ConfigureLoginInfo();
            modelBuilder.ConfigureUserAdditionalInfo();
            modelBuilder.ConfigureUnconfirmedAppUser();

            //users page
            modelBuilder.ConfigureAllUserPagePosts();
            modelBuilder.ConfigureUserPageSettings();


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

            //published tests
            modelBuilder.ConfigureBaseTest();
            modelBuilder.ConfigureTestTags();
            modelBuilder.ConfigureTestRatings();

            //published general tests
            modelBuilder.ConfigureTestGeneralTemplate();
            modelBuilder.ConfigureGeneralTestQuestions();
            modelBuilder.ConfigureGeneralTestAnswers();
            modelBuilder.ConfigureGeneralTestResults();

            //test taking records
            modelBuilder.ConfigureBaseTestTakenRecords();
            modelBuilder.ConfigureGeneralTestTakenRecords();
        }

    }
}
