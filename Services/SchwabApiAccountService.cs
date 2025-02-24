using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text.Json;


namespace SchwabApi.WebpApi.Services
{
    public class SchwabApiAccountService
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenStore _tokenStore;

        public SchwabApiAccountService(HttpClient httpClient, ITokenStore tokenStore)
        {
            _httpClient = httpClient;
            _tokenStore = tokenStore;
        }

        public async Task<List<SchwabAccountNumbers>> SchwabAccountNumbers()
        {
            var token = await _tokenStore.GetAccessTokenAsync();

            if (token == null || string.IsNullOrEmpty(token))
            {
                throw new Exception("No access token available. Please authorize first.");
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var schwabApiUrl = "https://api.schwabapi.com/trader/v1/accounts/accountNumbers";
            Console.WriteLine($"Requesting Schwab API: {schwabApiUrl}"); // ✅ Debugging URL

            var response = await _httpClient.GetAsync(schwabApiUrl);
            // var response = await _httpClient.GetAsync("https://api.schwabapi.com/v1/accounts/accountNumbers");

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                // Deserialize into SchwabErroResponse property
                try
                {
                    var errorResponse = JsonSerializer.Deserialize<SchwabErrorResponse>(errorContent, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (errorResponse != null && !string.IsNullOrEmpty(errorResponse.Message))
                    {
                        throw new Exception($"Failed to fetch account numebrs. Error: {errorResponse.Message}");
                    }
                }
                catch (JsonException)
                {
                    throw new Exception($"Failed to fetch account numbers. Response: {errorContent}");
                }
                throw new Exception($"Failed to fetch account numbers. Response: {errorContent}");
            }

            var content = await response.Content.ReadAsStringAsync();
            var accounts = JsonSerializer.Deserialize<List<SchwabAccountNumbers>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return accounts ?? new List<SchwabAccountNumbers>();
        }
    }
}