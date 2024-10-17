using Microsoft.EntityFrameworkCore;
using vokimi_api.Src.db_related.context_configuration.db_entities_relations_classes;
using vokimi_api.Src.db_related.db_entities.draft_tests.draft_general_test;
using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.db_related.context_configuration.model_builder_extensions
{
    internal static class DraftGeneralTestsConfigExtensions
    {
        internal static void ConfigureDraftGeneralTest(this ModelBuilder modelBuilder) {
            modelBuilder.Entity<DraftGeneralTest>(entity => {
                entity.HasMany(x => x.Questions)
                      .WithOne()
                      .HasForeignKey(x => x.TestId);

                entity.HasMany(x => x.PossibleResults)
                      .WithOne()
                      .HasForeignKey(x => x.TestId);
            });
        }
        internal static void ConfigureDraftGeneralTestQuestions(this ModelBuilder modelBuilder) {
            modelBuilder.Entity<DraftGeneralTestQuestion>(entity => {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasConversion(v => v.Value, v => new DraftGeneralTestQuestionId(v));
                entity.HasMany(x => x.Answers)
                      .WithOne()
                      .HasForeignKey(x => x.QuestionId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
        internal static void ConfigureDraftGeneralTestAnswers(this ModelBuilder modelBuilder) {
            modelBuilder.Entity<DraftGeneralTestAnswer>(entity => {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasConversion(v => v.Value, v => new DraftGeneralTestAnswerId(v));
                entity.Property(e => e.QuestionId).IsRequired();
                entity.Property(e => e.OrderInQuestion).IsRequired();
                entity.HasOne(e => e.TypeSpecificInfo)
                      .WithOne()
                      .HasForeignKey<DraftGeneralTestAnswer>(e => e.TypeSpecificInfoId)
                      .OnDelete(DeleteBehavior.Cascade);


                entity.HasMany(e => e.RelatedResults)
                    .WithMany(e => e.AnswersLeadingToResult)
                    .UsingEntity<RelationsDraftGeneralTestAnswerWithResult>(
                        j => j.HasOne(x => x.Result)
                              .WithMany()
                              .HasForeignKey(x => x.ResultId),
                        j => j.HasOne(x => x.Answer)
                              .WithMany()
                              .HasForeignKey(x => x.AnswerId)
                    );
            });
        }
        internal static void ConfigureDraftGeneralTestResults(this ModelBuilder modelBuilder) {
            modelBuilder.Entity<DraftGeneralTestResult>(entity => {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasConversion(v => v.Value, v => new DraftGeneralTestResultId(v));

            });
        }
    }
}
