using System.Text.Json.Serialization;
namespace vokimi_api.Src.dtos.requests.draft_tests_request
{
    public class DraftTestOverviewInfoRequest
    {
        public string TestId { get; init; }
        public string ViewerId { get; init; }
    }

}
