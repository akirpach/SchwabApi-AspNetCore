using Microsoft.AspNetCore.Mvc;
using SchwabApi.WebpApi.Services;

namespace SchwabApi.WebpApi.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    public class TransactionsController : ControllerBase
    {
        private readonly SchwabApiTransactionsService _schwabTransactionsService;

        public TransactionsController(SchwabApiTransactionsService schwabTransactionService)
        {
            _schwabTransactionsService = schwabTransactionService;
        }

        [HttpGet("{accountNumber}")]
        public async Task<IActionResult> GetTransactions(
            string accountNumber, [FromQuery] string startDate, [FromQuery] string endDate)
        {
            try
            {
                var transactions = await _schwabTransactionsService.GetTransactionsAsync(accountNumber, startDate, endDate);
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}