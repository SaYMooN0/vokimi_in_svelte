using System.Text.Json.Serialization;
using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.dtos.responses
{
    public record class PingAuthResponse(
        string? Email,
        string? Username,
        [property: JsonIgnore] AppUserId? UserId
    )
    {
        [JsonPropertyName("userId")]
        public string UserIdString => UserId?.ToString() ?? "";

        public static PingAuthResponse Empty => new(null, null, null);
        public const string
            ClaimKeyUsername = "username",
            ClaimKeyEmail = "email",
            ClaimKeyUserId = "userid";
        [JsonIgnore]
        public bool AnyFieldEmpty =>
            string.IsNullOrEmpty(Email) &&
            string.IsNullOrEmpty(Username) &&
            UserId is null;
    }
}
