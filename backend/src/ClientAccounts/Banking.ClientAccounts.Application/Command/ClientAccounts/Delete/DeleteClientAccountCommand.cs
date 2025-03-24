using Banking.Core.Abstractions;

namespace Banking.BankAccounts.Application.Command.ClientAccounts.Delete;

public record DeleteClientAccountCommand(Guid Id) : ICommand;