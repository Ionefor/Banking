using FluentValidation;

namespace Banking.BankAccounts.Application.Features.Command.ClientAccounts.SoftDelete;

public class SoftDeleteCACommandValidator :
    AbstractValidator<SoftDeleteCACommand>
{
    public SoftDeleteCACommandValidator()
    {
        RuleFor(c => c.Id).
            NotEmpty().NotNull();
    }
}