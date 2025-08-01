﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vokimi_api.Helpers;
using vokimi_api.Src.db_related;
using vokimi_api.Src.db_related.db_entities.published_tests.published_tests_shared;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.dtos.responses.manage_test_page.overall;
using vokimi_api.Src.extension_classes;

namespace vokimi_api.Endpoints.pages.manage_test
{
    internal static class ManageTestOverallEndpoints
    {
        internal static async Task<IResult> CheckUserAccessToPage(
            string testIdString,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {
            if (!Guid.TryParse(testIdString, out var testGuid)) {
                return ResultsHelper.BadRequest.UnknownTest();
            }
            TestId testId = new(testGuid);
            using (var db = await dbFactory.CreateDbContextAsync()) {
                BaseTest? t = await db.TestsSharedInfo.FindAsync(testId);
                if (t is null) {
                    return ResultsHelper.BadRequest.UnknownTest();
                }
                if (!httpContext.IsAuthenticatedUserIsTestCreator(t)) {
                    return ResultsHelper.BadRequest.WithErr("You don't have access to this page");
                }
                return Results.Ok(new { TestName = t.Name });

            }
        }
        internal static async Task<IResult> GetTabData(
            string testIdString,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {
            if (!Guid.TryParse(testIdString, out var testGuid)) {
                return ResultsHelper.BadRequest.UnknownTest();
            }
            TestId testId = new(testGuid);
            using (var db = await dbFactory.CreateDbContextAsync()) {
                BaseTest? test = await db.TestsSharedInfo
                    .Include(t => t.StylesSheet)
                    .FirstOrDefaultAsync(t => t.Id == testId);
                if (test is null) {
                    return ResultsHelper.BadRequest.UnknownTest();
                }
                if (!httpContext.IsAuthenticatedUserIsTestCreator(test)) {
                    return ResultsHelper.BadRequest.WithErr("You don't have access to this page");
                }
                return Results.Ok(ManageTestOverallTabDataResponse.FromTest(test));

            }
        }
        internal static async Task<IResult> ChangeTestDescription(
            string testIdString,
            [FromBody] string? newDescription,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {
            return ResultsHelper.BadRequest.WithErr("Not implemented");
        }
        internal static async Task<IResult> ChangeTestPrivacy(
            string testIdString,
            [FromBody] string newPrivacy,
            IDbContextFactory<AppDbContext> dbFactory,
            HttpContext httpContext
        ) {
            return ResultsHelper.BadRequest.WithErr("Not implemented");
        }
    }
}
