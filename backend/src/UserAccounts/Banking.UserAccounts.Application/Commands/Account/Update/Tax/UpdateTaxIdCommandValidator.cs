using Banking.Core.Validation;
using Banking.UserAccounts.Domain.ValueObjects;
using FluentValidation;

namespace Banking.UserAccounts.Application.Commands.Account.Update.Tax;

public class UpdateTaxIdCommandValidator : AbstractValidator<UpdateTaxIdCommand>
{
    public UpdateTaxIdCommandValidator()
    {
        RuleFor(u => u.UserId).
            NotEmpty().NotNull().WithMessage("UserId cannot be empty");

        RuleFor(u => u.TaxId).
            MustBeValueObject(TaxId.Create);
    }
}