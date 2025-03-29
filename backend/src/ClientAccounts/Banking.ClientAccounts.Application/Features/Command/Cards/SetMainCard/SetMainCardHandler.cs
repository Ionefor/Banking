using Banking.BankAccounts.Application.Abstractions;
using Banking.Core.Abstractions;
using Banking.SharedKernel.Definitions;
using Banking.SharedKernel.Models.Errors;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Banking.BankAccounts.Application.Features.Command.Cards.SetMainCard;

public class SetMainCardHandler : 
    ICommandHandler<Guid, SetMainCardCommand>
{
    private readonly ILogger<SetMainCardHandler> _logger;
    private readonly IClientAccountService _service;
    private readonly IUnitOfWork _unitOfWork;

    public SetMainCardHandler(
        ILogger<SetMainCardHandler> logger,
        IClientAccountService service,
        [FromKeyedServices(ModulesName.ClientAccounts)]IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _service = service;
    }
    public async Task<Result<Guid, ErrorList>> Handle(
        SetMainCardCommand command,
        CancellationToken cancellationToken = default)
    {
        var clientAccountResult = await _service.
            GetClientAccountIfExist(command.ClientAccountId, cancellationToken);
        
        if (clientAccountResult.IsFailure)
            return clientAccountResult.Error.ToErrorList();
        
        var cardResult = await _service.
            GetCardIfExist(command.CardId, cancellationToken);
        
        if (cardResult.IsFailure)
            return cardResult.Error.ToErrorList();

        clientAccountResult.Value.SetMainCard(cardResult.Value);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation(
            "Set main card with ID {cardId} from Client account with Id {ClientId}.",
            cardResult.Value.Id, clientAccountResult.Value.Id);
        
        return cardResult.Value.Id.Id;
    }
}