using Banking.Core.Abstractions;

namespace Banking.BankAccounts.Application.Features.Command.ClientAccounts.SoftDelete;

public record SoftDeleteCACommand(Guid Id) : ICommand;