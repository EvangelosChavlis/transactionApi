using Microsoft.AspNetCore.Mvc;
using server.Application.Interfaces;
using server.Domain.Models;
using server.Domain.Models.Common;

namespace WebApi.Controllers;

[ApiController]
[Route("api/transactions")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionsService _transactionsService;

    public TransactionsController(ITransactionsService transactionsService)
    {
        _transactionsService = transactionsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTransactions([FromQuery] UrlQuery urlQuery, CancellationToken token)
        => Ok(await _transactionsService.GetTransactionsService(urlQuery, token));

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetTransaction(Guid id, CancellationToken token)
        => Ok(await _transactionsService.GetTransactionByIdService(id, token));


    public class Create
    {
        public IFormFile File { get; set; }
    }

    [HttpPost]
    [Route("upload")]
    public async Task<IActionResult> UploadCsv([FromForm] Create dto, CancellationToken token)
        => Ok(await _transactionsService.ImportTransactionsService(dto.File, token));


    [HttpPost]
    public async Task<IActionResult> UpsertTransactionTransaction([FromBody] Transaction transaction, CancellationToken token)
        => Ok(await _transactionsService.UpsertTransactionService(transaction, token));

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteTransaction(Guid id, CancellationToken token)
        => Ok(await _transactionsService.DeleteTransactionService(id, token));
}
