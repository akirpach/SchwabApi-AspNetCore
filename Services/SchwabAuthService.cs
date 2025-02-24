using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;

namespace SchwabApi.WebpApi.Services
{
    public class SchwabAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly SchwabApiSettings _settings;
        private readonly ITokenStore _tokenStore;

        public SchwabAuthService(HttpClient httpClient, IOptions<SchwabApiSettings> settings, ITokenStore tokenStore)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
            _tokenStore = tokenStore;

            // Log or debug the settings to verify they are populated correctly
            // Console.WriteLine($"AuthorizeUrl: {_settings.AuthorizeUrl}");
            // Console.WriteLine($"ClientId: {_settings.ClientId}");
            // Console.WriteLine($"RedirectUri: {_settings.RedirectUri}");
        }

        public string GetAuthorizationUrl()
        {
            // Console.WriteLine($"{_settings.AuthorizeUrl}?client_id={_settings.ClientId}&redirect_uri={_settings.RedirectUri}");
            return $"{_settings.AuthorizeUrl}?client_id={_settings.ClientId}&redirect_uri={_settings.RedirectUri}";
        }

        public async Task<TokenResponse> ExchangeCodeForTokenAsync(string authorizationCode)
        {
            var requestBody = new Dictionary<string, string>
        {
            { "grant_type", "authorization_code" },
            { "code", authorizationCode },
            { "redirect_uri", _settings.RedirectUri }
        };

            using var request = new HttpRequestMessage(HttpMethod.Post, _settings.TokenUrl);

            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", GetEncodedClientCredentials());
            request.Content = new FormUrlEncodedContent(requestBody);

            var response = await _httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to get token. Response {response}");
            }
            response.EnsureSuccessStatusCode();

            var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true      // Allows case-insensitive matching
            });

            if (tokenResponse?.Access_Token == null)
            {
                throw new Exception($"Invalid token response: {content}");
            }

            await _tokenStore.SaveTokenAsync(tokenResponse);

            return tokenResponse;
        }

        public async Task<TokenResponse> RefreshAccessTokenAsync(string refreshToken)
        {
            var requestBody = new Dictionary<string, string>
        {
            { "grant_type", "refresh_token" },
            { "refresh_token", refreshToken }
        };

            using var request = new HttpRequestMessage(HttpMethod.Post, _settings.TokenUrl);
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", GetEncodedClientCredentials());
            request.Content = new FormUrlEncodedContent(requestBody);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(content);

            if (tokenResponse == null || string.IsNullOrEmpty(tokenResponse.Access_Token))
            {
                throw new Exception($"Failed to get valid token: {content}");
            }

            await _tokenStore.SaveTokenAsync(tokenResponse);

            return tokenResponse;

        }

        private string GetEncodedClientCredentials()
        {
            var credentials = $"{_settings.ClientId}:{_settings.ClientSecret}";
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));
        }

    }
}