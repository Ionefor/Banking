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

namespace Banking.Users.Application.Features.Commands.Register.IndividualAccount;

public class RegisterIndividualHandler :
    ICommandHandler<RegisterIndividualCommand>
{
    private readonly IValidator<RegisterIndividualCommand> _validator;
    private readonly IAccountsContract _contract;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly ILogger<RegisterIndividualHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterIndividualHandler(
        IValidator<RegisterIndividualCommand> validator,
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        IAccountsContract contract,
        ILogger<RegisterIndividualHandler> logger,
        [FromKeyedServices(ModulesName.Users)]IUnitOfWork unitOfWork)
    {
        _validator = validator;
        _userManager = userManager;
        _roleManager = roleManager;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _contract = contract;
    }
    
    public async Task<UnitResult<ErrorList>> Handle(
        RegisterIndividualCommand command,
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
        
            var individualRole = await _roleManager.
                FindByNameAsync(Constants.Accounts.Individual);
            
            if (individualRole is null)
            {
                return Errors.General.
                    NotFound(new ErrorParameters.NotFound(nameof(Role),
                        nameof(Constants.Accounts.Individual),
                            Constants.Accounts.Individual)).ToErrorList();
            }
            
            var user = User.
                CreateIndividual(command.Register.UserName,  command.Register.Email, individualRole);
            
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
        
            var resultCreate = await _contract.CreateIndividualAccount(
                user.Value.Id, command.IndividualAccount.FullName,
                command.IndividualAccount.Address, command.File,
                command.IndividualAccount.PhoneNumber, command.IndividualAccount.DateOfBirth,
                command.Register.Email, cancellationToken);

            if (resultCreate.IsFailure)
                return resultCreate.Error;
            
            _logger.
                LogInformation("Register user with username {Name}," +
                " and created individual account", command.Register.UserName);
        
            transaction.Commit();
            
            return UnitResult.Success<ErrorList>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Can not register user with individual account in transaction");

            transaction.Rollback();

            return Errors.General.Failed(new ErrorParameters.Failed(
                "Can not register user with individual account in transaction")).ToErrorList();
        }
    }
}