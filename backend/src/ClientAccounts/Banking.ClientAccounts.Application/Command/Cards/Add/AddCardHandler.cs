using Banking.BankAccounts.Application.Abstractions;
using Banking.ClientAccounts.Domain.Entities;
using Banking.ClientAccounts.Domain.ValueObjects;
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

namespace Banking.BankAccounts.Application.Command.Cards.Add;

public class AddCardHandler :
    ICommandHandler<Guid, AddCardCommand>
{
    private readonly IValidator<AddCardCommand> _validator;
    private readonly ILogger<AddCardHandler> _logger;
    private readonly IReadDbContext _readDbContext;
    private readonly IClientAccountRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public AddCardHandler(
        IValidator<AddCardCommand> validator,
        ILogger<AddCardHandler> logger,
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
        AddCardCommand command,
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
        
        var accountExist = await _readDbContext.Accounts.AnyAsync(
            c => c.Id == command.AccountId && c.ClientAccountId == command.ClientAccountId,
            cancellationToken);
        
        if (!accountExist)
        {
            return Errors.General.NotFound(
                new ErrorParameters.NotFound(nameof(Accounts), nameof(command.AccountId),
                    command.AccountId)).ToErrorList();
        }
        
        var cardExist = await _readDbContext.Cards.AnyAsync(
            c => c.PaymentDetails == command.PaymentDetails, cancellationToken);
        
        if (cardExist)
        {
            return Errors.Extra.AlreadyExists(
                new ErrorParameters.ValueAlreadyExists(command.PaymentDetails)).ToErrorList();
        }

        var cardId = CardId.NewGuid();
        var accountId = AccountId.Create(command.AccountId);
        var clientAccountId = ClientAccountId.Create(command.ClientAccountId);
        
        var paymentDetails = PaymentDetails.Create(command.PaymentDetails).Value;
        var ccv = Ccv.Create(command.Ccv).Value;

        var card = new Card(cardId, paymentDetails, accountId, ccv, command.ValidThru);
        
        var clientAccountResult = await _repository.
            GetClientAccountById(clientAccountId, cancellationToken);

        if (clientAccountResult.IsFailure)
            clientAccountResult.Error.ToErrorList();
        
        clientAccountResult.Value.AddCard(card);

        if(clientAccountResult.Value.Cards.Count == 1)
            clientAccountResult.Value.SetMainCard(card);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation("Card added successfully");

        return card.Id.Id;
    }
}
