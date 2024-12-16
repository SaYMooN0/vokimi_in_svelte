using Microsoft.EntityFrameworkCore;
using vokimi_api.Src.db_related.context_configuration.db_entities_relations_classes;
using vokimi_api.Src.db_related.db_entities.draft_published_tests_shared;
using vokimi_api.Src.db_related.db_entities.published_tests.general_test_related;
using vokimi_api.Src.db_related.db_entities.published_tests.published_tests_shared;
using vokimi_api.Src.db_related.db_entities.tests_related;
using vokimi_api.Src.db_related.db_entities.tests_related.discussions;
using vokimi_api.Src.db_related.db_entities.tests_related.discussions.attachments;
using vokimi_api.Src.db_related.db_entities.tests_related.tags;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.enums;

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
                entity.HasMany(x => x.SuggestedTags)
                    .WithOne()
                    .HasForeignKey(x => x.TestId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(t => t.Ratings)
                    .WithOne(t => t.Test)
                    .HasForeignKey(t => t.TestId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(t => t.DiscussionsComments)
                   .WithOne()
                   .HasForeignKey(dc => dc.TestId)
                   .OnDelete(DeleteBehavior.Cascade);
            });
        }
        internal static void ConfigureTestTags(this ModelBuilder modelBuilder) {
            modelBuilder.Entity<TestTag>(entity => {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasConversion(v => v.Value, v => new TestTagId(v));
                entity.Property(e => e.Value).IsRequired();
            });
        }
        internal static void ConfigureTestTagSuggestions(this ModelBuilder modelBuilder) {
            modelBuilder.Entity<TagSuggestionForTest>(entity => {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasConversion(v => v.Value, v => new TagSuggestionForTestId(v));
                entity.Property(e => e.Value).IsRequired();
            });
        }
        internal static void ConfigureTestRatings(this ModelBuilder modelBuilder) {
            modelBuilder.Entity<TestRating>(entity => {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasConversion(v => v.Value, v => new TestRatingId(v));
            });
        }
        internal static void ConfigureTestDiscussionComments(this ModelBuilder modelBuilder) {
            modelBuilder.Entity<TestDiscussionsComment>(entity => {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasConversion(v => v.Value, v => new TestDiscussionsCommentId(v));

                entity.HasOne(tc => tc.ParentComment)
                    .WithMany(tc => tc.ChildComments)
                    .HasForeignKey(tc => tc.ParentCommentId);
                entity.HasOne(tc => tc.Attachment)
                    .WithOne()
                    .HasForeignKey<TestDiscussionsComment>(tc => tc.AttachmentId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(tc => tc.CommentVotes)
                    .WithOne()
                    .HasForeignKey(cv => cv.CommentId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
        internal static void ConfigureDiscussionsCommentAttachmentConfiguration(this ModelBuilder modelBuilder) {
            modelBuilder.Entity<BaseDiscussionsCommentAttachment>(entity => {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasConversion(v => v.Value, v => new DiscussionsCommentAttachmentId(v));

                entity.HasDiscriminator<DiscussionsCommentAttachmentType>("AttachmentType")
                    .HasValue<GeneralTestResultCommentAttachment>(DiscussionsCommentAttachmentType.GeneralTestResult);
            });
            modelBuilder.Entity<GeneralTestResultCommentAttachment>(entity => {
                entity.HasOne(grca => grca.ReceivedResult)
                    .WithOne()
                    .HasForeignKey<GeneralTestResultCommentAttachment>(grca => grca.ReceivedResultId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
        internal static void ConfigureTestDiscussionCommentVotes(this ModelBuilder modelBuilder) {
            modelBuilder.Entity<DiscussionsCommentVote>(entity => {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasConversion(v => v.Value, v => new DiscussionsCommentVoteId(v));
            });
        }
    }

}
