using Banking.Accounts.Contracts.Dto.Commands;
using Banking.Accounts.Contracts.Dto.Queries;
using Banking.Core.Abstractions;

namespace Banking.Accounts.Application.Queries.GetByFullName;

public record GetAccountsByFullNameQuery(
    FullNameDto FullName,
    PaginationParamsDto PaginationParams,
    SortingParamsIndividualAccountsDto? SortingParams) : IQuery;
