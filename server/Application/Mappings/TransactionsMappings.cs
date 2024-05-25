using System.Globalization;

using server.Domain.Dto;
using server.Domain.Models;

namespace server.src.Application.Mappings;

public static class TransactionsMappings
{
    public static TransactionDto TransactionDtoDtoMapping(
        this Transaction model) => new (
            Id: model.Id,
            ApplicationName: model.ApplicationName,
            Email: model.Email,
            Filename: model.Filename,
            Url: model.Url,
            Inception: model.Inception.ToLocalTime().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
            Amount: model.Amount,
            Allocation: model.Allocation
        );


    public static Transaction TransactionModelMapping(this string[] values)
    {
        return new Transaction
        {
            Id = string.IsNullOrWhiteSpace(values[0]) ? Guid.NewGuid() : Guid.Parse(values[0]),
            ApplicationName = values[1].Trim(),
            Email = values[2].Trim(),
            Filename = values[3].Trim(),
            Url = values[4].Trim(),
            Inception = DateTime.Parse(values[5], CultureInfo.InvariantCulture),
            Amount = values[6].Trim(),
            Allocation = double.TryParse(values[7], NumberStyles.Any, CultureInfo.InvariantCulture, out double allocation) ? allocation : 0.0
        };
    }
}