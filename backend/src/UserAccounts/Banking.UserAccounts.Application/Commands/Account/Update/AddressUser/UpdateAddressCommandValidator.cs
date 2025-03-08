using Banking.Core.Validation;
using Banking.UserAccounts.Domain.ValueObjects;
using FluentValidation;

namespace Banking.UserAccounts.Application.Commands.Account.Update.AddressUser;

public class UpdateAddressCommandValidator : AbstractValidator<UpdateAddressCommand>
{
    public UpdateAddressCommandValidator()
    {
        RuleFor(u => u.UserId).
            NotEmpty().NotNull().WithMessage("UserId cannot be empty");

        RuleFor(u => u.Address).
            MustBeValueObject(ad => Address.Create(
                ad.Country, ad.City, ad.Street, ad.HouseNumber));
    }
}