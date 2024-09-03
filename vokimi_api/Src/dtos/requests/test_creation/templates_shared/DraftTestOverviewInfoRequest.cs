using System.Text.Json.Serialization;
namespace vokimi_api.Src.dtos.requests.test_creation.templates_shared
{
    public class DraftTestOverviewInfoRequest
    {
        public string TestId { get; init; }
        public string ViewerId { get; init; }
    }

}
