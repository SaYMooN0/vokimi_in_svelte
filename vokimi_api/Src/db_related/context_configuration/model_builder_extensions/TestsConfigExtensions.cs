using Microsoft.EntityFrameworkCore;
using vokimi_api.Src.db_related.context_configuration.db_entities_relations_classes;
using vokimi_api.Src.db_related.db_entities.published_tests.general_test_related;
using vokimi_api.Src.db_related.db_entities.tests_related;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.enums;
using VokimiShared.src.models.db_classes.test.test_types;

namespace vokimi_api.Src.db_related.context_configuration.model_builder_extensions
{
    internal static class TestsConfigExtensions
    {
        internal static void ConfigureBaseTest(this ModelBuilder modelBuilder) {
            modelBuilder.Entity<BaseTest>(entity => {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasConversion(v => v.Value, v => new TestId(v));

                entity.HasDiscriminator<TestTemplate>("Template")
                    .HasValue<TestGeneralTemplate>(TestTemplate.General);
                //.HasValue<TestScoringTemplate>(TestTemplate.Scoring)
                //.HasValue<TestCorrectAnswersTemplate>(TestTemplate.CorrectAnswers);

                entity.OwnsOne(t => t.Settings);


                entity.Property(x => x.ConclusionId).HasConversion(
                    v => v.HasValue ? v.Value.Value : (Guid?)null,
                    v => v.HasValue ? new TestConclusionId(v.Value) : null);


                entity.HasOne(x => x.Conclusion)
                      .WithMany()
                      .HasForeignKey(x => x.ConclusionId);

                entity.HasOne(x => x.StylesSheet)
                      .WithMany()
                      .HasForeignKey(x => x.StylesSheetId);

                entity.HasMany(x => x.Tags)
                      .WithMany(x => x.Tests)
                      .UsingEntity<RelationsTestWithTag>(
                          j => j.HasOne(t => t.Tag).WithMany().HasForeignKey(t => t.TagId),
                          j => j.HasOne(t => t.Test).WithMany().HasForeignKey(t => t.TestId)
                      );
            });
        }
        internal static void ConfigureTestTags(this ModelBuilder modelBuilder) {
            modelBuilder.Entity<TestTag>(entity => {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasConversion(v => v.Value, v => new TestTagId(v));
                entity.Property(e => e.Value).IsRequired();
            });
        }
    }

}
