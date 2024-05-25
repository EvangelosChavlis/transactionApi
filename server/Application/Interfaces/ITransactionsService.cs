using Microsoft.AspNetCore.Http;

using server.Domain.Dto;
using server.Domain.Dto.Common;
using server.Domain.Models;
using server.Domain.Models.Common;

namespace server.Application.Interfaces;

public interface ITransactionsService
{
    Task<ListResponse<List<TransactionDto>>> GetTransactionsService(UrlQuery pageParams, CancellationToken token);

    Task<ItemResponse<TransactionDto>> GetTransactionByIdService(Guid id, CancellationToken token);

    Task<CommandResponse<string>> ImportTransactionsService(IFormFile csvFile, CancellationToken token);

    Task<CommandResponse<string>> UpsertTransactionService(Transaction transaction, CancellationToken token);

    Task<CommandResponse<string>> DeleteTransactionService(Guid id, CancellationToken token);
}