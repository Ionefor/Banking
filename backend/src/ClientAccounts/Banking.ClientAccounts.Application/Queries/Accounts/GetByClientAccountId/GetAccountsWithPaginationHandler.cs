using Banking.BankAccounts.Application.Abstractions;
using Banking.BankAccounts.Contracts.Dto;
using Banking.Core.Abstractions;
using Banking.Core.Extension;
using Banking.Core.Models;

namespace Banking.BankAccounts.Application.Queries.Accounts.GetByClientAccountId;

public class GetAccountsWithPaginationHandler : 
    IQueryHandler<PageList<AccountDto>,
        GetAccountsWithPaginationQuery>
{
    private readonly IReadDbContext _readDbContext;

    public GetAccountsWithPaginationHandler(
        IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }
    
    public async Task<PageList<AccountDto>> Handle(
        GetAccountsWithPaginationQuery query,
        CancellationToken cancellationToken = default)
    {
        var accounts = _readDbContext.Accounts.
            Where(a => a.ClientAccountId == query.ClientAccountId);
        
        return await accounts.ToPagedList(
            query.PaginationParams.Page,
            query.PaginationParams.PageSize,
            cancellationToken);
    }
}