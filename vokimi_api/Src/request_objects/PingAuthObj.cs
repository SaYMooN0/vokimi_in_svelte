using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src
{
    public record class PingAuthObj(string? Email, string? Username, AppUserId? UserId)
    {
        public static PingAuthObj Empty => new(null, null, null);
        public const string
            ClaimKeyUsername = "username",
            ClaimKeyEmail = "email",
            ClaimKeyUserId = "userid";
    }
}
