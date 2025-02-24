using System.Text.Json.Serialization;

public class SchwabErrorResponse
{
    [JsonPropertyName("message")]
    public string? Message { get; set; }
    [JsonPropertyName("erros")]
    public List<string>? Errors { get; set; }
}