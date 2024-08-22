using Microsoft.EntityFrameworkCore;
using vokimi_api.Src.db_related.db_entities.draft_published_tests_shared;
using vokimi_api.Src.db_related.db_entities.draft_published_tests_shared.general_test_answers;
using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.db_related.context_configuration.model_builder_extensions
{
    internal static class DraftAndPublishedTestsSharedConfigExtensions
    {
        internal static void ConfigureTestConclusion(this ModelBuilder modelBuilder) {
            modelBuilder.Entity<TestConclusion>(entity => {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasConversion(v => v.Value, v => new TestConclusionId(v));
            });
        }
        internal static void ConfigureTestStylesSheet(this ModelBuilder modelBuilder) {
            modelBuilder.Entity<TestStylesSheet>(entity => {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasConversion(v => v.Value, v => new TestStylesSheetId(v));
            });
        }
        internal static void ConfigureGenerlTestAnswerTypeSpecificInfo(this ModelBuilder modelBuilder) {
            modelBuilder.Entity<GeneralTestAnswerTypeSpecificInfo>(entity => {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasConversion(v => v.Value, v => new(v));
            });
            modelBuilder.Entity<ImageOnlyAnswerAdditionalInfo>()
                        .ToTable("AnswerAdditionalForInfoImageOnly");
            modelBuilder.Entity<TextAndImageAnswerAdditionalInfo>()
                        .ToTable("AnswerAdditionalForTextAndImage");
            modelBuilder.Entity<TextOnlyAnswerAdditionalInfo>()
                        .ToTable("AnswerAdditionalForTextOnly");
        }

    }
}
