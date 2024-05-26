using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using server.Domain.Models.Errors;


namespace server.Persistence.Configurations;

public class LogErrorConfiguration : IEntityTypeConfiguration<LogError>
{
    public void Configure(EntityTypeBuilder<LogError> builder)
    {
        builder.HasKey(c => c.Id);

    }
}