using Banking.Accounts.Application.Queries.GetAll;
using Banking.Accounts.Contracts.Dto.Queries;

namespace Banking.Accounts.Presentation.Requests.General;

public record GetAccountsWithPaginationRequest(
    PaginationParamsDto PaginationParams,
    SortingParamsAllAccountsDto? SortingParams,
    FilteringParamsAllAccountsDto? FilteringParams)
{
    public GetAccountsWithPaginationQuery ToQuery()
        => new(PaginationParams, SortingParams, FilteringParams);
}