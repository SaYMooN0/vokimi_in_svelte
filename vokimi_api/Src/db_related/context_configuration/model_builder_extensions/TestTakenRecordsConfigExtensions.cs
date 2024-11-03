using Microsoft.EntityFrameworkCore;
using vokimi_api.Src.db_related.db_entities;
using vokimi_api.Src.db_related.db_entities.test_taken_records;
using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.db_related.context_configuration.model_builder_extensions
{
    internal static class TestTakenRecordsConfigExtensions
    {
        internal static void ConfigureBaseTestTakenRecords(this ModelBuilder modelBuilder) {
            modelBuilder.Entity<BaseTestTakenRecord>(entity => {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasConversion(v => v.Value, v => new(v));

                entity.HasOne(b => b.Test)
                    .WithMany()
                    .HasForeignKey(b => b.TestId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(b => b.User)
                    .WithMany(u => u.TestTakings)
                    .HasForeignKey(b => b.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
        internal static void ConfigureGeneralTestTakenRecords(this ModelBuilder modelBuilder) {
            modelBuilder.Entity<GeneralTestTakenRecord>(entity =>
            {
                entity.HasOne(g => g.ReceivedResult)
                    .WithMany(r => r.TestTakenRecordsWithThisResult)
                    .HasForeignKey(g => g.ReceivedResultId);    
            });
        }
    }
}
