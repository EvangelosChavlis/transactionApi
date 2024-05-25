using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

using server.Domain.Models.Common;

namespace server.Persistence.Interfaces;

public interface ICommonRepository
{
    Task<Envelope<T>> GetPagedResultsAsync<T>(
        DbSet<T> query, 
        UrlQuery pageParams,
        Expression<Func<T, bool>>[] filterExpressions,
        Expression<Func<T, object>>[] includeExpressions,
        CancellationToken token = default
    ) where T : class ;

    Task<T?> GetResultByIdAsync<T>(
        DbSet<T> query,
        Expression<Func<T, bool>>[] filterExpressions,
        Expression<Func<T, object>>[] includeExpressions,
        CancellationToken token = default
    ) where T : class ;
}