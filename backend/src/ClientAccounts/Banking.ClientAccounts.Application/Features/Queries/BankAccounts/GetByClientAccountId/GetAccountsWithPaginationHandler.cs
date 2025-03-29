using Banking.BankAccounts.Application.Abstractions;
using Banking.BankAccounts.Contracts.Dto;
using Banking.Core.Abstractions;
using Banking.Core.Extension;
using Banking.Core.Models;

namespace Banking.BankAccounts.Application.Features.Queries.BankAccounts.GetByClientAccountId;

public class GetAccountsWithPaginationHandler(IReadDbContext readDbContext) :
    IQueryHandler<PageList<BankAccountDto>, GetAccountsWithPaginationQuery>
{
    public async Task<PageList<BankAccountDto>> Handle(
        GetAccountsWithPaginationQuery query,
        CancellationToken cancellationToken = default)
    {
        var accounts = readDbContext.Accounts.
            Where(a => a.ClientAccountId == query.ClientAccountId);
        
        return await accounts.ToPagedList(
            query.PaginationParams.Page,
            query.PaginationParams.PageSize,
            cancellationToken);
    }
}