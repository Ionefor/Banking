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

namespace Banking.UserAccounts.Application.Commands.Account.Update.Photos;

public class UpdatePhotoHandler : ICommandHandler<Guid, UpdatePhotoCommand>
{
    private readonly IValidator<UpdatePhotoCommand> _validator;
    private readonly IAccountManager _accountManager;
    private readonly UserManager<User> _userManager;
    private readonly ILogger<UpdatePhotoHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePhotoHandler(
        IValidator<UpdatePhotoCommand> validator,
        IAccountManager accountManager,
        UserManager<User> userManager,
        ILogger<UpdatePhotoHandler> logger,
        [FromKeyedServices(ModulesName.UserAccounts)]IUnitOfWork unitOfWork)
    {
        _validator = validator;
        _accountManager = accountManager;
        _userManager = userManager;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<Guid, ErrorList>> Handle(
        UpdatePhotoCommand command,
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
        
        var individualAccount = await _accountManager.GetAccountByUserId(command.UserId);

        var photo = FilePath.Create(command.FilePath).Value;
        
        if (individualAccount.Item1 is not null)
        {
            individualAccount.Item1.UpdatePhoto(photo);
        }
        else
        {
            return Errors.General.
                NotFound(new ErrorParameters.General.NotFound
                    (nameof(Account), nameof(command.UserId), command.UserId)).ToErrorList();
        }
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.
            LogInformation("User with ID {UserId} update photo.", command.UserId);

        return command.UserId;
    }
}