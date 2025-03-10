using Banking.Core.Validation;
using FluentValidation;

namespace Banking.Accounts.Application.Commands.Update.CompanName;

public class UpdateCompanyNameCommandValidator : AbstractValidator<UpdateCompanyNameCommand>
{
    public UpdateCompanyNameCommandValidator()
    {
        RuleFor(u => u.UserId).
            NotEmpty().NotNull().WithMessage("UserId cannot be empty");

        RuleFor(u => u.CompanyName).
            MustBeValueObject(SharedKernel.ValueObjects.Name.Create);
    }
}