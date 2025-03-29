using Banking.Core.Validation;
using Banking.SharedKernel.ValueObjects;
using FluentValidation;

namespace Banking.Users.Application.Features.Commands.Register.CorporateAccount;

public class RegisterCorporateCommandValidator  :
    AbstractValidator<RegisterCorporateCommand>
{
    public RegisterCorporateCommandValidator()
    {
        RuleFor(i => i.Register.Password).
            NotNull().NotEmpty().
            WithMessage("Password must be provided.");
        
        RuleFor(i => i.Register.Email).
            MustBeValueObject(Email.Create);
        
        RuleFor(i => i.Register.UserName).
            MustBeValueObject(Name.Create);
        
        RuleFor(i => i.CorporateAccount.Address).
            MustBeValueObject(ad => Address.Create(
                ad.Country, ad.City, ad.Street, ad.HouseNumber));
        
        RuleFor(i => i.CorporateAccount.CompanyName).
            MustBeValueObject(Name.Create);
        
        RuleFor(i => i.CorporateAccount.ContactPhone).
            MustBeValueObject(PhoneNumber.Create);
        
        RuleFor(i => i.CorporateAccount.TaxId).
            MustBeValueObject(TaxId.Create);
    }
}
