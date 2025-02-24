using System.Text.Json.Serialization;
using SchwabApi.WebpApi.Services;

public class TransactionsInstrument
{
    [JsonPropertyName("cusip")]
    public string Cusip { get; set; }
    [JsonPropertyName("symbol")]
    public string Symbol { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; }
    [JsonPropertyName("instrumentId")]
    public int InstrumentId { get; set; }
    [JsonPropertyName("netChange")]
    public int NetChange { get; set; }
    [JsonPropertyName("type")]
    public string Type { get; set; }
}

public class TransactionsRoot
{
    [JsonPropertyName("activityId")]
    public long? ActivityId { get; set; }
    [JsonPropertyName("time")]
    [JsonConverter(typeof(CustomDateTimeOffsetConverter))]
    public DateTimeOffset Time { get; set; }
    [JsonPropertyName("user")]
    public TransactionsUser User { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; }
    [JsonPropertyName("accountNumber")]
    public string AccountNumber { get; set; }
    [JsonPropertyName("type")]
    public string Type { get; set; }
    [JsonPropertyName("status")]
    public string Status { get; set; }
    [JsonPropertyName("subAccount")]
    public string SubAccount { get; set; }
    [JsonPropertyName("tradeDate")]
    [JsonConverter(typeof(CustomDateTimeOffsetConverter))]
    public DateTimeOffset TradeDate { get; set; }
    [JsonPropertyName("settlementDate")]
    [JsonConverter(typeof(CustomDateTimeOffsetConverter))]
    public DateTimeOffset SettlementDate { get; set; }
    [JsonPropertyName("positionId")]
    public long PositionId { get; set; }
    [JsonPropertyName("orderID")]
    public long OrderId { get; set; }
    [JsonPropertyName("netAmount")]
    public decimal NetAmount { get; set; }
    [JsonPropertyName("activityType")]
    public string ActivityType { get; set; }
    [JsonPropertyName("transferItems")]
    public List<TransactionsTransferItem> TransferItems { get; set; }
}

public class TransactionsTransferItem
{
    [JsonPropertyName("instrument")]
    public TransactionsInstrument Instrument { get; set; }
    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }
    [JsonPropertyName("cost")]
    public decimal Cost { get; set; }
    [JsonPropertyName("price")]
    public decimal Price { get; set; }
    [JsonPropertyName("feeType")]
    public string FeeType { get; set; }
    [JsonPropertyName("positionEffect")]
    public string PositionEffect { get; set; }
}

public class TransactionsUser
{
    [JsonPropertyName("cdDomainId")]
    public string CdDomainId { get; set; }
    [JsonPropertyName("login")]
    public string Login { get; set; }
    [JsonPropertyName("type")]
    public string Type { get; set; }
    [JsonPropertyName("userId")]
    public int UserId { get; set; }
    [JsonPropertyName("systemUserName")]
    public string SystemUserName { get; set; }
    [JsonPropertyName("firstName")]
    public string FirstName { get; set; }
    [JsonPropertyName("lastName")]
    public string LastName { get; set; }
    [JsonPropertyName("brokerRepCode")]
    public string BrokerRepCode { get; set; }
}