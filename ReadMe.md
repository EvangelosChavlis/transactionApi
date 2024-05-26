# Transactions API

This is the repository for the Transactions API project. This project is a .NET C# solution structured in multiple layers following the Clean Architecture principles.

## Folder Structure

### Root Directory

- `server.sln`: The solution file that includes all the projects in the repository.

### Application Layer

Located at `.\server\Application`

- `Application.csproj`: Project file for the Application layer.
- `DependencyInjection.cs`: Contains dependency injection configurations.
- `Helpers`: Directory containing helper classes.
  - `CsvExtension.cs`: Helper class for CSV file operations.
  - `Filters.cs`: Helper class for filtering logic.
- `Interfaces`: Directory containing interface definitions.
  - `ITransactionsService.cs`: Interface for transaction services.
- `Mappings`: Directory containing mapping configurations.
  - `TransactionsMappings.cs`: Mapping profile for transactions.
- `Services`: Directory containing service implementations.
  - `TransactionsService.cs`: Implementation of the transactions service.
- `Validators`: Directory containing validation logic.
  - `TransactionValidator.cs`: Validator for transaction entities.

### Domain Layer

Located at `.\transactionsApi\server\Domain`

- `Domain.csproj`: Project file for the Domain layer.
- `Dto`: Directory containing Data Transfer Objects (DTOs).
  - `TransactionDto.cs`: DTO for transactions.
  - `Common`: Directory for common DTOs.
    - `CommandResponse.cs`: DTO for command responses.
    - `ItemResponse.cs`: DTO for item responses.
    - `ListResponse.cs`: DTO for list responses.
    - `PaginatedList.cs`: DTO for paginated lists.
- `Models`: Directory containing domain models.
  - `Transaction.cs`: Domain model for transactions.
  - `Common`: Directory for common models.
    - `Envelope.cs`: Model for response envelope.
    - `UrlQuery.cs`: Model for URL query parameters.
  - `Errors`: Directory for error-related models.
    - `ErrorDetails.cs`: Model for error details.
    - `LogError.cs`: Model for log errors.

### Persistence Layer

Located at `.\transactionsApi\server\Persistence`

- `Persistence.csproj`: Project file for the Persistence layer.
- `Configurations`: Directory containing entity configurations.
  - `TransactionConfiguration.cs`: Configuration for the transaction entity.
  - `LogErrorConfiguration.cs`: Configuration for the log-error entity.
- `DataContext.cs`: Database context class.
- `DependencyInjection.cs`: Contains dependency injection configurations for the Persistence layer.
- `Interfaces`: Directory containing repository interface definitions.
  - `ICommonRepository.cs`: Interface for common repository operations.
- `Repositories`: Directory containing repository implementations.
  - `CommonRepository.cs`: Implementation of the common repository.

### WebApi Layer

Located at `.\transactionsApi\server\WebApi`

- `WebApi.csproj`: Project file for the Web API layer.
- `Controllers`: Directory containing API controllers.
- `Middlewares`: Directory containing middleware components.
  - `ExceptionHandlingMiddleware.cs`: Middleware for handling exceptions.
- `Program.cs`: Entry point for the Web API application.
- `transaction.db`: The database file used by the application.

## Getting Started

To get started with the Transactions API, follow these steps:

1. ```git clone https://github.com/yourusername/transactionsApi.git```

2. ```cd .\transactionsApi\server```

3. ```dotnet run --project WebApi```

4. open ```http://localhost:5000/swagger/index.html```