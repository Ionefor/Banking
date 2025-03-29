using Banking.Accounts.Contracts;
using Banking.Core.Abstractions;
using Banking.Core.Extension;
using Banking.SharedKernel.Definitions;
using Banking.SharedKernel.Models.Errors;
using Banking.Users.Domain;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Banking.Users.Application.Features.Commands.Register.CorporateAccount;

public class RegisterCorporateHandler :
    ICommandHandler<RegisterCorporateCommand>
{
    private readonly IValidator<RegisterCorporateCommand> _validator;
    private readonly IAccountsContract _contract;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly ILogger<RegisterCorporateHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterCorporateHandler(
        IValidator<RegisterCorporateCommand> validator,
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        IAccountsContract contract,
        ILogger<RegisterCorporateHandler> logger,
        [FromKeyedServices(ModulesName.Users)]IUnitOfWork unitOfWork)
    {
        _validator = validator;
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _contract = contract;
    }
    
    public async Task<UnitResult<ErrorList>> Handle(
        RegisterCorporateCommand command,
        CancellationToken cancellationToken = default)
    {
        var transaction = await _unitOfWork.
            BeginTransaction(cancellationToken);
        
        try
        {
            var validationResult = await _validator.
                ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                return validationResult.ToErrorList();
            
            var corporateRole = await _roleManager.
                FindByNameAsync(Constants.Accounts.Corporate);
            
            if (corporateRole is null)
            {
                return Errors.General.
                    NotFound(new ErrorParameters.NotFound(nameof(Role),
                        nameof(Constants.Accounts.Corporate), 
                            Constants.Accounts.Corporate)).ToErrorList();
            }
            
            var user = User.CreateCorporate(
                command.Register.UserName,  command.Register.Email, corporateRole);
            
            if (user.IsFailure)
                return user.Error.ToErrorList();
            
            var result = await _userManager.
                CreateAsync(user.Value,  command.Register.Password);
        
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e =>
                    Errors.General.Failed(new ErrorParameters.Failed(e.Description)));
              
                return new ErrorList(errors);
            }

            var resultCreate = await _contract.CreateCorporateAccount(
                user.Value.Id, command.CorporateAccount.Address,
                command.CorporateAccount.CompanyName, command.CorporateAccount.TaxId,
                command.Register.Email, command.CorporateAccount.ContactPhone, cancellationToken);
            
            if (resultCreate.IsFailure)
                return resultCreate.Error;
            
            _logger.LogInformation("Register user with username {Name}," +
                     " and created corporate account", command.Register.UserName);
            
            transaction.Commit();
            
            return UnitResult.Success<ErrorList>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Can not register user with corporate account in transaction");

            transaction.Rollback();

            return Errors.General.Failed(new ErrorParameters.Failed(
                "Can not register user with corporate account in transaction")).ToErrorList();
        }
    }
}