using Banking.BankAccounts.Application.Abstractions;
using Banking.Core.Abstractions;
using Banking.Core.Extension;
using Banking.SharedKernel.Definitions;
using Banking.SharedKernel.Models.Errors;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Banking.BankAccounts.Application.Features.Command.ClientAccounts.Restore;

public class RestoreClientAccountHandler : 
    ICommandHandler<Guid,  RestoreClientAccountCommand>
{
    private readonly IValidator<RestoreClientAccountCommand> _validator;
    private readonly ILogger<RestoreClientAccountHandler> _logger;
    private readonly IClientAccountService _service;
    private readonly IUnitOfWork _unitOfWork;

    public RestoreClientAccountHandler(
        IValidator<RestoreClientAccountCommand> validator,
        ILogger<RestoreClientAccountHandler> logger,
        IClientAccountService service,
        [FromKeyedServices(ModulesName.ClientAccounts)]IUnitOfWork unitOfWork)
    {
        _validator = validator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _service = service;
    }
    public async Task<Result<Guid, ErrorList>> Handle(
        RestoreClientAccountCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.
            ValidateAsync(command, cancellationToken);
        
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();
        
        var clientAccountResult = await _service.
            GetClientAccountIfExist(command.Id, cancellationToken);

        if (clientAccountResult.IsFailure)
            return clientAccountResult.Error.ToErrorList();
        
        clientAccountResult.Value.Restore();
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.
            LogInformation("Restore client account with ID {AccountId}.", command.Id);
        
        return command.Id;
    }
}