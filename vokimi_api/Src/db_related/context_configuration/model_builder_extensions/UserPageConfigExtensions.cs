using Microsoft.EntityFrameworkCore;
using vokimi_api.Src.db_related.db_entities.user_page.posts;
using vokimi_api.Src.db_related.db_entities.users;
using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.db_related.context_configuration.model_builder_extensions
{
    public static class UserPageConfigExtensions
    {
        internal static void ConfigureUserPageSettings(this ModelBuilder modelBuilder) {
            modelBuilder.Entity<UserPageSettings>(entity => {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasConversion(v => v.Value, v => new UserPageSettingsId(v));
                entity.OwnsOne(u => u.PrivacySettings);
            });
        }
        internal static void ConfigureAllUserPagePosts(this ModelBuilder modelBuilder) {
            modelBuilder.Entity<BaseUserPost>(entity => {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasConversion(v => v.Value, v => new UserPagePostId(v));
            });
            modelBuilder.Entity<UserPostTestCreated>(entity => {
                entity.Property(x => x.RelatedTestId).HasConversion(v => v.Value, v => new(v));
                entity.HasOne(x => x.RelatedTest)
                      .WithOne()
                      .HasForeignKey<UserPostTestCreated>(x => x.RelatedTestId);
            });

        }  
    }
}
