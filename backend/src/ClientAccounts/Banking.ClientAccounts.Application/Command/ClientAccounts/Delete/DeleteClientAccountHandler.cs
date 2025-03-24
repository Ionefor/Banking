using Banking.BankAccounts.Application.Abstractions;
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

namespace Banking.BankAccounts.Application.Command.ClientAccounts.Delete;

public class DeleteClientAccountHandler : 
    ICommandHandler<Guid,  DeleteClientAccountCommand>
{
    private readonly IValidator<DeleteClientAccountCommand> _validator;
    private readonly ILogger<DeleteClientAccountHandler> _logger;
    private readonly IReadDbContext _readDbContext;
    private readonly IClientAccountRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteClientAccountHandler(
        IValidator<DeleteClientAccountCommand> validator,
        ILogger<DeleteClientAccountHandler> logger,
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
        DeleteClientAccountCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.
            ValidateAsync(command, cancellationToken);
        
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var clientAccountExist = await _readDbContext.ClientAccounts.
            AnyAsync(c => c.Id == command.Id, cancellationToken);
        
        if (!clientAccountExist)
        {
            return Errors.General.NotFound(
                new ErrorParameters.NotFound(nameof(ClientAccounts), nameof(command.Id),
                    command.Id)).ToErrorList();
        }
        
        var clientAccountId = ClientAccountId.Create(command.Id);
        
        var clientAccount = await _repository.
            GetClientAccountById(clientAccountId, cancellationToken);

        if (clientAccount.IsFailure)
            return clientAccount.Error.ToErrorList();

        await _repository.Delete(clientAccount.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.
            LogInformation("Deleted client account with ID {AccountId}.", command.Id);

        return command.Id;
    }
}