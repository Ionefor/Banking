using Banking.Accounts.Application.Commands.Update.Tax;

namespace Banking.Accounts.Presentation.Requests.Corporate;

public record UpdateTaxIdRequest(string TaxId)
{
    public UpdateTaxIdCommand ToCommand(Guid accountId) => new(accountId, TaxId);
}