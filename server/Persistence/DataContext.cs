using Microsoft.EntityFrameworkCore;

using server.Domain.Models;
using server.Persistence.Configurations;

namespace server.src.Persistence;

public class DataContext : DbContext
{
    public DbSet<Transaction> Transactions { get; set; }
    
    public DataContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configurations
        modelBuilder.ApplyConfiguration(new TransactionConfiguration());
    }
}
