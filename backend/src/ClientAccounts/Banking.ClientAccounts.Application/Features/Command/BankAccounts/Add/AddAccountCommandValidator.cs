using Banking.ClientAccounts.Domain.ValueObjects;
using Banking.Core.Validation;
using Banking.SharedKernel;
using FluentValidation;

namespace Banking.BankAccounts.Application.Features.Command.BankAccounts.Add;

public class AddAccountCommandValidator : AbstractValidator<AddAccountCommand>
{
    public AddAccountCommandValidator()
    {
        RuleFor(c => c.ClientAccountId).
            NotEmpty().NotNull().WithMessage("ClientAccountId cannot be empty");

        RuleFor(c => c.PaymentDetails).
            MustBeValueObject(PaymentDetails.Create);
        
        RuleFor(c => c.Type).
            MustBeEnum<AddAccountCommand, string, WalletType>();
        
        RuleFor(c => c.Сurrency).
            MustBeEnum<AddAccountCommand, string, Currencies>();
    }
}