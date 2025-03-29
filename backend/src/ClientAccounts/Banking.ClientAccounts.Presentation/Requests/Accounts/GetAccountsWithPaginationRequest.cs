using Banking.Accounts.Contracts.Dto.Queries;
using Banking.BankAccounts.Application.Features.Queries.BankAccounts.GetByClientAccountId;

namespace Banking.ClientAccounts.Presentation.Requests.Accounts;

public record GetAccountsWithPaginationRequest(PaginationParamsDto PaginationParams)
{
    public GetAccountsWithPaginationQuery ToQuery(Guid id) =>
        new(id, PaginationParams);
}