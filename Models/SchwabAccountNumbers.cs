using System.Text.Json.Serialization;

public class SchwabAccountNumbers
{
    [JsonPropertyName("accountNumber")]
    public string AccountNumber { get; set; }
    [JsonPropertyName("hashValue")]
    public string HashValue { get; set; }

}