using Microsoft.EntityFrameworkCore;
using vokimi_api.Endpoints.tests_operations.test_creation;
using vokimi_api.Src.db_related;
using vokimi_api.Src.enums;

namespace vokimi_api.EndpointsMappers
{
    public class TestCreationEndpointsMapper
    {
        public static void MapAll(WebApplication app) {
            app.MapPost("/createNewTest/{template}", async (HttpContext httpContext, IDbContextFactory<AppDbContext> dbFactory, string template) =>
            {
                TestTemplate? parsedTemplate = TestTemplateExtensions.FromId(template);
                if (parsedTemplate is null) {
                    return Results.BadRequest("Invalid template specified.");
                }
                return await TestCreationSharedEndpoints.CreateNewTest(httpContext, dbFactory, parsedTemplate.Value);
            });

            app.MapGet("/testCreation/getDraftTestMainInfoData/{testId}", TestCreationSharedEndpoints.GetDraftTestMainInfoData);
            app.MapPost("/testCreation/updateDraftTestMainInfoData", TestCreationSharedEndpoints.UpdateDraftTestMainInfo);
            app.MapPost("/testCreation/setDraftTestCoverToDefault/{testId}", TestCreationSharedEndpoints.SetDraftTestCoverToDefault);
            app.MapGet("/testCreation/getDraftTestConclusionData/{testId}", TestCreationSharedEndpoints.GetDraftTestConclusionData);
            app.MapPost("/testCreation/createDraftTestConclusion/{testId}", TestCreationSharedEndpoints.CreateDraftTestConclusion);
            app.MapPost("testCreation/updateDraftTestConclusion", TestCreationSharedEndpoints.UpdateDraftTestConclusion);
            app.MapDelete("/testCreation/deleteDraftTestConclusion/{testId}", TestCreationSharedEndpoints.DeleteDraftTestConclusion);
        }
    }
}
