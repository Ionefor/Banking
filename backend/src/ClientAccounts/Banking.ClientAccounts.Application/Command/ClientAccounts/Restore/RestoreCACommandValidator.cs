using FluentValidation;

namespace Banking.BankAccounts.Application.Command.ClientAccounts.Restore;

public class RestoreCACommandValidator :
    AbstractValidator< RestoreClientAccountCommand>
{
    public RestoreCACommandValidator()
    {
        RuleFor(c => c.Id).
            NotEmpty().NotNull();
    }
}