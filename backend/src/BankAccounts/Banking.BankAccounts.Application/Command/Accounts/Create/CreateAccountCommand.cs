using Banking.Core.Abstractions;

namespace Banking.BankAccounts.Application.Command.Accounts.Create;

public record CreateAccountCommand(Guid UserAccountId) : ICommand;