using Banking.Accounts.Application.Abstractions;
using Banking.Accounts.Domain;
using Banking.Core.Abstractions;
using Banking.Core.Extension;
using Banking.SharedKernel.Definitions;
using Banking.SharedKernel.Models.Errors;
using Banking.SharedKernel.ValueObjects;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Banking.Accounts.Application.Commands.Update.Name;

public class UpdateFullNameHandler :
    ICommandHandler<Guid, UpdateFullNameCommand>
{
    private readonly ILogger<UpdateFullNameHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAccountRepository _accountRepository;
    private readonly IReadDbContext _readDbContext;
    private readonly IValidator<UpdateFullNameCommand> _validator;

    public UpdateFullNameHandler(
        IValidator<UpdateFullNameCommand> validator,
        ILogger<UpdateFullNameHandler> logger,
        [FromKeyedServices(ModulesName.Accounts)]IUnitOfWork unitOfWork,
        IReadDbContext readDbContext,
        IAccountRepository accountRepository)
    {
        _validator = validator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _readDbContext = readDbContext;
        _accountRepository = accountRepository;
    }
    
    public async Task<Result<Guid, ErrorList>> Handle(
        UpdateFullNameCommand command,
        CancellationToken cancellationToken = default)
    { 
        var validationResult = await _validator.
            ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            return validationResult.ToErrorList();
        
        var individualExist = await _readDbContext.IndividualAccounts.
            AnyAsync(i => i.UserId == command.UserId, cancellationToken);

        if (!individualExist)
        {
            return Errors.General.NotFound(new ErrorParameters.NotFound
                (nameof(IndividualAccount), nameof(command.UserId), command.UserId)).ToErrorList();
        }

        var individualAccount = await _accountRepository.
            GetIndividualByUserId(command.UserId, cancellationToken);

        if (individualAccount.IsFailure)
            return individualAccount.Error.ToErrorList();
        
        var fullName = FullName.Create(
            command.FullName.FirstName,
            command.FullName.MiddleName,
            command.FullName.LastName).Value;
        
        individualAccount.Value.UpdateName(fullName);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.
            LogInformation("User with ID {UserId} update full name.", command.UserId);

        return command.UserId;
    }
}