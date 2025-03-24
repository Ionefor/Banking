using Banking.Accounts.Contracts.Dto.Queries;
using Banking.BankAccounts.Application.Queries.ClientAccounts.GetAll;
using Banking.SharedKernel;

namespace Banking.ClientAccounts.Presentation.Requests.ClientAccounts;

public record GetCAWithPaginationRequest(
    PaginationParamsDto PaginationParams,
    AccountType? UserAccountTypeFilter)
{
    public GetCAWithPaginationQuery ToQuery()
        => new(PaginationParams, UserAccountTypeFilter);
}