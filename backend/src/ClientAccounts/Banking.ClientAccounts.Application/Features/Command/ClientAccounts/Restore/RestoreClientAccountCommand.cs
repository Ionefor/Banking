using Banking.Core.Abstractions;

namespace Banking.BankAccounts.Application.Features.Command.ClientAccounts.Restore;

public record RestoreClientAccountCommand(Guid Id) : ICommand;