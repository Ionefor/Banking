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

namespace Banking.BankAccounts.Application.Features.Command.ClientAccounts.Create;

public class CreateClientAccountHandler : 
    ICommandHandler<Guid,  CreateClientAccountCommand>
{
    private readonly IValidator<CreateClientAccountCommand> _validator;
    private readonly ILogger<CreateClientAccountHandler> _logger;
    private readonly IAccountsContract _accountsContract;
    private readonly IReadDbContext _readDbContext;
    private readonly IClientWriteRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateClientAccountHandler(
        IValidator<CreateClientAccountCommand> validator,
        ILogger<CreateClientAccountHandler> logger,
        IAccountsContract accountsContract,
        IReadDbContext readDbContext,
        IClientWriteRepository repository,
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

        var ensureCreateAccount = await EnsureCreateClientAccount(command, cancellationToken);
        
        if (ensureCreateAccount.IsFailure)
            return ensureCreateAccount.Error.ToErrorList();
        
        var clientAccountId = ClientAccountId.NewGuid();
        
        var clientAccount = new ClientAccount(
            clientAccountId, command.UserAccountId, ensureCreateAccount.Value);
        
        await _repository.Add(clientAccount, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.
            LogInformation("Created client account with ID {AccountId}.", clientAccountId);

        return clientAccountId.Id;
    }

    private async Task<Result<AccountType, Error>> EnsureCreateClientAccount(
        CreateClientAccountCommand command, CancellationToken cancellationToken = default)
    {
        var clientAccountExist = await _readDbContext.ClientAccounts.
            AnyAsync(c => c.UserAccountId == command.UserAccountId, cancellationToken);

        if (clientAccountExist)
        {
            return Errors.Extra.AlreadyExists(
                new ErrorParameters.ValueAlreadyExists(
                    $"Client account with {command.UserAccountId} already exists"));
        }
        
        var corporateAccountResult = await _accountsContract.
            GetCorporateAccount(command.UserAccountId, cancellationToken);

        if (corporateAccountResult.IsSuccess)
            return AccountType.Corporate;
        
        var individualAccountResult = await _accountsContract.
            GetIndividualAccount(command.UserAccountId, cancellationToken);
        
        if (individualAccountResult.IsSuccess)
            return AccountType.Individual;
        
        return Errors.General.
            NotFound(new ErrorParameters.NotFound(nameof(Accounts),
                nameof(command.UserAccountId), command.UserAccountId));
    }
}