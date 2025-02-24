using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using SchwabApi.WebpApi.Services;

namespace SchwabApi.WebApi.Entities
{
    [Table("schwab_transactions")]      // PostgreSQL Table Name
    public class Transactions
    {
        [Key]
        public int Id { get; set; }
        [JsonPropertyName("activityId")]
        public long? ActivityId { get; set; }
        [JsonPropertyName("time")]
        [JsonConverter(typeof(CustomDateTimeOffsetConverter))]
        public DateTimeOffset Time { get; set; }

        [JsonPropertyName("accountNumber")]
        [Required]
        public string AccountNumber { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; } // ✅ Trade type (TRADE, DEPOSIT)

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
        [Column(TypeName = "decimal(18,2)")] // ✅ PostgreSQL financial precision
        public decimal NetAmount { get; set; }

        [JsonPropertyName("activityType")]
        public string ActivityType { get; set; }

        // ✅ One-to-Many Relationship (Ignored for now)
        public List<TransactionsTransferItem> TransferItems { get; set; }
    }
}