using Banking.BankAccounts.Application.Command.Cards;
using Banking.BankAccounts.Application.Command.Cards.Add;

namespace Banking.ClientAccounts.Presentation.Requests.Card;

public record AddCardRequest(
    string PaymentDetails,
    string Ccv,
    DateTime ValidThru)
{
    public AddCardCommand ToCommand(
        Guid clientAccountId,
        Guid accountId) => 
        new(clientAccountId, accountId, PaymentDetails, Ccv, ValidThru);
}