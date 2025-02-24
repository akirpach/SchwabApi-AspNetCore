using SchwabApi.WebApi.Entities;

namespace SchwabApi.WebApi.Repository
{
    public interface ITransactionsRepository
    {
        Task<IEnumerable<Transactions>> GetAllAsync(); // ✅ Returns multiple records
        Task<Transactions?> GetByIdAsync(int id); // ✅ Returns a single record
    }
}