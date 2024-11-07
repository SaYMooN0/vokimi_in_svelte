using Microsoft.EntityFrameworkCore;
using vokimi_api.Endpoints.pages.test_creation;
using vokimi_api.Src.db_related;
using vokimi_api.Src.enums;

namespace vokimi_api.EndpointsMappers.pages.test_creation
{
    internal class TestCreationEndpointsMapper
    {
        internal static void MapAll(WebApplication app) {
            app.MapPost("/testCreation/createNewTest/{template}", async (HttpContext httpContext, IDbContextFactory<AppDbContext> dbFactory, string template) => {
                TestTemplate? parsedTemplate = TestTemplateExtensions.FromId(template);
                if (parsedTemplate is null) {
                    return Results.BadRequest("Invalid template specified.");
                }
                return await TestCreationSharedEndpoints.CreateNewTest(httpContext, dbFactory, parsedTemplate.Value);
            });
            app.MapPost("/testCreation/setDraftTestCoverToDefault/{testId}", TestCreationSharedEndpoints.SetDraftTestCoverToDefault);

            app.MapGet("/testCreation/getDraftTestMainInfoData/{testId}", TestCreationSharedEndpoints.GetDraftTestMainInfoData);
            app.MapPost("/testCreation/updateDraftTestMainInfo", TestCreationSharedEndpoints.UpdateDraftTestMainInfo);

            app.MapGet("/testCreation/getDraftTestSettingsData/{testId}", TestCreationSharedEndpoints.GetDraftTestSettingsData);
            app.MapPost("/testCreation/updateDraftTestSettings", TestCreationSharedEndpoints.UpdateDraftTestSettings);

            app.MapGet("/testCreation/getDraftTestConclusionData/{testId}", TestCreationSharedEndpoints.GetDraftTestConclusionData);
            app.MapPost("/testCreation/createDraftTestConclusion/{testId}", TestCreationSharedEndpoints.CreateDraftTestConclusion);
            app.MapPost("testCreation/updateDraftTestConclusion/{testId}", TestCreationSharedEndpoints.UpdateDraftTestConclusion);
            app.MapDelete("/testCreation/deleteDraftTestConclusion/{testId}", TestCreationSharedEndpoints.DeleteDraftTestConclusion);
        }
    }
}
