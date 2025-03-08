using Banking.UserAccounts.Application.Commands.Account.Update.Tax;

namespace Banking.UserAccounts.Presentation.Requests.Account;

public record UpdateTaxIdRequest(string TaxId)
{
    public UpdateTaxIdCommand ToCommand(Guid userId) => new(userId, TaxId);
}