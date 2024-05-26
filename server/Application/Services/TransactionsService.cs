using System.Linq.Expressions;
using System.Net;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using server.Application.Exceptions;
using server.Application.Helpers;
using server.Application.Interfaces;
using server.Application.Mappings;
using server.Domain.Dto;
using server.Domain.Dto.Common;
using server.Domain.Models;
using server.Domain.Models.Common;
using server.Persistence;
using server.Persistence.Interfaces;

namespace server.src.Application.Services;

public class TransactionsService : ITransactionsService
{
    private readonly DataContext _context;
    private readonly ICommonRepository _commonRepository;

    public TransactionsService(DataContext context, ICommonRepository commonRepository)
    {
        _context = context;
        _commonRepository = commonRepository;
    }


    public async Task<ListResponse<List<TransactionDto>>> GetTransactionsService(UrlQuery pageParams, CancellationToken token)
    {
        // Default Sorting
        if (!pageParams.HasSortBy)
        {
            pageParams.SortBy = Filters.TransactionDateSorting;
            pageParams.SortDescending = true;
        }

        // Filtering
        Expression<Func<Transaction, bool>>? filter = null;
        if (pageParams.HasFilter) filter = pageParams.Filter!.TransactionsFilter();

        var filters = new Expression<Func<Transaction, bool>>[] { filter! };

        // Including
        var includes = new Expression<Func<Transaction, object>>[] { };

        // Paging
        var pagedTransactions = await _commonRepository.GetPagedResultsAsync(_context.Transactions, pageParams, filters, includes, token);
        // Mapping
        var dto = pagedTransactions.Rows.Select(t => t.TransactionDtoDtoMapping()).ToList();

        // Initializing object
        return new ListResponse<List<TransactionDto>>()
        {
            Data = dto,
            Pagination = new PaginatedList
            {
                TotalRecords = pagedTransactions.UrlQuery.TotalRecords,
                PageSize = pagedTransactions.UrlQuery.PageSize,
                PageNumber = pagedTransactions.UrlQuery.PageNumber ?? 1,
            }
        };
    }

    public async Task<ItemResponse<TransactionDto>> GetTransactionByIdService(Guid id, CancellationToken token)
    {
        // Searching Item
        var includes = new Expression<Func<Transaction, object>>[] { };
        var filters = new Expression<Func<Transaction, bool>>[] { x => x.Id == id};
        var transaction = await _commonRepository.GetResultByIdAsync(_context.Transactions, filters, includes, token) ?? 
            throw new CustomException("Transaction was not found", (int)HttpStatusCode.NotFound);

        // Mapping
        var dto = transaction.TransactionDtoDtoMapping();

        // Initializing object
        return new ItemResponse<TransactionDto>()
            .WithData(dto);
    }

    public async Task<CommandResponse<string>> ImportTransactionsService(IFormFile csvFile, CancellationToken token)
    {
        using var stream = csvFile.OpenReadStream();
        using var reader = new StreamReader(stream);
        var headerLine = await reader.ReadLineAsync();
       
        while (!reader.EndOfStream)
        {
            var line = await reader.ReadLineAsync();
            var transaction = line!.ParseLine();
            if (transaction != null)
                await UpsertTransactionService(transaction!, token);
        }

        return new CommandResponse<string>()
            .WithSuccess(true)
            .WithData("Data imported successfully");
    }

    
    public async Task<CommandResponse<string>> UpsertTransactionService(Transaction transaction, CancellationToken token)
    {
        var existingTransaction = await _context.Transactions.FirstOrDefaultAsync(t => t.Id == transaction.Id, token);

        if (existingTransaction == null)
            await _context.Transactions.AddAsync(transaction, token);
        else
        {
            // Get currency symbol using regular expression
            var currencyPattern = new Regex(@"^[^\d]+");
            var existingCurrency = currencyPattern.Match(existingTransaction.Amount).Value;
            var transactionCurrency = currencyPattern.Match(transaction.Amount).Value;

            // Check if currencies match
            if (existingCurrency != transactionCurrency)
                throw new CustomException("Transaction aborted: The currency of the new transaction does not match the existing transaction currency.", statusCode: 400);
               
            _context.Entry(existingTransaction).CurrentValues.SetValues(transaction);
        }
            
        var result = await _context.SaveChangesAsync(token) > 0;

        return new CommandResponse<string>()
            .WithSuccess(result)
            .WithData($"Transaction with id {transaction.Id} upserted successfully");
    }


    public async Task<CommandResponse<string>> DeleteTransactionService(Guid id, CancellationToken token)
    {
        // Searching Item
        var includes = new Expression<Func<Transaction, object>>[] { };
        var filters = new Expression<Func<Transaction, bool>>[] { x => x.Id == id};
        var transaction = await _commonRepository.GetResultByIdAsync(_context.Transactions, filters, includes, token) ?? 
            throw new CustomException("Transaction was not found", (int)HttpStatusCode.NotFound);

        // Deleting
        _context.Transactions.Remove(transaction);
        var result = await _context.SaveChangesAsync(token) > 0;

        // Initializing object
        return new CommandResponse<string>()
            .WithSuccess(result)
            .WithData($"Transaction with id {id} deleted successfully");
    }
}