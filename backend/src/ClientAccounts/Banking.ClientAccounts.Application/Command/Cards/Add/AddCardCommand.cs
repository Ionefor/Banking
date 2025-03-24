using Banking.Core.Abstractions;

namespace Banking.BankAccounts.Application.Command.Cards.Add;

public record AddCardCommand(
    Guid ClientAccountId,
    Guid AccountId,
    string PaymentDetails,
    string Ccv,
    DateTime ValidThru) : ICommand;