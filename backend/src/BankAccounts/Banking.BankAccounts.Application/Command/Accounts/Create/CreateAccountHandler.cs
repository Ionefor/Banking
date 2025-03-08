using Banking.Core.Abstractions;
using Banking.Core.Extension;
using Banking.SharedKernel.Models.Errors;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Banking.BankAccounts.Application.Command.Accounts.Create;

public class CreateAccountHandler : ICommandHandler<Guid, CreateAccountCommand>
{
    private readonly IValidator<CreateAccountCommand> _validator;
    private readonly ILogger<CreateAccountHandler> _logger;

    public CreateAccountHandler(
        // IVolunteerRepository volunteerRepository,
        IValidator<CreateAccountCommand> validator,
        ILogger<CreateAccountHandler> logger)
    {
        //_volunteerRepository = volunteerRepository;
        _validator = validator;
        _logger = logger;
    }
    public async Task<Result<Guid, ErrorList>> Handle(
        CreateAccountCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        // var accountId = AccountId.NewGuid();
        // // проверка что аккаунт с таким command.UserAccountId не существует
        // // получить UserAccount и проверить какого он типа и в зависимости от этого определить AccountType
        // var bankAccount = new BankAccount(accountId, command.UserAccountId, AccountType.Corporate);
        throw new NotImplementedException();
    }
}