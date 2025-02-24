using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchwabApi.WebApi.Entities;
using SchwabApi.WebApi.Repository;

namespace SchwabApi.WebApi.Services
{
    public class TransactionsService : ITransactionsService
    {
        private readonly ITransactionsRepository _transactionsRepo;     // Repository instance for database operations

        public TransactionsService(ITransactionsRepository transactionsRepo)
        {
            _transactionsRepo = transactionsRepo;       // Injecting the repository via constructor
        }

        public async Task<IEnumerable<Transactions>> GetAllTransactionsAsync()
        {
            var trans = await _transactionsRepo.GetAllAsync();      // Fetch all transactions from repository

            // Convert each transaction entity into a TransactionResponse

            return trans.Select(t => new Transactions
            {
                Id = t.Id,
                ActivityId = t.ActivityId,
                Time = t.Time,
                AccountNumber = t.AccountNumber,
                Description = t.Description,
                Type = t.Type,
                Status = t.Status,
                SubAccount = t.SubAccount,
                TradeDate = t.TradeDate,
                SettlementDate = t.SettlementDate,
                PositionId = t.PositionId,
                OrderId = t.OrderId,
                NetAmount = t.NetAmount,
                ActivityType = t.ActivityType
            })
            .ToList();
        }
    }
}