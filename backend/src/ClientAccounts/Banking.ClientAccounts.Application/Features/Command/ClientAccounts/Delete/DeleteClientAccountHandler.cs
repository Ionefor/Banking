using Banking.BankAccounts.Application.Abstractions;
using Banking.Core.Abstractions;
using Banking.Core.Extension;
using Banking.SharedKernel.Definitions;
using Banking.SharedKernel.Models.Errors;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Banking.BankAccounts.Application.Features.Command.ClientAccounts.Delete;

public class DeleteClientAccountHandler : 
    ICommandHandler<Guid,  DeleteClientAccountCommand>
{
    private readonly IValidator<DeleteClientAccountCommand> _validator;
    private readonly IClientAccountService _service;
    private readonly ILogger<DeleteClientAccountHandler> _logger;
    private readonly IClientWriteRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteClientAccountHandler(
        IValidator<DeleteClientAccountCommand> validator,
        ILogger<DeleteClientAccountHandler> logger,
        IClientWriteRepository repository,
        IClientAccountService service,
        [FromKeyedServices(ModulesName.ClientAccounts)]IUnitOfWork unitOfWork)
    {
        _validator = validator;
        _logger = logger;
        _repository = repository;
        _unitOfWork = unitOfWork;
        _service = service;
    }
    
    public async Task<Result<Guid, ErrorList>> Handle(
        DeleteClientAccountCommand command,
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
        
        _repository.Delete(clientAccountResult.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.
            LogInformation("Deleted client account with ID {AccountId}.", command.Id);

        return command.Id;
    }
}