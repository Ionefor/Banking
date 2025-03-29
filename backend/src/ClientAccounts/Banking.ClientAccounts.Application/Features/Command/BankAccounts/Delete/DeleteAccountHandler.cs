using Banking.BankAccounts.Application.Abstractions;
using Banking.Core.Abstractions;
using Banking.SharedKernel.Definitions;
using Banking.SharedKernel.Models.Errors;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Banking.BankAccounts.Application.Features.Command.BankAccounts.Delete;

public class DeleteAccountHandler : 
    ICommandHandler<Guid, DeleteAccountCommand>
{
    private readonly ILogger<DeleteAccountHandler> _logger;
    private readonly IClientAccountService _service;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAccountHandler(
        ILogger<DeleteAccountHandler> logger,
        IClientAccountService service,
        [FromKeyedServices(ModulesName.ClientAccounts)]IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _service = service;
    }
    
    public async Task<Result<Guid, ErrorList>> Handle(
        DeleteAccountCommand command,
        CancellationToken cancellationToken = default)
    {
        var clientAccountResult = await _service.
            GetClientAccountIfExist(command.ClientAccountId, cancellationToken);
        
        if (clientAccountResult.IsFailure)
            return clientAccountResult.Error.ToErrorList();
        
        var bankAccountResult = await _service.
            GetBankAccountIfExist(command.BankAccountId, cancellationToken);
        
        if (bankAccountResult.IsFailure)
            return bankAccountResult.Error.ToErrorList();
        
        clientAccountResult.Value.DeleteAccount(bankAccountResult.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation(
                "Deleted bankAccount with ID {AccountId} from Client account" +
                    " with Id {ClientId}.", bankAccountResult.Value.Id, 
                        clientAccountResult.Value.Id);
        
        return bankAccountResult.Value.Id.Id;
    }
}