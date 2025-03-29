using Banking.BankAccounts.Application.Features.Command.Cards.Add;

namespace Banking.ClientAccounts.Presentation.Requests.Card;

public record AddCardRequest(
    Guid BankAccountId,
    string PaymentDetails,
    string Ccv,
    DateTime ValidThru)
{
    public AddCardCommand ToCommand(Guid clientAccountId) => 
        new(clientAccountId, BankAccountId, PaymentDetails, Ccv, ValidThru);
}