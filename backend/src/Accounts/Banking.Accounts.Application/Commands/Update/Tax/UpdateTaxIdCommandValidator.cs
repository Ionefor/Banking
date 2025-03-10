using Banking.Core.Validation;
using Banking.SharedKernel.ValueObjects;
using FluentValidation;

namespace Banking.Accounts.Application.Commands.Update.Tax;

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