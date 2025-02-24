using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchwabApi.WebpApi.Services
{
    public interface ITokenStore
    {
        Task SaveTokenAsync(TokenResponse token);
        Task<string?> GetAccessTokenAsync();
    }
}