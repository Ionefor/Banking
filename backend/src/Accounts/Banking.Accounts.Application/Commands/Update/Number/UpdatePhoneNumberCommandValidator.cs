using Banking.Core.Validation;
using Banking.SharedKernel.ValueObjects;
using FluentValidation;

namespace Banking.Accounts.Application.Commands.Update.Number;

public class UpdatePhoneNumberCommandValidator : AbstractValidator<UpdatePhoneNumberCommand>
{
    public UpdatePhoneNumberCommandValidator()
    {
        RuleFor(u => u.AccountId).
            NotEmpty().NotNull().WithMessage("UserId cannot be empty");

        RuleFor(u => u.PhoneNumber).
            MustBeValueObject(PhoneNumber.Create);
    }
}