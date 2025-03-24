using Banking.BankAccounts.Application.Abstractions;
using Banking.ClientAccounts.Domain.ValueObjects.Ids;
using Banking.Core.Abstractions;
using Banking.SharedKernel.Definitions;
using Banking.SharedKernel.Models.Errors;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Banking.BankAccounts.Application.Command.Cards.SetMainCard;

public class SetMainCardHandler : 
    ICommandHandler<Guid, SetMainCardCommand>
{
    private readonly ILogger<SetMainCardHandler> _logger;
    private readonly IReadDbContext _readDbContext;
    private readonly IClientAccountRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public SetMainCardHandler(
        ILogger<SetMainCardHandler> logger,
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
        SetMainCardCommand command,
        CancellationToken cancellationToken = default)
    {
        var clientAccountExist = await _readDbContext.ClientAccounts.
            AnyAsync(c => c.Id == command.ClientAccountId, cancellationToken);

        if (!clientAccountExist)
        {
            return Errors.General.NotFound(
                new ErrorParameters.NotFound(nameof(ClientAccounts),
                    nameof(command.ClientAccountId), command.ClientAccountId)).ToErrorList();
        }
        
        var card = await _readDbContext.Cards.FirstOrDefaultAsync(
            c => c.Id == command.CardId && c.ClientAccountId == command.ClientAccountId,
            cancellationToken);

        if (card is null)
        {
            return Errors.General.NotFound(new ErrorParameters.NotFound(
                nameof(Cards), nameof(command.CardId),
                command.CardId)).ToErrorList();
        }
        
        var clientAccountId = ClientAccountId.Create(command.ClientAccountId);
        var cardId = CardId.Create(command.CardId);
        
        var clientAccountResult = await _repository.
            GetClientAccountById(clientAccountId, cancellationToken);
        
        if (clientAccountResult.IsFailure)
            return clientAccountResult.Error.ToErrorList();
        
        var cardResult = await _repository.
            GetCardById(cardId, cancellationToken);
        
        if (cardResult.IsFailure)
            return cardResult.Error.ToErrorList();

        clientAccountResult.Value.
            SetMainCard(cardResult.Value);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation(
            "Set main card with ID {cardId} from Client account with Id {ClientId}.",
            cardId, clientAccountId);
        
        return cardResult.Value.Id.Id;
    }
}