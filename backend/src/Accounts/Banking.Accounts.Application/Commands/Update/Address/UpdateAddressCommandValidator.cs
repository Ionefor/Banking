using Banking.Core.Validation;
using FluentValidation;

namespace Banking.Accounts.Application.Commands.Update.Address;

public class UpdateAddressCommandValidator : AbstractValidator<UpdateAddressCommand>
{
    public UpdateAddressCommandValidator()
    {
        RuleFor(u => u.AccountId).
            NotEmpty().NotNull().WithMessage("UserId cannot be empty");

        RuleFor(u => u.Address).
            MustBeValueObject(ad => SharedKernel.ValueObjects.Address.Create(
                ad.Country, ad.City, ad.Street, ad.HouseNumber));
    }
}