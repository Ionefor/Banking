using Banking.Accounts.Application.Queries.GetByFullName;
using Banking.Accounts.Contracts.Dto.Commands;
using Banking.Accounts.Contracts.Dto.Queries;

namespace Banking.Accounts.Presentation.Requests.Individual;

public record GetAccountsByFullNameRequest(
    FullNameDto FullName,
    PaginationParamsDto PaginationParams,
    SortingParamsIndividualAccountsDto? SortingParams)
{
    public GetAccountsByFullNameQuery ToQuery() 
        => new(FullName, PaginationParams, SortingParams);
}