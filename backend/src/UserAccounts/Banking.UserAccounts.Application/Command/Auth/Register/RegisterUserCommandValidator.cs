using Banking.Core.Validation;
using Banking.SharedKernel;
using Banking.UserAccounts.Domain.ValueObjects;
using FluentValidation;

namespace Banking.UserAccounts.Application.Command.Auth.Register;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        When(r => r.AccountType == AccountType.Corporate, () =>
        {
            RuleFor(r => r.CorporateAccountDto)
                .NotNull().
                WithMessage("Corporate account details must be provided for corporate accounts.");
            
            RuleFor(r => r.CorporateAccountDto!.RegisterDto.Email).
                MustBeValueObject(Email.Create);
            
            RuleFor(r => r.CorporateAccountDto!.RegisterDto.UserName).
                MustBeValueObject(Name.Create);
            
            RuleFor(r => r.CorporateAccountDto!.RegisterDto.Password).
                NotNull().NotEmpty().
                WithMessage("Password must be provided.");
            
            RuleFor(r => r.CorporateAccountDto!.CompanyName).
                MustBeValueObject(Name.Create);
            
            RuleFor(r => r.CorporateAccountDto!.ContactEmail).
                MustBeValueObject(Email.Create);
            
            RuleFor(r => r.CorporateAccountDto!.ContactPhone).
                MustBeValueObject(PhoneNumber.Create);
            
            RuleFor(r => r.CorporateAccountDto!.TaxId).
                MustBeValueObject(TaxId.Create);

            RuleFor(r => r.CorporateAccountDto!.Address).MustBeValueObject(ad =>
                Address.Create(
                    ad.Country, ad.City, ad.Street, ad.HouseNumber));
            
            RuleFor(r => r.CorporateAccountDto!.FullName).MustBeValueObject(f =>
                FullName.Create(f.FirstName, f.MiddleName, f.LastName));
        });
        
        When(r => r.AccountType == AccountType.Individual, () =>
        {
            RuleFor(r => r.IndividualAccountDto)
                .NotNull().
                WithMessage("Individual account details must be provided for individual accounts.");
            
            RuleFor(r => r.IndividualAccountDto!.RegisterDto.Email).
                MustBeValueObject(Email.Create);
            
            RuleFor(r => r.IndividualAccountDto!.RegisterDto.UserName).
                MustBeValueObject(Name.Create);
            
            RuleFor(r => r.IndividualAccountDto!.RegisterDto.Password).
                NotNull().NotEmpty().
                WithMessage("Password must be provided.");
            
            RuleFor(r => r.IndividualAccountDto!.PhoneNumber).
                MustBeValueObject(PhoneNumber.Create);
            
            RuleFor(r => r.IndividualAccountDto!.ContactEmail).
                MustBeValueObject(Email.Create);
            
            RuleFor(r => r.IndividualAccountDto!.DateOfBirth).
                MustBeValueObject(DateOfBirth.Create);

            When(r => r.IndividualAccountDto!.Photo != null, () =>
            {
                RuleFor(r => r.IndividualAccountDto!.Photo).
                    MustBeValueObject(Photo.Create!);
            });
            
            RuleFor(r => r.IndividualAccountDto!.Address).MustBeValueObject(ad =>
                Address.Create(
                    ad.Country, ad.City, ad.Street, ad.HouseNumber));
            
            RuleFor(r => r.IndividualAccountDto!.FullName).MustBeValueObject(f =>
                FullName.Create(f.FirstName, f.MiddleName, f.LastName));
        });
    }
}