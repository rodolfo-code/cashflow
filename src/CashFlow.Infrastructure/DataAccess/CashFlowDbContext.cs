using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess;
public class CashFlowDbContext : DbContext
{
    public DbSet<Expense> Expenses {  get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var _connectionString = "Server=[::1],1433;Database=CashFlow;User ID=sa;Password=rodolfo@1;TrustServerCertificate=True;";
        optionsBuilder.UseSqlServer(_connectionString, options => options.EnableRetryOnFailure());
    }
}
