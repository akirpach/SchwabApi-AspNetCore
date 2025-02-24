
using SchwabApi.WebApi.Entities;

namespace SchwabApi.WebApi.Services
{
    public interface ITransactionsService
    {
        Task<IEnumerable<Transactions>> GetAllTransactionsAsync();
    }
}