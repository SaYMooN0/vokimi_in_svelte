using Microsoft.EntityFrameworkCore;
using vokimi_api.Src.db_related.db_entities.draft_tests.draft_general_test;
using vokimi_api.Src.db_related.db_entities.draft_tests.draft_tests_shared;
using vokimi_api.Src.db_related.db_entities.published_tests.general_test_related;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.enums;

namespace vokimi_api.Src.db_related.context_configuration.model_builder_extensions
{
    internal static class BaseDraftTestsConfigExtensions
    {
        internal static void ConfigureBaseDraftTest(this ModelBuilder modelBuilder) {
            modelBuilder.Entity<BaseDraftTest>(entity => {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasConversion(v => v.Value, v => new(v));

                entity.HasDiscriminator<TestTemplate>("Template")
                    .HasValue<DraftGeneralTest>(TestTemplate.General)
                //.HasValue<DraftScoringTest>(TestTemplate.Scoring)
                //.HasValue<DraftCorrectAnswersTest>(TestTemplate.CorrectAnswers);
                    .IsComplete();


                entity.OwnsOne(t => t.Settings);

                entity.Property(x => x.MainInfoId).HasConversion(v => v.Value, v => new(v));
                entity.Property(x => x.CreatorId).HasConversion(v => v.Value, v => new(v));

                entity.HasOne(x => x.MainInfo)
                      .WithOne()
                      .HasForeignKey<BaseDraftTest>(x => x.MainInfoId);

                entity.HasOne(x => x.Conclusion)
                      .WithOne()
                      .HasForeignKey<BaseDraftTest>(x => x.ConclusionId);

                entity.HasOne(x => x.StylesSheet)
                      .WithOne()
                      .HasForeignKey<BaseDraftTest>(x => x.StylesSheetId);
            });
        }

        internal static void ConfigureDraftTestMainInfo(this ModelBuilder modelBuilder) {
            modelBuilder.Entity<DraftTestMainInfo>(entity => {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasConversion(v => v.Value, v => new DraftTestMainInfoId(v));
            });
        }
    }
}
