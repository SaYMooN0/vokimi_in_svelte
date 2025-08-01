﻿using Microsoft.EntityFrameworkCore;
using vokimi_api.Helpers;
using vokimi_api.Src.db_related.db_entities.users;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.db_related;
using vokimi_api.Src.dtos.responses.users_page;
using vokimi_api.Src.extension_classes;
using MailKit.Net.Imap;

namespace vokimi_api.Endpoints.pages
{
    public static class UserEndpoints
    {
        public static async Task<IResult> GetUserPageInfo(string userId, IDbContextFactory<AppDbContext> dbFactory) {
            AppUserId appUserId;
            if (!Guid.TryParse(userId, out _)) {
                return ResultsHelper.BadRequest.ServerError();
            }
            appUserId = new(new(userId));

            using (var db = await dbFactory.CreateDbContextAsync()) {
                AppUser? user = await db.AppUsers.FindAsync(appUserId);
                if (user is null) {
                    return ResultsHelper.BadRequest.UserDoesNotExist();
                }
                return null;
            }
        }
        public static async Task<IResult> DoesUserExist(string userId, IDbContextFactory<AppDbContext> dbFactory) {
            AppUserId appUserId;
            if (!Guid.TryParse(userId, out _)) {
                return ResultsHelper.BadRequest.ServerError();
            }
            appUserId = new(new(userId));

            using (var db = await dbFactory.CreateDbContextAsync()) {
                AppUser? user = await db.AppUsers.FindAsync(appUserId);
                bool doesExist = user is not null;
                return Results.Ok(new { Exists = doesExist });
            }
        }
        public static async Task<IResult> GetUserPageTopInfoData(
            string userId,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {
            AppUserId appUserId;
            if (!Guid.TryParse(userId, out var userGuid)) {
                return ResultsHelper.BadRequest.ServerError();
            }
            appUserId = new(userGuid);


            using (var db = await dbFactory.CreateDbContextAsync()) {
                AppUser? user = await db.AppUsers
                    .Include(u => u.UserPageSettings)
                    .Include(u => u.Followings)
                    .Include(u => u.Followers)
                    .Include(u => u.Friends)
                    .Include(u => u.PublishedTests)
                    .FirstOrDefaultAsync(u => u.Id == appUserId);
                if (user is null) {
                    return ResultsHelper.BadRequest.WithErr("Unknown user");
                }
                if (!httpContext.TryGetUserId(out var viewerId)) {
                    return Results.Ok(PageTopInfoDataResponse.ForBasicUser(user));
                }
                if (viewerId == appUserId) {
                    return Results.Ok(PageTopInfoDataResponse.ForMyself(user));
                }
                if (user.Friends.Any(u => u.Id == viewerId)) {
                    return Results.Ok(PageTopInfoDataResponse.ForFriend(user));
                }
                if (user.Followers.Any(u => u.Id == viewerId)) {
                    return Results.Ok(PageTopInfoDataResponse.ForFollower(user));
                }
                return Results.Ok(PageTopInfoDataResponse.ForBasicUser(user));

            }
        }
        public static async Task<IResult> GetUserPageAdditionalInfoData(
            string userId,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {
            if (!Guid.TryParse(userId, out _)) {
                return ResultsHelper.BadRequest.ServerError();
            }
            var appUserId = new AppUserId(new Guid(userId));

            using var db = await dbFactory.CreateDbContextAsync();
            var user = await db.AppUsers
                .Include(u => u.UserAdditionalInfo)
                .Include(u => u.Friends)
                .Include(u => u.Followers)
                .FirstOrDefaultAsync(u => u.Id == appUserId);

            if (user is null) {
                return ResultsHelper.BadRequest.WithErr("Unknown user");
            }

            bool isViewerAuthenticated = httpContext.TryGetUserId(out AppUserId viewerId);

            return (isViewerAuthenticated, viewerId, user) switch {
                (true, var vId, { Id: var id }) when id == vId =>
                    Results.Ok(UserPageAdditionalInfoData.ForMyself(user.UserAdditionalInfo)),
                (true, var vId, { Friends: var friends }) when friends.Any(friend => friend.Id == vId) =>
                    Results.Ok(UserPageAdditionalInfoData.ForFriends(user.UserAdditionalInfo)),
                (true, var vId, { Followers: var followers }) when followers.Any(follower => follower.Id == vId) =>
                    Results.Ok(UserPageAdditionalInfoData.ForFollowers(user.UserAdditionalInfo)),
                _ =>
                    Results.Ok(UserPageAdditionalInfoData.ForAnyOne(user.UserAdditionalInfo))
            };
        }
        public static async Task<IResult> GetViewerAndUserRelations(
            string userId,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {
            if (!Guid.TryParse(userId, out var userGuid)) {
                return ResultsHelper.BadRequest.WithErr("Unknown user page");
            }
            AppUserId appUserId = new(userGuid);
            if (!httpContext.TryGetUserId(out var viewerId)) {
                return Results.Ok(new UserRelationsResponse(false, false));
            }
            using (var db = await dbFactory.CreateDbContextAsync()) {
                AppUser? viewer = await db.AppUsers
                    .Include(u => u.Friends)
                    .Include(u => u.Followers)
                    .Include(u => u.Followings)
                    .FirstOrDefaultAsync(u => u.Id == viewerId);
                if (viewer is null) {
                    return ResultsHelper.BadRequest.LogOutLogIn();
                }
                if (viewer.Friends.Any(f => f.Id == appUserId)) {
                    return Results.Ok(new UserRelationsResponse(true, true));
                }
                bool userIsFollowed = viewer.Followings.Any(f => f.Id == appUserId);
                bool viewerIsFollowed = viewer.Followers.Any(f => f.Id == appUserId);
                return Results.Ok(new UserRelationsResponse(userIsFollowed, viewerIsFollowed));
            }
        }

        public static async Task<IResult> FollowUser(
            string userToFollowId,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {
            if (!Guid.TryParse(userToFollowId, out var userToFollowGuid)) {
                return ResultsHelper.BadRequest.ServerError();
            }
            var toFollowId = new AppUserId(userToFollowGuid);

            using var db = await dbFactory.CreateDbContextAsync();

            if (!httpContext.TryGetUserId(out var viewerId)) {
                return ResultsHelper.BadRequest.LogOutLogIn();
            }
            AppUser? viewer = await db.AppUsers.FindAsync(viewerId);
            if (viewer is null) {
                return ResultsHelper.BadRequest.LogOutLogIn();
            }

            AppUser? userToFollow = await db.AppUsers
                .Include(u => u.Friends)
                .Include(u => u.Followers)
                .FirstOrDefaultAsync(u => u.Id == toFollowId);
            if (userToFollow is null) {
                return ResultsHelper.BadRequest.WithErr("User not found");
            }

            using var transaction = await db.Database.BeginTransactionAsync();
            bool userFollowsViewer = false;

            try {
                if (
                    userToFollow.Friends.Any(u => u.Id == viewerId) ||
                    userToFollow.Followers.Any(u => u.Id == viewerId)
                ) {
                    return ResultsHelper.BadRequest.WithErr("You are already follow this user");
                }

                AppUser? userBackFollowing = userToFollow.Followings.FirstOrDefault(u => u.Id == viewerId);

                if (userBackFollowing is null) {
                    userToFollow.Followers.Add(viewer);
                    userFollowsViewer = false;
                } else {
                    userToFollow.Followings.Remove(viewer);
                    userToFollow.Friends.Add(viewer);
                    userFollowsViewer = true;
                }
                bool viewerFollowsUser = true;
                db.SaveChanges();
                transaction.Commit();
                return Results.Ok(new UserRelationsResponse(viewerFollowsUser, userFollowsViewer));
            } catch {
                await transaction.RollbackAsync();
                return ResultsHelper.BadRequest.ServerError();
            }
        }
        public static async Task<IResult> UnfollowUser(
           string userToUnfollowId,
           IDbContextFactory<AppDbContext> dbFactory,
           HttpContext httpContext
       ) {
            if (!Guid.TryParse(userToUnfollowId, out var userToUnfollowGuid)) {
                return ResultsHelper.BadRequest.ServerError();
            }
            var toUnfollowId = new AppUserId(userToUnfollowGuid);

            using var db = await dbFactory.CreateDbContextAsync();

            if (!httpContext.TryGetUserId(out var viewerId)) {
                return ResultsHelper.BadRequest.LogOutLogIn();
            }
            AppUser? viewer = await db.AppUsers.FindAsync(viewerId);
            if (viewer is null) {
                return ResultsHelper.BadRequest.LogOutLogIn();
            }

            AppUser? userToUnfollow = await db.AppUsers
                .Include(u => u.Friends)
                .Include(u => u.Followers)
                .FirstOrDefaultAsync(u => u.Id == toUnfollowId);
            if (userToUnfollow is null) {
                return ResultsHelper.BadRequest.WithErr("User not found");
            }

            using var transaction = await db.Database.BeginTransactionAsync();
            try {
                throw new NotImplementedException();
                db.SaveChanges();
                transaction.Commit();
            } catch {
                await transaction.RollbackAsync();
                return ResultsHelper.BadRequest.ServerError();
            }
        }
    }
}
