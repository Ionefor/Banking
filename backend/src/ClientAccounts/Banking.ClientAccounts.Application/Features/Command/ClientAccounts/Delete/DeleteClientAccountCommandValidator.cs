using FluentValidation;

namespace Banking.BankAccounts.Application.Features.Command.ClientAccounts.Delete;

public class DeleteClientAccountCommandValidator :
    AbstractValidator<DeleteClientAccountCommand>
{
    public DeleteClientAccountCommandValidator()
    {
        RuleFor(c => c.Id).
            NotEmpty().NotNull();
    }
}