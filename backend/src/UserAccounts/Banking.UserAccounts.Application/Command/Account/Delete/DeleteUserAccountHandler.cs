using Banking.Core.Abstractions;
using Banking.SharedKernel.Definitions;
using Banking.SharedKernel.Models.Errors;
using Banking.UserAccounts.Application.Abstractions;
using Banking.UserAccounts.Domain;
using Banking.UserAccounts.Domain.Accounts;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Banking.UserAccounts.Application.Command.Account.Delete;

public class DeleteUserAccountHandler : ICommandHandler<Guid, DeleteUserAccountCommand>
{
    private readonly ILogger<DeleteUserAccountCommand> _logger;
    private readonly IAccountManager _accountManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;

    public DeleteUserAccountHandler(
        UserManager<User> userManager,
        ILogger<DeleteUserAccountCommand> logger,
        IAccountManager accountManager,
        [FromKeyedServices(ModulesName.UserAccounts)]IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _accountManager = accountManager;
        _userManager = userManager;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<Guid, ErrorList>> Handle(
        DeleteUserAccountCommand command,
        CancellationToken cancellationToken = default)
    {
        var transaction = await _unitOfWork.BeginTransaction(cancellationToken);

        try
        {
            var user = await _userManager.
                FindByIdAsync(command.UserId.ToString());

            if (user is null)
            {
                return Errors.General.
                    NotFound(new ErrorParameters.General.NotFound
                        (nameof(User), nameof(command.UserId), command.UserId)).ToErrorList();
            }
            
            var accounts = await _accountManager.GetAccountByUserId(command.UserId);
            
            if (accounts.Item1 is not null)
            {
                await DeleteIndividualAccount(accounts.Item1, cancellationToken);
            }
            else if(accounts.Item2 is not null)
            {
                await DeleteCorporateAccount(accounts.Item2, cancellationToken);
            }
            else
            {
                return Errors.General.
                    NotFound(new ErrorParameters.General.NotFound
                        (nameof(Account), nameof(command.UserId), command.UserId)).ToErrorList();
            }
            
            var deletingUser = await _userManager.DeleteAsync(user);

            if (!deletingUser.Succeeded)
            {
                var errors = deletingUser.Errors.Select(e =>
                    Errors.General.Failed(new ErrorParameters.General.Failed(e.Description)));
              
                return new ErrorList(errors);
            }

            _logger.
                LogInformation("User with ID {UserId} has been hard deleted.", command.UserId);
            
            transaction.Commit();
            
            return command.UserId;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Can not delete user with ID {UserId} in transaction", command.UserId);

            transaction.Rollback();

            return Errors.General.
                Failed(new ErrorParameters.General.
                    Failed("Can not delete user with ID {UserId} in transaction")).ToErrorList();
        }
    }

    private async Task DeleteIndividualAccount(
        IndividualAccount individualAccount, CancellationToken cancellationToken)
    {
        await _accountManager.DeleteIndividualAccount(individualAccount);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
    
    private async Task DeleteCorporateAccount(
        CorporateAccount corporateAccount, CancellationToken cancellationToken)
    {
        await _accountManager.DeleteCorporateAccount(corporateAccount);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}