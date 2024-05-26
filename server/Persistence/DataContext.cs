using Microsoft.EntityFrameworkCore;

using server.Domain.Models;
using server.Domain.Models.Errors;
using server.Persistence.Configurations;

namespace server.Persistence;

public class DataContext : DbContext
{
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<LogError> LogErrors { get; set; }
    
    public DataContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configurations
        modelBuilder.ApplyConfiguration(new TransactionConfiguration());
        modelBuilder.ApplyConfiguration(new LogErrorConfiguration());
    }
}
