using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SchwabApi.WebpApi.Services
{
    public class SchwabApiFormatDate
    {
        // Converts a user-friendly date (yyy-MM-dd) into Schwab API format.

        public static string FormatSchwabApiDate(string date, bool isStartDate)
        {
            if (!DateTime.TryParse(date, out var parsedDate))
            {
                throw new ArgumentException("Invalid date format. Expected: yyyy-MM-dd");
            }

            // Set time: 00:00:00.000Z for start, 23:59:59.999Z for end
            var formattedDate = isStartDate
                ? parsedDate.Date.AddHours(0).AddMinutes(0).AddSeconds(0).AddMilliseconds(0)            // Start of day
                : parsedDate.Date.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);      // End of day

            return formattedDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        }

    }


    public class CustomDateTimeOffsetConverter : JsonConverter<DateTimeOffset>
    {
        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string dateString = reader.GetString();

            if (string.IsNullOrEmpty(dateString))
            {
                return default;
            }

            dateString = dateString.Replace("+0000", "+00:00");

            if (DateTimeOffset.TryParseExact(
                dateString,
                "yyyy-MM-ddTHH:mm:sszzz",
                CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeUniversal,
                out var parsedDate))
            {
                return parsedDate;
            }

            throw new JsonException($"Invalid date format: {dateString}");
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-ddTHH:mm:sszzz"));
        }
    }
}