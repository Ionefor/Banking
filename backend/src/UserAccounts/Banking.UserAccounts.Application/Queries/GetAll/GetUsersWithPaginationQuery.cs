using Banking.Core.Abstractions;
using Banking.UserAccounts.Contracts.Dto;

namespace Banking.UserAccounts.Application.Queries.GetAll;

public record GetUsersWithPaginationQuery(PaginationParamsDto? PaginationParams) : IQuery;
