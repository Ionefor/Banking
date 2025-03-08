using Banking.Core.Validation;
using Banking.UserAccounts.Domain.ValueObjects;
using FluentValidation;

namespace Banking.UserAccounts.Application.Commands.Account.Update.Name;

public class UpdateFullNameCommandValidator : AbstractValidator<UpdateFullNameCommand>
{
    public UpdateFullNameCommandValidator()
    {
        RuleFor(u => u.UserId).
            NotEmpty().NotNull().WithMessage("UserId cannot be empty");

        RuleFor(u => u.FullName).MustBeValueObject(f =>
            FullName.Create(f.FirstName, f.MiddleName, f.LastName));
    }
}