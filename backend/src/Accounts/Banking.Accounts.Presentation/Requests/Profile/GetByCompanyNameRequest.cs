using Banking.Accounts.Application.Queries.GetByCompanyName;

namespace Banking.Accounts.Presentation.Requests.Profile;

public record GetByCompanyNameRequest(string CompanyName)
{
    public GetByCompanyNameQuery ToQuery() => new(CompanyName);
}