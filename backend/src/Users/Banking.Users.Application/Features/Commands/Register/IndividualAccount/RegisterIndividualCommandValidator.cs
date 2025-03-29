using Banking.Core.Validation;
using Banking.SharedKernel.ValueObjects;
using FluentValidation;

namespace Banking.Users.Application.Features.Commands.Register.IndividualAccount;

public class RegisterIndividualCommandValidator :
    AbstractValidator<RegisterIndividualCommand>
{
    public RegisterIndividualCommandValidator()
    {
        RuleFor(i => i.Register.Password).
            NotNull().NotEmpty().
            WithMessage("Password must be provided.");
        
        RuleFor(i => i.Register.Email).
          MustBeValueObject(Email.Create);
        
        RuleFor(i => i.Register.UserName).
            MustBeValueObject(Name.Create);
        
        RuleFor(i => i.IndividualAccount.Address).
            MustBeValueObject(ad => Address.Create(
                ad.Country, ad.City, ad.Street, ad.HouseNumber));
        
        RuleFor(i => i.IndividualAccount.FullName).MustBeValueObject(f =>
            FullName.Create(f.FirstName, f.MiddleName, f.LastName));
        
        RuleFor(i => i.File.FileName).
            MustBeValueObject(FilePath.Create);
        
        RuleFor(i => i.IndividualAccount.DateOfBirth).
            MustBeValueObject(DateOfBirth.Create);
        
        RuleFor(i => i.IndividualAccount.PhoneNumber).
            MustBeValueObject(PhoneNumber.Create);
    }
}