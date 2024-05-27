using FluentValidation;

using server.Domain.Models;
using System.Text.RegularExpressions;

namespace server.Application.Validators;

public class TransactionValidator : AbstractValidator<Transaction>
{
    public TransactionValidator()
    {
        RuleFor(t => t.Id)
            .NotEmpty().WithMessage("Id is mandatory.");

        RuleFor(t => t.ApplicationName)
            .NotEmpty()
            .WithMessage("ApplicationName is mandatory.")
            .MaximumLength(200)
            .WithMessage("ApplicationName cannot exceed 200 characters.");

        RuleFor(t => t.Email)
            .NotEmpty()
            .WithMessage("Email is mandatory.")
            .MaximumLength(200)
            .WithMessage("Email cannot exceed 200 characters.")
            .EmailAddress()
            .WithMessage("Email must be a valid email address.");

        RuleFor(t => t.Filename)
            .MaximumLength(300)
            .WithMessage("Filename cannot exceed 300 characters.")
            .Must(BeAValidFileExtension)
            .WithMessage("Filename must have a valid extension (png, mp3, tiff, xls, pdf).")
            .When(t => !string.IsNullOrEmpty(t.Filename));

        RuleFor(t => t.Url)
            .Must(BeAValidUrl)
            .WithMessage("Url must be a valid URL.")
            .When(t => !string.IsNullOrEmpty(t.Url));

        RuleFor(t => t.Inception)
            .NotEmpty()
            .WithMessage("Inception is mandatory.")
            .Must(BeInThePast)
            .WithMessage("Inception date must be in the past.");

        RuleFor(t => t.Amount)
            .NotEmpty()
            .WithMessage("Amount is mandatory.")
            .Must(BeAValidAmount)
            .WithMessage("Amount must be a valid currency value.");

        RuleFor(t => t.Allocation)
            .InclusiveBetween(0, 100)
            .WithMessage("Allocation must be a positive decimal between 0 and 100.")
            .When(t => t.Allocation > 0);
    }

    private bool BeAValidFileExtension(string filename)
    {
        var validExtensions = new[] { ".png", ".mp3", ".tiff", ".xls", ".pdf" };
        var extension = System.IO.Path.GetExtension(filename)?.ToLower();
        return validExtensions.Contains(extension);
    }

    private bool BeAValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out _);
    }

    private bool BeInThePast(DateTime inception)
    {
        return inception < DateTime.Now;
    }

    private bool BeAValidAmount(string amount)
    {
        var currencyPattern = new Regex(@"^[^\d]+");
        var currency = currencyPattern.Match(amount).Value;
        
        if (currency == string.Empty) return false;
        return true;  
    }
}
