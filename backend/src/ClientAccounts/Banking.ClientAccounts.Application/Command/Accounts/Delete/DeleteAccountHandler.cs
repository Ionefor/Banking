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
    private readonly IValidator<DeleteAccountCommand> _validator;
    private readonly ILogger<DeleteAccountHandler> _logger;
    private readonly IReadDbContext _readDbContext;
    private readonly IClientAccountRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAccountHandler(
        IValidator<DeleteAccountCommand> validator,
        ILogger<DeleteAccountHandler> logger,
        IReadDbContext readDbContext,
        IClientAccountRepository repository,
        [FromKeyedServices(ModulesName.ClientAccounts)]IUnitOfWork unitOfWork)
    {
        _validator = validator;
        _logger = logger;
        _readDbContext = readDbContext;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<Guid, ErrorList>> Handle(
        DeleteAccountCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.
            ValidateAsync(command, cancellationToken);
        
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var clientAccountExist = await _readDbContext.ClientAccounts.
            AnyAsync(c => c.Id == command.ClientAccountId, cancellationToken);

        if (!clientAccountExist)
        {
            return Errors.General.NotFound(
                new ErrorParameters.NotFound(nameof(ClientAccounts),
                    nameof(command.ClientAccountId), command.ClientAccountId)).ToErrorList();
        }
        
        var accountExist = await _readDbContext.Accounts.
            AnyAsync(c => c.Id == command.AccountId, cancellationToken);

        if (!accountExist)
        {
            return Errors.General.NotFound(new ErrorParameters.NotFound
                    (nameof(Accounts), nameof(command.AccountId), command.AccountId)).ToErrorList();
        }

        var clientAccountId = ClientAccountId.
            Create(command.ClientAccountId);
        
        var accountId = AccountId.
            Create(command.AccountId);
        
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