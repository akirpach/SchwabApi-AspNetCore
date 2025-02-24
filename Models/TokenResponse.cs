using System.Text.Json.Serialization;

public class TokenResponse
{
    [JsonPropertyName("expires_in")]
    public int Expires_In { get; }
    [JsonPropertyName("token_type")]
    public string Token_Type { get; set; }

    [JsonPropertyName("scope")]
    public string Scope { get; }
    [JsonPropertyName("refresh_token")]
    public string Refresh_Token { get; set; }
    [JsonPropertyName("access_token")]
    public string Access_Token { get; set; }

    [JsonPropertyName("id_token")]
    public string IdToken { get; set; }

}