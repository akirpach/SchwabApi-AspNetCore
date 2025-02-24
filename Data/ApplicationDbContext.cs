
using Microsoft.EntityFrameworkCore;
using SchwabApi.WebApi.Entities;

namespace SchwabApi.WebApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
        public DbSet<Transactions> SchwabTransactions { get; set; }

    }
}