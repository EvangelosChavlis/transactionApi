using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using server.Domain.Models;


namespace server.Persistence.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.ApplicationName)
            .HasMaxLength(200)
            .IsRequired(true);

        builder.Property(c => c.Email)
            .HasMaxLength(200)
            .IsRequired(true);

        builder.Property(c => c.Filename)
            .HasMaxLength(300)
            .IsRequired(false);

        builder.Property(c => c.Url)
            .IsRequired(false);

        builder.Property(c => c.Inception)
            .IsRequired(true);

        builder.Property(c => c.Amount)
            .IsRequired(true);
    }
}