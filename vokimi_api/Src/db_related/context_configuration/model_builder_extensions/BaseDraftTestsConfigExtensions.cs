using Microsoft.EntityFrameworkCore;
using vokimi_api.Src.db_related.db_entities.draft_tests.draft_tests_shared;
using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.db_related.context_configuration.model_builder_extensions
{
    internal static class BaseDraftTestsConfigExtensions
    {
        internal static void ConfigureBaseDraftTest(this ModelBuilder modelBuilder) {
            modelBuilder.Entity<BaseDraftTest>(entity => {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasConversion(v => v.Value, v => new(v));
                entity.Property(x => x.MainInfoId).HasConversion(v => v.Value, v => new(v));
                entity.Property(x => x.CreatorId).HasConversion(v => v.Value, v => new(v));

                entity.HasOne(x => x.MainInfo)
                      .WithOne()
                      .HasForeignKey<BaseDraftTest>(x => x.MainInfoId);

                entity.HasOne(x => x.Conclusion)
                      .WithMany()
                      .HasForeignKey(x => x.ConclusionId);

                entity.HasOne(x => x.StylesSheet)
                      .WithMany()
                      .HasForeignKey(x => x.StylesSheetId);
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
