using Banking.BankAccounts.Application.Abstractions;
using Banking.ClientAccounts.Domain.ValueObjects.Ids;
using Banking.Core.Abstractions;
using Banking.Core.Extension;
using Banking.SharedKernel.Definitions;
using Banking.SharedKernel.Models.Errors;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Banking.BankAccounts.Application.Command.Accounts.Delete;

public class DeleteAccountHandler : 
    ICommandHandler<Guid, DeleteAccountCommand>
{
    private readonly ILogger<DeleteAccountHandler> _logger;
    private readonly IReadDbContext _readDbContext;
    private readonly IClientAccountRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAccountHandler(
        ILogger<DeleteAccountHandler> logger,
        IReadDbContext readDbContext,
        IClientAccountRepository repository,
        [FromKeyedServices(ModulesName.ClientAccounts)]IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _readDbContext = readDbContext;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<Guid, ErrorList>> Handle(
        DeleteAccountCommand command,
        CancellationToken cancellationToken = default)
    {
        var clientAccountExist = await _readDbContext.ClientAccounts.
            AnyAsync(c => c.Id == command.ClientAccountId, cancellationToken);

        var a = _readDbContext.ClientAccounts;
        var aa = command.ClientAccountId;
        
        if (!clientAccountExist)
        {
            return Errors.General.NotFound(
                new ErrorParameters.NotFound(nameof(ClientAccounts),
                    nameof(command.ClientAccountId), command.ClientAccountId)).ToErrorList();
        }
        
        var accountExist = await _readDbContext.Accounts.
            AnyAsync(c => c.Id == command.BankAccountId, cancellationToken);

        if (!accountExist)
        {
            return Errors.General.NotFound(new ErrorParameters.NotFound
                    (nameof(Accounts), nameof(command.BankAccountId), command.BankAccountId)).ToErrorList();
        }

        var clientAccountId = ClientAccountId.
            Create(command.ClientAccountId);
        
        var accountId = BankAccountId.
            Create(command.BankAccountId);
        
        var clientAccountResult = await _repository.
            GetClientAccountById(clientAccountId, cancellationToken);

        if (clientAccountResult.IsFailure)
            return clientAccountResult.Error.ToErrorList();
        
        var accountResult = await _repository.
            GetAccountById(accountId, cancellationToken);
        
        if (accountResult.IsFailure)
            return accountResult.Error.ToErrorList();
        
        clientAccountResult.Value.DeleteAccount(accountResult.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation(
                "Deleted account with ID {AccountId} from Client account with Id {ClientId}.",
                accountId, clientAccountId);
        
        return accountResult.Value.Id.Id;
    }
}