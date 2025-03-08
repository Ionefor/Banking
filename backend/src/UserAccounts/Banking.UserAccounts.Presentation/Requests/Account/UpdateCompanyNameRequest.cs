using Banking.UserAccounts.Application.Commands.Account.Update.CompanName;

namespace Banking.UserAccounts.Presentation.Requests.Account;

public record UpdateCompanyNameRequest(string CompanyName)
{
    public UpdateCompanyNameCommand ToCommand(Guid userId) => new(userId, CompanyName);
}