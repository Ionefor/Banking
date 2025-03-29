using Banking.Core.Abstractions;

namespace Banking.BankAccounts.Application.Features.Command.ClientAccounts.Delete;

public record DeleteClientAccountCommand(Guid Id) : ICommand;