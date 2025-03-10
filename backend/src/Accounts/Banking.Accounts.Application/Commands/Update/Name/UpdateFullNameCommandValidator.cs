using Banking.Core.Validation;
using Banking.SharedKernel.ValueObjects;
using FluentValidation;

namespace Banking.Accounts.Application.Commands.Update.Name;

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