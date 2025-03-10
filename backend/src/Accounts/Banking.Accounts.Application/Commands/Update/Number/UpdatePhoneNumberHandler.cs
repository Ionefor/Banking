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

namespace Banking.Accounts.Application.Commands.Update.Number;

public class UpdatePhoneNumberHandler :
    ICommandHandler<Guid, UpdatePhoneNumberCommand>
{
    private readonly ILogger<UpdatePhoneNumberHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAccountRepository _accountRepository;
    private readonly IReadDbContext _readDbContext;
    private readonly IValidator<UpdatePhoneNumberCommand> _validator;

    public UpdatePhoneNumberHandler(
        IValidator<UpdatePhoneNumberCommand> validator,
        ILogger<UpdatePhoneNumberHandler> logger,
        [FromKeyedServices(ModulesName.Accounts)]IUnitOfWork unitOfWork,
        IAccountRepository accountRepository,
        IReadDbContext readDbContext)
    {
        _validator = validator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _accountRepository = accountRepository;
        _readDbContext = readDbContext;
    }
    
    public async Task<Result<Guid, ErrorList>> Handle(
        UpdatePhoneNumberCommand command,
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

        var phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;
        
        if (individualExist)
        {
            var individualAccount = _accountRepository.
                GetIndividualByUserId(command.UserId, cancellationToken).Result.Value;
            
            individualAccount.UpdatePhoneNumber(phoneNumber);
        }
        else if(corporateExist)
        {
            var corporateAccount = _accountRepository.
                GetCorporateByUserId(command.UserId, cancellationToken).Result.Value;
            
            corporateAccount.UpdatePhoneNumber(phoneNumber);
        }
        else
        {
            return Errors.General.NotFound(new ErrorParameters.NotFound
                (nameof(Constants.Accounts.Individual), nameof(command.UserId),
                    command.UserId)).ToErrorList();
        }
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.
            LogInformation("User with ID {UserId} update phone number.", command.UserId);

        return command.UserId;
    }
}