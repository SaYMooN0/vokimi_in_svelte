using Microsoft.EntityFrameworkCore;
using vokimi_api.Src.db_related.context_configuration.db_entities_relations_classes;
using vokimi_api.Src.db_related.db_entities.users;
using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.db_related.context_configuration.model_builder_extensions
{
    public static class UsersConfigExtensions
    {
        internal static void ConfigureAppUser(this ModelBuilder modelBuilder) {
            modelBuilder.Entity<AppUser>(entity => {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasConversion(v => v.Value, v => new AppUserId(v));
                entity.Property(x => x.LoginInfoId).HasConversion(v => v.Value, v => new LoginInfoId(v));
                entity.Property(x => x.UserAdditionalInfoId).HasConversion(v => v.Value, v => new UserAdditionalInfoId(v));
                entity.Property(x => x.UserPageSettingsId).HasConversion(v => v.Value, v => new UserPageSettingsId(v));

                entity.HasMany(x => x.DraftTests)
                      .WithOne()
                      .HasForeignKey(x => x.CreatorId);

                entity.HasMany(x => x.PublishedTests)
                    .WithOne(x => x.Creator)
                    .HasForeignKey(x => x.CreatorId);

                entity.HasMany(x => x.PagePosts)
                    .WithOne()
                    .HasForeignKey(x => x.UserId);

                entity.HasMany(x => x.Friends)
                      .WithMany()
                      .UsingEntity<RelationsAppUserWithFriend>(
                          j => j.HasOne(uf => uf.Friend).WithMany().HasForeignKey(uf => uf.FriendId),
                          j => j.HasOne(uf => uf.User).WithMany().HasForeignKey(uf => uf.UserId)
                      );

                entity.HasMany(x => x.Followers)
                      .WithMany()
                      .UsingEntity<RelationsAppUserWithFollower>(
                          j => j.HasOne(uf => uf.Follower).WithMany().HasForeignKey(uf => uf.FollowerId),
                          j => j.HasOne(uf => uf.User).WithMany().HasForeignKey(uf => uf.UserId)
                      );
                entity.HasMany(a => a.TestTakings)
                    .WithOne(t => t.User)
                    .HasForeignKey(t => t.UserId);
            });
        }

        internal static void ConfigureLoginInfo(this ModelBuilder modelBuilder) {
            modelBuilder.Entity<LoginInfo>(entity => {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasConversion(v => v.Value, v => new LoginInfoId(v));
            });
        }

        internal static void ConfigureUserAdditionalInfo(this ModelBuilder modelBuilder) {
            modelBuilder.Entity<UserAdditionalInfo>(entity => {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasConversion(v => v.Value, v => new UserAdditionalInfoId(v));
                entity.OwnsOne(u => u.Links);
                entity.OwnsOne(u => u.PrivacySettings);
            });
        }
        internal static void ConfigureUnconfirmedAppUser(this ModelBuilder modelBuilder) {
            modelBuilder.Entity<UnconfirmedAppUser>(entity => {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasConversion(v => v.Value, v => new UnconfirmedAppUserId(v));
            });
        }
    }
}
