using Banking.Accounts.Contracts;
using Banking.BankAccounts.Application.Abstractions;
using Banking.ClientAccounts.Domain.Aggregate;
using Banking.ClientAccounts.Domain.ValueObjects.Ids;
using Banking.Core.Abstractions;
using Banking.Core.Extension;
using Banking.SharedKernel;
using Banking.SharedKernel.Definitions;
using Banking.SharedKernel.Models.Errors;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Banking.BankAccounts.Application.Command.ClientAccounts.Create;

public class CreateClientAccountHandler : 
    ICommandHandler<Guid,  CreateClientAccountCommand>
{
    private readonly IValidator<CreateClientAccountCommand> _validator;
    private readonly ILogger<CreateClientAccountHandler> _logger;
    private readonly IAccountsContract _accountsContract;
    private readonly IReadDbContext _readDbContext;
    private readonly IClientAccountRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateClientAccountHandler(
        IValidator<CreateClientAccountCommand> validator,
        ILogger<CreateClientAccountHandler> logger,
        IAccountsContract accountsContract,
        IReadDbContext readDbContext,
        IClientAccountRepository repository,
        [FromKeyedServices(ModulesName.ClientAccounts)]IUnitOfWork unitOfWork)
    {
        _validator = validator;
        _logger = logger;
        _accountsContract = accountsContract;
        _readDbContext = readDbContext;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Guid, ErrorList>> Handle(
        CreateClientAccountCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.
            ValidateAsync(command, cancellationToken);
        
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var clientAccountExist = await _readDbContext.ClientAccounts.
            AnyAsync(c => c.AccountId == command.UserAccountId, cancellationToken);

        if (clientAccountExist)
        {
            return Errors.Extra.AlreadyExists(
                new ErrorParameters.ValueAlreadyExists(
                    $"Client account with {command.UserAccountId} already exists")).ToErrorList();
        }
        
        var corporateAccount = await _accountsContract.
            GetCorporateAccount(command.UserAccountId, cancellationToken);
        
        var individualAccount = await _accountsContract.
            GetIndividualAccount(command.UserAccountId, cancellationToken);

        var clientAccountId = ClientAccountId.NewGuid();

        if (corporateAccount.IsFailure && individualAccount.IsFailure)
        {
            return Errors.General.NotFound(
                new ErrorParameters.NotFound(nameof(Accounts), nameof(command.UserAccountId),
                    command.UserAccountId)).ToErrorList();
        }
        
        var accountType = corporateAccount.IsSuccess ? 
            AccountType.Corporate : AccountType.Individual;
        
        var clientAccount = new ClientAccount(
            clientAccountId, command.UserAccountId, accountType);
        
        await _repository.Add(clientAccount, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.
            LogInformation("Created client account with ID {AccountId}.", clientAccountId);

        return clientAccountId.Id;
    }
}