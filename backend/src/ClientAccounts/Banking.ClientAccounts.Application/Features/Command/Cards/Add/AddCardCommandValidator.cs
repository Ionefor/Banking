using Banking.ClientAccounts.Domain.ValueObjects;
using Banking.Core.Validation;
using FluentValidation;

namespace Banking.BankAccounts.Application.Features.Command.Cards.Add;

public class AddCardCommandValidator : AbstractValidator<AddCardCommand>
{
    public AddCardCommandValidator()
    {
        RuleFor(c => c.ClientAccountId).
            NotEmpty().NotNull().WithMessage("ClientAccountId cannot be empty");

        RuleFor(c => c.BankAccountId)
            .NotEmpty().NotNull().WithMessage("BankAccountId cannot be empty");

        RuleFor(c => c.PaymentDetails).
            MustBeValueObject(PaymentDetails.Create);

        RuleFor(c => c.Ccv).
            MustBeValueObject(Ccv.Create);

        RuleFor(c => c.ValidThru).
            NotEmpty().WithMessage("ValidThru cannot be empty");
    }
}