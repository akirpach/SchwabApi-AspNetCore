using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchwabApi.WebpApi.Services;

namespace SchwabApi.WebpApi.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountController : ControllerBase
    {
        private readonly SchwabApiAccountService _schwabApiService;

        public AccountController(SchwabApiAccountService schwabApiAccountService)
        {
            _schwabApiService = schwabApiAccountService;
        }

        [HttpGet("accountNumbers")]
        public async Task<IActionResult> GetAccountNumbers()
        {
            try
            {
                var accounts = await _schwabApiService.SchwabAccountNumbers();
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}