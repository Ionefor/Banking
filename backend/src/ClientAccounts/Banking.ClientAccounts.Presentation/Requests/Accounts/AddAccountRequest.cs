using Banking.BankAccounts.Application.Features.Command.BankAccounts.Add;

namespace Banking.ClientAccounts.Presentation.Requests.Accounts;

public record AddAccountRequest(string PaymentDetails, string Type, string Сurrency)
{
    public AddAccountCommand ToCommand(Guid id) =>
        new(id, PaymentDetails, Type, Сurrency);
}