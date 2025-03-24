using FluentValidation;

namespace Banking.BankAccounts.Application.Command.ClientAccounts.Create;

public class CreateClientAccountCommandValidator :
    AbstractValidator<CreateClientAccountCommand>
{
    public CreateClientAccountCommandValidator()
    {
        RuleFor(c => c.UserAccountId).
            NotEmpty().NotNull();
    }
}