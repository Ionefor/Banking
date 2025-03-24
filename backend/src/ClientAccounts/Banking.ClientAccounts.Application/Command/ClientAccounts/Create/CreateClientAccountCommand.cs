using Banking.Core.Abstractions;

namespace Banking.BankAccounts.Application.Command.ClientAccounts.Create;

public record CreateClientAccountCommand(Guid UserAccountId) : ICommand;