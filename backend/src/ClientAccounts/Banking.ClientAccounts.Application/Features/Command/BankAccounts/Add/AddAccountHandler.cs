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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Banking.BankAccounts.Application.Features.Command.BankAccounts.Add;

public class AddAccountHandler :
    ICommandHandler<Guid, AddAccountCommand>
{
    private readonly IValidator<AddAccountCommand> _validator;
    private readonly ILogger<AddAccountHandler> _logger;
    private readonly IClientAccountService _service;
    private readonly IUnitOfWork _unitOfWork;

    public AddAccountHandler(
        IValidator<AddAccountCommand> validator,
        ILogger<AddAccountHandler> logger,
        IClientAccountService service,
        [FromKeyedServices(ModulesName.ClientAccounts)]IUnitOfWork unitOfWork)
    {
        _validator = validator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _service = service;
    }
    
    public async Task<Result<Guid, ErrorList>> Handle(
        AddAccountCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.
            ValidateAsync(command, cancellationToken);
        
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var clientAccountResult = await _service.
            GetClientAccountIfExist(command.ClientAccountId, cancellationToken);

        if (clientAccountResult.IsFailure)
            return clientAccountResult.Error.ToErrorList();

        var bankAccount = CreateBankAccount(command);
        
        clientAccountResult.Value.AddAccount(bankAccount);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation(
            "BankAccount with id: {AccountId} has been added" +
                " to the Client account with id: {ClientAccountId}",
                    bankAccount.Id, command.ClientAccountId);

        return bankAccount.Id.Id;
    }

    private BankAccount CreateBankAccount(AddAccountCommand command)
    {
        var accountId = BankAccountId.NewGuid();
        
        var paymentDetails = PaymentDetails.
            Create(command.PaymentDetails).Value;
        
        var balance = Balance.Create(0).Value;
        
        Enum.TryParse(typeof(WalletType), command.Type, out var type);
        var typeEnum = (WalletType)type!;
        
        Enum.TryParse(typeof(Currencies), command.Сurrency, out var currency);
        var currencyEnum = (Currencies)currency!;

        return new BankAccount(accountId, paymentDetails, typeEnum, currencyEnum, balance);
    }
}
