using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using server.Persistence;
using server.Persistence.Interfaces;
using server.Persistence.Repositories;

namespace server.src.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(opt => 
        {
            opt.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<ICommonRepository, CommonRepository>();
        
        return services;
    }
}