using Banking.Accounts.Application.Abstractions;
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

namespace Banking.Accounts.Application.Commands.Update.Mail;

public class UpdateEmailHandler :
    ICommandHandler<Guid,  UpdateEmailCommand>
{
    private readonly ILogger<UpdateEmailHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAccountRepository _accountRepository;
    private readonly IReadDbContext _readDbContext;
    private readonly IValidator<UpdateEmailCommand> _validator;

    public UpdateEmailHandler(
        IValidator<UpdateEmailCommand> validator,
        ILogger<UpdateEmailHandler> logger,
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
        UpdateEmailCommand command,
        CancellationToken cancellationToken = default)
    { 
        var validationResult = await _validator.
            ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            return validationResult.ToErrorList();
        
        var individualExist = await _readDbContext.IndividualAccounts.
            AnyAsync(i => i.UserId == command.UserId, cancellationToken);
        
        var corporateExist = await _readDbContext.CorporateAccounts.
            AnyAsync(i => i.UserId == command.UserId, cancellationToken);

        var email = Email.Create(command.Email).Value;
        
        if (individualExist)
        {
            var individualAccount = _accountRepository.
                GetIndividualByUserId(command.UserId, cancellationToken).Result.Value;
            
            individualAccount.UpdateEmail(email);
        }
        else if(corporateExist)
        {
            var corporateAccount = _accountRepository.
                GetCorporateByUserId(command.UserId, cancellationToken).Result.Value;
            
            corporateAccount.UpdateEmail(email);
        }
        else
        {
            return Errors.General.NotFound(new ErrorParameters.NotFound
                (nameof(Constants.Accounts.Individual), nameof(command.UserId),
                    command.UserId)).ToErrorList();
        }
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.
            LogInformation("User with ID {UserId} update email.", command.UserId);

        return command.UserId;
    }
}