using Banking.Core.Abstractions;

namespace Banking.BankAccounts.Application.Command.Accounts.Delete;

public record DeleteAccountCommand(
    Guid ClientAccountId, Guid AccountId) : ICommand;
