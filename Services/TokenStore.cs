namespace SchwabApi.WebpApi.Services
{
    public class TokenStore : ITokenStore
    {
        private TokenResponse? _token;
        public Task SaveTokenAsync(TokenResponse token)
        {
            _token = token;
            return Task.CompletedTask;
        }

        public Task<string?> GetAccessTokenAsync()
        {
            return Task.FromResult(_token?.Access_Token);
        }

    }
}