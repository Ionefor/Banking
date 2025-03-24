using Banking.Core.Abstractions;

namespace Banking.BankAccounts.Application.Command.ClientAccounts.Restore;

public record RestoreClientAccountCommand(Guid Id) : ICommand;