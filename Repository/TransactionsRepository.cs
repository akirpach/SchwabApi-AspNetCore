using Microsoft.EntityFrameworkCore;
using SchwabApi.WebApi.Data;
using SchwabApi.WebApi.Entities;

namespace SchwabApi.WebApi.Repository
{
    public class TransactionsRepository : ITransactionsRepository
    {
        private readonly ApplicationDbContext _context;

        public TransactionsRepository(ApplicationDbContext context)
        {
            _context = context;     // Inject database context via constructor
        }

        public async Task<IEnumerable<Transactions>> GetAllAsync()
        {
            return await _context.SchwabTransactions.ToListAsync();
        }

        public async Task<Transactions> GetByIdAsync(int id)
        {
            return await _context.SchwabTransactions.FindAsync(id);

        }
    }
}