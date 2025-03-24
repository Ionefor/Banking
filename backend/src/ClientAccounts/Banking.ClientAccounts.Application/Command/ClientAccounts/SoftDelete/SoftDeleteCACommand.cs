using Banking.Core.Abstractions;

namespace Banking.BankAccounts.Application.Command.ClientAccounts.SoftDelete;

public record SoftDeleteCACommand(Guid Id) : ICommand;