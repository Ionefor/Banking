using Banking.Core.Abstractions;
using Banking.Core.Extension;
using Banking.SharedKernel.Definitions;
using Banking.SharedKernel.Models.Errors;
using Banking.UserAccounts.Application.Abstractions;
using Banking.UserAccounts.Domain;
using Banking.UserAccounts.Domain.ValueObjects;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Banking.UserAccounts.Application.Commands.Account.Update.AddressUser;

public class UpdateAddressHandler : ICommandHandler<Guid, UpdateAddressCommand>
{
    private readonly IValidator<UpdateAddressCommand> _validator;
    private readonly IAccountManager _accountManager;
    private readonly UserManager<User> _userManager;
    private readonly ILogger<UpdateAddressHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAddressHandler(
        IValidator<UpdateAddressCommand> validator,
        IAccountManager accountManager,
        UserManager<User> userManager,
        ILogger<UpdateAddressHandler> logger,
        [FromKeyedServices(ModulesName.UserAccounts)]IUnitOfWork unitOfWork)
    {
        _validator = validator;
        _accountManager = accountManager;
        _userManager = userManager;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<Guid, ErrorList>> Handle(
        UpdateAddressCommand command,
        CancellationToken cancellationToken = default)
    { 
        var validationResult = await _validator.
            ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            return validationResult.ToErrorList();
        
        var user = await _userManager.
            FindByIdAsync(command.UserId.ToString());
        
        if (user is null)
        {
            return Errors.General.
                NotFound(new ErrorParameters.General.NotFound
                    (nameof(User), nameof(command.UserId), command.UserId)).ToErrorList();
        }
        
        var accounts = await _accountManager.GetAccountByUserId(command.UserId);

        var address = Address.Create(
            command.Address.Country,
            command.Address.City,
            command.Address.Street,
            command.Address.HouseNumber).Value;
        
        if (accounts.Item1 is not null)
        {
            accounts.Item1.UpdateAddress(address);
        }
        else if(accounts.Item2 is not null)
        {
            accounts.Item2.UpdateAddress(address);
        }
        else
        {
            return Errors.General.
                NotFound(new ErrorParameters.General.NotFound
                    (nameof(Account), nameof(command.UserId), command.UserId)).ToErrorList();
        }
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.
            LogInformation("User with ID {UserId} update address.", command.UserId);

        return command.UserId;
    }
}