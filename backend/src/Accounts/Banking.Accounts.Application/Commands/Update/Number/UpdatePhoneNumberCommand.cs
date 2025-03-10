using Banking.Core.Abstractions;

namespace Banking.Accounts.Application.Commands.Update.Number;

public record UpdatePhoneNumberCommand(Guid UserId, string PhoneNumber) : ICommand;
