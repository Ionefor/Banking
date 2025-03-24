using Banking.Accounts.Contracts.Dto.Queries;
using Banking.Core.Abstractions;
using Banking.SharedKernel;

namespace Banking.BankAccounts.Application.Queries.ClientAccounts.GetAll;

public record GetCAWithPaginationQuery(
    PaginationParamsDto PaginationParams,
    AccountType? UserAccountTypeFilter) : IQuery;