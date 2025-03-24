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

namespace Banking.BankAccounts.Application.Command.ClientAccounts.Restore;

public class RestoreClientAccountHandler : 
    ICommandHandler<Guid,  RestoreClientAccountCommand>
{
    private readonly IValidator<RestoreClientAccountCommand> _validator;
    private readonly ILogger<RestoreClientAccountHandler> _logger;
    private readonly IReadDbContext _readDbContext;
    private readonly IClientAccountRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public RestoreClientAccountHandler(
        IValidator<RestoreClientAccountCommand> validator,
        ILogger<RestoreClientAccountHandler> logger,
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
        RestoreClientAccountCommand command,
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
        
        clientAccount.Value.Restore();
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.
            LogInformation("Restore client account with ID {AccountId}.", command.Id);
        
        return command.Id;
    }
}