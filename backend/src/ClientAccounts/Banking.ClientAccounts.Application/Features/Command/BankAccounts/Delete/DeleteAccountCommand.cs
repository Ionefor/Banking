using Banking.Core.Abstractions;

namespace Banking.BankAccounts.Application.Features.Command.BankAccounts.Delete;

public record DeleteAccountCommand(
    Guid ClientAccountId, Guid BankAccountId) : ICommand;
