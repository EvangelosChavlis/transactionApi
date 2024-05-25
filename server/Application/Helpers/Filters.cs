using server.Domain.Models;

using System.Linq.Expressions;

namespace server.Application.Helpers;

public static class Filters
{
    public static string TransactionDateSorting = typeof(Transaction).GetProperty(nameof(Transaction.Inception))!.Name;
    
    
    public static Expression<Func<Transaction, bool>> TransactionsFilter(this string filter)
    {
        return c => c.ApplicationName.Contains(filter ?? "") ||
            c.Email.Contains(filter ?? "") || 
            c.Filename.Contains(filter ?? "") ||
            c.Url.Contains(filter ?? "") || 
            c.Amount.Contains(filter ?? "") ||
            c.Inception.ToString().Contains(filter ?? "") ||
            c.Allocation.ToString().Contains(filter ?? "");
    }
}