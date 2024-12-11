using vokimi_api.Endpoints.pages;

namespace vokimi_api.EndpointsMappers.pages
{
    internal class CreatorsEndpointsMapper
    {
        internal static void MapAll(WebApplication app) {
            app.MapGet("/creators/listCreators", CreatorsEndpoints.GetCreatorsIds);
        }
    }
}
