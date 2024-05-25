using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

using server.Application.Interfaces;
using server.Application.Validators;
using server.Domain.Models;
using server.src.Application.Services;

namespace server.src.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ITransactionsService, TransactionsService>();

        services.AddFluentValidationAutoValidation();
        services.AddScoped<IValidator<Transaction>, TransactionValidator>();

        return services;
    }
}