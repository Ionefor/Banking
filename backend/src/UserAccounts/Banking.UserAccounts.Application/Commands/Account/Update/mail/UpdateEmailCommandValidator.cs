using Banking.Core.Validation;
using Banking.UserAccounts.Domain.ValueObjects;
using FluentValidation;

namespace Banking.UserAccounts.Application.Commands.Account.Update.mail;

public class UpdateEmailCommandValidator : AbstractValidator<UpdateEmailCommand>
{
    public UpdateEmailCommandValidator()
    {
        RuleFor(u => u.UserId).
            NotEmpty().NotNull().WithMessage("UserId cannot be empty");

        RuleFor(u => u.Email).
            MustBeValueObject(Email.Create);
    }
}