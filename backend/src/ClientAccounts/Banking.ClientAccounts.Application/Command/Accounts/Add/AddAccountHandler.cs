using Banking.BankAccounts.Application.Abstractions;
using Banking.ClientAccounts.Domain.Entities;
using Banking.ClientAccounts.Domain.ValueObjects;
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

namespace Banking.BankAccounts.Application.Command.Accounts.Add;

public class AddAccountHandler :
    ICommandHandler<Guid, AddAccountCommand>
{
    private readonly IValidator<AddAccountCommand> _validator;
    private readonly ILogger<AddAccountHandler> _logger;
    private readonly IReadDbContext _readDbContext;
    private readonly IClientAccountRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public AddAccountHandler(
        IValidator<AddAccountCommand> validator,
        ILogger<AddAccountHandler> logger,
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
        AddAccountCommand command,
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
                new ErrorParameters.NotFound(nameof(ClientAccounts), nameof(command.ClientAccountId),
                    command.ClientAccountId)).ToErrorList();
        }
        
        var accountId = AccountId.NewGuid();
        
        var paymentDetails = PaymentDetails.
            Create(command.PaymentDetails).Value;
        
        var balance = Balance.Create(0).Value;
        
        Enum.TryParse(typeof(WalletType), command.Type, out var type);
        var typeEnum = (WalletType)type!;
        
        Enum.TryParse(typeof(Currencies), command.Сurrency, out var currency);
        var currencyEnum = (Currencies)currency!;

        var account = new Account(
            accountId, paymentDetails, typeEnum, currencyEnum, balance);
        
        var clientAccountId = ClientAccountId.Create(command.ClientAccountId);
        
        var clientAccount = await _repository.
            GetClientAccountById(clientAccountId, cancellationToken);
        
        if (clientAccount.IsFailure)
            return clientAccount.Error.ToErrorList();
        
        clientAccount.Value.AddAccount(account);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation(
            "Account with id: {AccountId} has been added to the Client account with id: {ClientAccountId}",
            accountId, clientAccountId);

        return account.Id.Id;
    }
}