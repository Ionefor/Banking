using Banking.Core.Abstractions;

namespace Banking.Accounts.Application.Commands.Update.Number;

public record UpdatePhoneNumberCommand(Guid AccountId, string PhoneNumber) : ICommand;
