using Banking.Accounts.Contracts.Dto.Queries;
using Banking.Core.Abstractions;

namespace Banking.Accounts.Application.Queries.GetAll;

public record GetAccountsWithPaginationQuery(
    PaginationParamsDto PaginationParams,
    SortingParamsAllAccountsDto? SortingParams,
    FilteringParamsAllAccountsDto? FilteringParams) : IQuery;
