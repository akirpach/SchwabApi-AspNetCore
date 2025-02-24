using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using SchwabApi.WebpApi.Services;

namespace SchwabApi.WebpApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly SchwabAuthService _authService;

        public AuthController(SchwabAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet("authorize")]
        public IActionResult Authorize()
        {
            string authUrl = _authService.GetAuthorizationUrl();
            // Open the URL in the default web browser
            OpenBrowser(authUrl);
            // return Ok(new { Url = authUrl });
            return Ok(
                new
                {
                    message = "OAuth page opened in browser.",
                    url = authUrl
                });
        }

        // Opens a browser window (Only works on local machines)
        private void OpenBrowser(string url)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                };
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to open browser: {ex.Message}");
            }
        }

        [HttpGet("callback")]
        public async Task<IActionResult> Callback([FromQuery] string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return BadRequest("Authorization code is missing.");
            }
            var tokenResponse = await _authService.ExchangeCodeForTokenAsync(code);
            return Ok("Success! Your account has been authorized and access token has been stored locally. You may now proceed using Schwab API. ");
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenResponse tokenResponse)
        {
            if (string.IsNullOrEmpty(tokenResponse.Refresh_Token))
            {
                return BadRequest("Refresh token is required.");
            }

            var newTokenResponse = await _authService.RefreshAccessTokenAsync(tokenResponse.Refresh_Token);
            return Ok(newTokenResponse);
        }
    }
}