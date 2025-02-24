

using System.Net.Http.Headers;
using System.Text.Json;

namespace SchwabApi.WebpApi.Services
{
    public class SchwabApiTransactionsService
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenStore _tokenStore;

        public SchwabApiTransactionsService(HttpClient httpClient, ITokenStore tokenStore)
        {
            _httpClient = httpClient;
            _tokenStore = tokenStore;
        }

        public async Task<List<TransactionsRoot>> GetTransactionsAsync(
            string accountNumber, string startDate, string endDate, string type = "TRADE")
        {
            var accessToken = await _tokenStore.GetAccessTokenAsync();

            if (string.IsNullOrEmpty(accessToken))
            {
                throw new Exception("No access token available. Please authenticate first.");
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Format Dates, per Schwab API (yyyy-MM-dd'T'HH:mm:ss.SSSZ)
            string formattedStartDate = SchwabApiFormatDate.FormatSchwabApiDate(startDate, true);
            string formattedEndDate = SchwabApiFormatDate.FormatSchwabApiDate(endDate, false);

            // string formattedStartDate = startDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            // string formattedEndDate = endDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

            var schwabTransactionsUrl = $"https://api.schwabapi.com/trader/v1/accounts/{accountNumber}/transactions" +
                                        $"?startDate={formattedStartDate}&endDate={formattedEndDate}&types={type}";

            Console.WriteLine($"Requesting Schwab API: {schwabTransactionsUrl}");

            var response = await _httpClient.GetAsync(schwabTransactionsUrl);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Failed to fetch transactions. Response: {errorContent}");

            }

            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Schwab API Response: {content}");       // debugging response

            var transactions = JsonSerializer.Deserialize<List<TransactionsRoot>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return transactions ?? new List<TransactionsRoot>();
        }
    }
}