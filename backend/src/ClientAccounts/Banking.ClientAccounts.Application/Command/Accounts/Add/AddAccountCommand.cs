using Banking.Core.Abstractions;

namespace Banking.BankAccounts.Application.Command.Accounts.Add;

public record AddAccountCommand(
    Guid ClientAccountId,
    string PaymentDetails,
    string Type,
    string Сurrency) : ICommand;