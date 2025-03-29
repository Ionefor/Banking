using Banking.Core.Abstractions;

namespace Banking.BankAccounts.Application.Features.Command.Cards.Add;

public record AddCardCommand(
    Guid ClientAccountId,
    Guid BankAccountId,
    string PaymentDetails,
    string Ccv,
    DateTime ValidThru) : ICommand;