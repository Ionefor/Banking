using FluentValidation;

namespace Banking.BankAccounts.Application.Command.Accounts.Create;

public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor(c => c.UserAccountId).
            NotEmpty().NotNull();
    }
}