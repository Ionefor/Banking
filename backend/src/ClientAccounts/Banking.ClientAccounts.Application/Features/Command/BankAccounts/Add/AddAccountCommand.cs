using Banking.Core.Abstractions;

namespace Banking.BankAccounts.Application.Features.Command.BankAccounts.Add;

public record AddAccountCommand(
    Guid ClientAccountId,
    string PaymentDetails,
    string Type,
    string Сurrency) : ICommand;