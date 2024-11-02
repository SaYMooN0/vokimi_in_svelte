using Microsoft.EntityFrameworkCore;
using vokimi_api.Src.db_related.context_configuration.db_entities_relations_classes;
using vokimi_api.Src.db_related.db_entities.published_tests.general_test_related;
using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.db_related.context_configuration.model_builder_extensions
{
    internal static class GeneralTestConfigExtensions
    {
        internal static void ConfigureTestGeneralTemplate(this ModelBuilder modelBuilder) {
            modelBuilder.Entity<TestGeneralTemplate>(entity => {
                entity.HasMany(x => x.Questions)
                      .WithOne()
                      .HasForeignKey(x => x.TestId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(x => x.PossibleResults)
                    .WithOne()
                    .HasForeignKey(x => x.TestId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(t => t.TestTakings)
                    .WithOne()
                    .HasForeignKey(t => t.TestId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

        internal static void ConfigureGeneralTestQuestions(this ModelBuilder modelBuilder) {
            modelBuilder.Entity<GeneralTestQuestion>(entity => {
                entity.Property(x => x.Id).HasConversion(v => v.Value, v => new GeneralTestQuestionId(v));
                entity.HasMany(q => q.Answers)
                    .WithOne()
                    .HasForeignKey(a => a.QuestionId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

        }
        internal static void ConfigureGeneralTestAnswers(this ModelBuilder modelBuilder) {
            modelBuilder.Entity<GeneralTestAnswer>(entity => {
                entity.HasKey(e => e.Id);
                entity.Property(x => x.Id).HasConversion(v => v.Value, v => new GeneralTestAnswerId(v));

                entity.HasOne(a => a.TypeSpecificInfo)
                    .WithMany()
                    .HasForeignKey(a => a.TypeSpecificInfoId)
                    .OnDelete(DeleteBehavior.Cascade);

            });
        }
        internal static void ConfigureGeneralTestResults(this ModelBuilder modelBuilder) {
            modelBuilder.Entity<GeneralTestResult>(entity => {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasConversion(v => v.Value, v => new GeneralTestResultId(v));

                entity.HasMany(e => e.AnswersLeadingToResult)
                    .WithMany(e => e.RelatedResults)
                    .UsingEntity<RelationsGeneralTestAnswerWithResult>(
                         j => j.HasOne(pt => pt.Answer).WithMany().HasForeignKey(pt => pt.AnswerId),
                         j => j.HasOne(pt => pt.Result).WithMany().HasForeignKey(pt => pt.ResultId)
                    );
            });

        }
    }
}
