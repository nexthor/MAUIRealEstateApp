using System.Text.Json.Serialization;

namespace RealEstate.Models
{
    public class LoginResponse
    {
        [JsonPropertyName("access_token")]
        public string? Token { get; set; }
        [JsonPropertyName("token_type")]
        public string? TokenType { get; set; }
        [JsonPropertyName("user_id")]
        public int UserId { get; set; }
        [JsonPropertyName("user_name")]
        public string? UserName { get; set; }
    }
}
