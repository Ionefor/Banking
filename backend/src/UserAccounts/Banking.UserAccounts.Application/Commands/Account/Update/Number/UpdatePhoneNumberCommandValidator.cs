using Banking.Core.Validation;
using Banking.UserAccounts.Domain.ValueObjects;
using FluentValidation;

namespace Banking.UserAccounts.Application.Commands.Account.Update.Number;

public class UpdatePhoneNumberCommandValidator : AbstractValidator<UpdatePhoneNumberCommand>
{
    public UpdatePhoneNumberCommandValidator()
    {
        RuleFor(u => u.UserId).
            NotEmpty().NotNull().WithMessage("UserId cannot be empty");

        RuleFor(u => u.PhoneNumber).
            MustBeValueObject(PhoneNumber.Create);
    }
}