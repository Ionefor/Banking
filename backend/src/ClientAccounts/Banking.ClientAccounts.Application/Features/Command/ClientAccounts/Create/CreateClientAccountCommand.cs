using Banking.Core.Abstractions;

namespace Banking.BankAccounts.Application.Features.Command.ClientAccounts.Create;

public record CreateClientAccountCommand(Guid UserAccountId) : ICommand;