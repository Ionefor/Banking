using Banking.Core.Abstractions;
using Banking.Core.Extension;
using Banking.SharedKernel;
using Banking.SharedKernel.Models.Errors;
using Banking.UserAccounts.Application.Abstractions;
using Banking.UserAccounts.Contracts.Dto;
using Banking.UserAccounts.Domain;
using Banking.UserAccounts.Domain.Accounts;
using Banking.UserAccounts.Domain.ValueObjects;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Banking.UserAccounts.Application.Command.Auth.Register;

public record RegisterUserHandler : ICommandHandler<RegisterUserCommand>
{
    private readonly IValidator<RegisterUserCommand> _validator;
    private readonly IAccountManager _accountManager;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly ILogger<RegisterUserHandler> _logger;

    public RegisterUserHandler(
        IValidator<RegisterUserCommand> validator,
        IAccountManager accountManager,
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        ILogger<RegisterUserHandler> logger)
    {
        _validator = validator;
        _accountManager = accountManager;
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
    }
    
    public async Task<UnitResult<ErrorList>> Handle(
        RegisterUserCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            return validationResult.ToErrorList();
        
        if (command.AccountType == AccountType.Individual)
            return await CreateIndividualAccount(command.IndividualAccountDto!);
        
        return await CreateCorporateAccount(command.CorporateAccountDto!);
    }

    private async Task<UnitResult<ErrorList>> CreateIndividualAccount(IndividualAccountDto dto)
    {
        var fullName = FullName.
            Create(
                dto.FullName.FirstName,
                dto.FullName.MiddleName,
                dto.FullName.LastName).Value;
        
        var individualRole = await _roleManager.FindByNameAsync(IndividualAccount.Individual);
            
        if (individualRole is null)
        {
            return Errors.General.
                NotFound(new ErrorParameters.General.NotFound(
                    nameof(Role), nameof(IndividualAccount.Individual),
                    IndividualAccount.Individual)).ToErrorList();
        }
            
        var user = User.
            CreateIndividualAccount(
                fullName, dto.RegisterDto.UserName,
                dto.RegisterDto.Email, individualRole);
            
        if (user.IsFailure)
            return user.Error.ToErrorList();
            
        var result = await _userManager.
            CreateAsync(user.Value, dto.RegisterDto.Password);
        
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e =>
                Errors.General.Failed(new ErrorParameters.General.Failed(e.Description)));
              
            return new ErrorList(errors);
        }

        var address = Address.Create(
            dto.Address.Country,
            dto.Address.City,
            dto.Address.Street,
            dto.Address.HouseNumber).Value;
            
        var email = Email.Create(dto.ContactEmail).Value;
            
        var phoneNumber = PhoneNumber.Create(dto.PhoneNumber).Value;
            
        var photo = Photo.Create(dto.Photo).Value ;
        
        var dateOfBirth = DateOfBirth.Create(dto.DateOfBirth).Value;
        
        var individualAccount= new IndividualAccount(
            user.Value,
            address,
            email,
            phoneNumber,
            dateOfBirth,
            photo);

        await _accountManager.CreateIndividualAccount(individualAccount);

        _logger.
            LogInformation("Created user with username {Name}", dto.RegisterDto.UserName);

        return UnitResult.Success<ErrorList>();
    }
    
    private async Task<UnitResult<ErrorList>> CreateCorporateAccount(CorporateAccountDto dto)
    {
        var fullName = FullName.
            Create(
                dto.FullName.FirstName,
                dto.FullName.MiddleName,
                dto.FullName.LastName).Value;
        
        var  corporateRole = await _roleManager.FindByNameAsync(CorporateAccount.Corporate);
            
            if (corporateRole is null)
            {
                return Errors.General.
                    NotFound(new ErrorParameters.General.NotFound(
                        nameof(Role), nameof(CorporateAccount.Corporate),
                        CorporateAccount.Corporate)).ToErrorList();
            }
            
            var user = User.
                CreateCorporateAccount(
                    fullName, dto.RegisterDto.UserName,
                    dto.RegisterDto.Email, corporateRole);
            
            if (user.IsFailure)
                return user.Error.ToErrorList();
            
            var result = await _userManager.
                CreateAsync(user.Value, dto.RegisterDto.Password);
            
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e =>
                    Errors.General.Failed(new ErrorParameters.General.Failed(e.Description)));
              
                return new ErrorList(errors);
            }

            var address = Address.Create(
                dto.Address.Country,
                dto.Address.City,
                dto.Address.Street,
                dto.Address.HouseNumber).Value;
            
            var email = Email.Create(dto.ContactEmail).Value;
            
            var phoneNumber = PhoneNumber.Create(dto.ContactPhone).Value;
            
            var name = Name.Create(dto.CompanyName).Value;
            
            var taxId = TaxId.Create(dto.TaxId).Value;
            
            var corporateAccount= new CorporateAccount(
                user.Value,
                name,
                address,
                taxId,
                email,
                phoneNumber);

            await _accountManager.CreateCorporateAccount(corporateAccount);

            _logger.
                LogInformation("Created user with username {Name}", dto.RegisterDto.UserName);

            return UnitResult.Success<ErrorList>();
    }
}