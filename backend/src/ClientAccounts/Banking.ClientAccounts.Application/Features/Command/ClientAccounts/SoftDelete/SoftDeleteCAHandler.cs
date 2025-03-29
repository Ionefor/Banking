using Banking.BankAccounts.Application.Abstractions;
using Banking.Core.Abstractions;
using Banking.Core.Extension;
using Banking.SharedKernel.Definitions;
using Banking.SharedKernel.Models.Errors;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Banking.BankAccounts.Application.Features.Command.ClientAccounts.SoftDelete;

public class SoftDeleteCAHandler  : 
    ICommandHandler<Guid,  SoftDeleteCACommand>
{
    private readonly IValidator<SoftDeleteCACommand> _validator;
    private readonly ILogger<SoftDeleteCAHandler> _logger;
    private readonly IClientAccountService _service;
    private readonly IUnitOfWork _unitOfWork;

    public SoftDeleteCAHandler(
        IValidator<SoftDeleteCACommand> validator,
        ILogger<SoftDeleteCAHandler> logger,
        IClientAccountService service,
        [FromKeyedServices(ModulesName.ClientAccounts)]IUnitOfWork unitOfWork)
    {
        _validator = validator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _service = service;
    }
    public async Task<Result<Guid, ErrorList>> Handle(
        SoftDeleteCACommand command,
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
        
        clientAccountResult.Value.Delete();
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.
            LogInformation("SoftDelete client account with ID {AccountId}.", command.Id);
        
        return command.Id;
    }
}