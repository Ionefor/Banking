using Banking.UserAccounts.Application.Queries.GetAll;
using Banking.UserAccounts.Contracts.Dto;

namespace Banking.UserAccounts.Presentation.Requests.Profile;

public record GetUsersWithPaginationRequest(PaginationParamsDto? PaginationParams)
{
    public GetUsersWithPaginationQuery ToCommand()
        => new(PaginationParams);
}