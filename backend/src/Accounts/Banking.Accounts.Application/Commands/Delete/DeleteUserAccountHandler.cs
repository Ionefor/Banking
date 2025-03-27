using Banking.Accounts.Application.Abstractions;
using Banking.Accounts.Domain;
using Banking.Core.Abstractions;
using Banking.SharedKernel.Definitions;
using Banking.SharedKernel.Models.Errors;
using Banking.Users.Contracts;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Banking.Accounts.Application.Commands.Delete;

public class DeleteUserAccountHandler : 
    ICommandHandler<Guid, DeleteUserAccountCommand>
{
    private readonly ILogger<DeleteUserAccountCommand> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAccountRepository _accountRepository;
    private readonly IUsersContract _userContract;
    private readonly IReadDbContext _readDbContext;

    public DeleteUserAccountHandler(
        ILogger<DeleteUserAccountCommand> logger,
        [FromKeyedServices(ModulesName.Accounts)]IUnitOfWork unitOfWork,
        IReadDbContext readDbContext,
        IAccountRepository accountRepository,
        IUsersContract userContract)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _readDbContext = readDbContext;
        _accountRepository = accountRepository;
        _userContract = userContract;
    }
    
    public async Task<Result<Guid, ErrorList>> Handle(
        DeleteUserAccountCommand command,
        CancellationToken cancellationToken = default)
    {
        var transaction = await _unitOfWork.
            BeginTransaction(cancellationToken);

        try
        {
            var individualExist = await _readDbContext.IndividualAccounts.
                AnyAsync(i => i.Id == command.AccountId, cancellationToken);
            
            var corporateExist = await _readDbContext.CorporateAccounts.
                AnyAsync(c => c.Id == command.AccountId, cancellationToken);
            
            if (individualExist)
            {
                var individualAccount = _accountRepository.
                    GetIndividualById(command.AccountId, cancellationToken).Result.Value;
                
                await DeleteIndividualAccount(individualAccount, cancellationToken);
            }
            else if(corporateExist)
            {
                var corporateAccount = _accountRepository.
                    GetCorporateById(command.AccountId, cancellationToken).Result.Value;
                
                await DeleteCorporateAccount(corporateAccount, cancellationToken);
            }
            else
            {
                return Errors.General.NotFound(new ErrorParameters.NotFound
                    (Constants.Accounts.Individual, nameof(command.AccountId),
                        command.AccountId)).ToErrorList();
            }

            var result = await _userContract.
                DeleteUser(command.AccountId);

            if (result.IsFailure)
            {
                return Errors.General.
                    Failed(new ErrorParameters.
                        Failed("Can not delete user with ID {UserId} in transaction")).ToErrorList();
            }
            
            _logger.
                LogInformation("User with ID {UserId} has been deleted.", command.AccountId);
            
            transaction.Commit();
            
            return command.AccountId;
        }
        catch (Exception ex)
        {
             _logger.LogError(ex,
                 "Can not delete user with ID {UserId} in transaction", command.AccountId);
             
             transaction.Rollback();
             
             return Errors.General.
                 Failed(new ErrorParameters.
                     Failed("Can not delete user with ID {UserId} in transaction")).ToErrorList();
        }
    }

    private async Task DeleteIndividualAccount(
        IndividualAccount individualAccount,
        CancellationToken cancellationToken)
    {
        await _accountRepository.Delete(individualAccount);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
    
    private async Task DeleteCorporateAccount(
        CorporateAccount corporateAccount,
        CancellationToken cancellationToken)
    {
        await _accountRepository.Delete(corporateAccount);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}