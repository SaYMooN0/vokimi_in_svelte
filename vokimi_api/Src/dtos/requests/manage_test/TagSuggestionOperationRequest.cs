namespace vokimi_api.Src.dtos.requests.manage_test
{
    public record class TagSuggestionOperationRequest(
        string TestId,
        string TagSuggestionId
    )
    {
    }
}
