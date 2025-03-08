using Banking.Core.Abstractions;

namespace Banking.UserAccounts.Application.Commands.Account.Update.Number;

public record UpdatePhoneNumberCommand(Guid UserId, string PhoneNumber) : ICommand;
