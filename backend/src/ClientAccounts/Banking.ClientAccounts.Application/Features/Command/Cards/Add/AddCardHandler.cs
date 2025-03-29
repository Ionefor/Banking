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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Banking.BankAccounts.Application.Features.Command.Cards.Add;

public class AddCardHandler :
    ICommandHandler<Guid, AddCardCommand>
{
    private readonly IValidator<AddCardCommand> _validator;
    private readonly ILogger<AddCardHandler> _logger;
    private readonly IClientAccountService _service;
    private readonly IClientReadRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public AddCardHandler(
        IValidator<AddCardCommand> validator,
        ILogger<AddCardHandler> logger,
        IClientAccountService service,
        IClientReadRepository repository,
        [FromKeyedServices(ModulesName.ClientAccounts)]IUnitOfWork unitOfWork)
    {
        _validator = validator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _repository = repository;
        _service = service;
    }
    
    public async Task<Result<Guid, ErrorList>> Handle(
        AddCardCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.
            ValidateAsync(command, cancellationToken);
        
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var clientAccountResult = await _service.
            GetClientAccountIfExist(command.ClientAccountId, cancellationToken);
        
        if (clientAccountResult.IsFailure)
            clientAccountResult.Error.ToErrorList();

        var ensureAddCard = await EnsureAddCard(command, cancellationToken);
        
        if (ensureAddCard.IsFailure)
            return ensureAddCard.Error.ToErrorList();
        
        var card = CreateCard(command);
        
        clientAccountResult.Value.AddCard(card);

        if(clientAccountResult.Value.Cards.Count == 1)
            clientAccountResult.Value.SetMainCard(card);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation("Card added successfully");

        return card.Id.Id;
    }

    private async Task<UnitResult<Error>> EnsureAddCard(
        AddCardCommand command, CancellationToken cancellationToken = default)
    {
        var bankAccountExist = await _repository.
            BankAccountExist(command.BankAccountId, cancellationToken);
        
        if (!bankAccountExist)
        {
            return Errors.General.
                NotFound(new ErrorParameters.NotFound(nameof(Banking.BankAccounts),
                    nameof(command.BankAccountId), command.BankAccountId));
        }
        
        var cardExist = await _repository.
            CardExist(command.PaymentDetails, cancellationToken);
        
        if (cardExist)
        {
            return Errors.Extra.AlreadyExists(
                new ErrorParameters.ValueAlreadyExists(command.PaymentDetails));
        }

        return UnitResult.Success<Error>();
    }
    
    private Card CreateCard(AddCardCommand command)
    {
        var cardId = CardId.NewGuid();
        
        var accountId = BankAccountId.
            Create(command.BankAccountId);
        
        var paymentDetails = PaymentDetails.
            Create(command.PaymentDetails).Value;
        
        var ccv = Ccv.Create(command.Ccv).Value;

        return new Card(cardId, paymentDetails, accountId, ccv, command.ValidThru);
    }
}
