namespace server.Domain.Dto;

public record TransactionDto(Guid Id, string ApplicationName, string Email,
    string Filename, string Url, string Inception, string Amount, 
    double Allocation);