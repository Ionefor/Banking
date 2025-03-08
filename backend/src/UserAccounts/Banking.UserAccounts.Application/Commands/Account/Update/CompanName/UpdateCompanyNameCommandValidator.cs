using Banking.Core.Validation;
using FluentValidation;

namespace Banking.UserAccounts.Application.Commands.Account.Update.CompanName;

public class UpdateCompanyNameCommandValidator : AbstractValidator<UpdateCompanyNameCommand>
{
    public UpdateCompanyNameCommandValidator()
    {
        RuleFor(u => u.UserId).
            NotEmpty().NotNull().WithMessage("UserId cannot be empty");

        RuleFor(u => u.CompanyName).
            MustBeValueObject(Domain.ValueObjects.Name.Create);
    }
}