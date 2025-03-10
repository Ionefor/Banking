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

namespace Banking.Accounts.Application.Commands.Update.Tax;

public class UpdateTaxIdHandler : ICommandHandler<Guid, UpdateTaxIdCommand>
{
    private readonly ILogger<UpdateTaxIdHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAccountRepository _accountRepository;
    private readonly IReadDbContext _readDbContext;
    private readonly IValidator<UpdateTaxIdCommand> _validator;

    public UpdateTaxIdHandler(
        IValidator<UpdateTaxIdCommand> validator,
        ILogger<UpdateTaxIdHandler> logger,
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
        UpdateTaxIdCommand command,
        CancellationToken cancellationToken = default)
    { 
        var validationResult = await _validator.
            ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            return validationResult.ToErrorList();
        
        var corporateExist = await _readDbContext.CorporateAccounts.
            AnyAsync(i => i.UserId == command.UserId, cancellationToken);

        var taxId = TaxId.Create(command.TaxId).Value;
        
        if (!corporateExist)
        {
            return Errors.General.NotFound(new ErrorParameters.NotFound
                (nameof(CorporateAccount), nameof(command.UserId), command.UserId)).ToErrorList();
        }
        
        var corporateAccount = _accountRepository.
            GetCorporateByUserId(command.UserId, cancellationToken).Result.Value;
            
        corporateAccount.UpdateTaxId(taxId);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.
            LogInformation("User with ID {UserId} update TaxId.", command.UserId);

        return command.UserId;
    }
}