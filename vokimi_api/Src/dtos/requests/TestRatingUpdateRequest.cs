using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.dtos.requests
{
    public record class TestRatingUpdateRequest(
        string TestId,
        ushort Rating
    )
    {
        public TestId? GetParsedTestId() =>
            Guid.TryParse(TestId, out var id)
            ? new TestId(id)
            : null;
        public Err CheckForErr() {
            if (Rating > 5 || Rating < 1) {
                return new Err($"Rating must be between 1 and 5");
            }
            if (GetParsedTestId() is null) {
                return new Err("Data transferring error. Please try again later");
            }
            return Err.None;
        }
    }
}
