using Banking.Accounts.Contracts.Dto.Queries;
using Banking.Core.Abstractions;

namespace Banking.BankAccounts.Application.Features.Queries.BankAccounts.GetByClientAccountId;

public record GetAccountsWithPaginationQuery(
    Guid ClientAccountId,
    PaginationParamsDto PaginationParams) : IQuery;