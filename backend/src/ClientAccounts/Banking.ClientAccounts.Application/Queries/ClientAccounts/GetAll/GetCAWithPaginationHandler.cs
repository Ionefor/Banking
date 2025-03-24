using Banking.BankAccounts.Application.Abstractions;
using Banking.BankAccounts.Contracts.Dto;
using Banking.Core.Abstractions;
using Banking.Core.Extension;
using Banking.Core.Models;

namespace Banking.BankAccounts.Application.Queries.ClientAccounts.GetAll;

public class GetCAWithPaginationHandler : 
    IQueryHandler<PageList<ClientAccountDto>,
        GetCAWithPaginationQuery>
{
    private readonly IReadDbContext _readDbContext;

    public GetCAWithPaginationHandler(
        IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }
    
    public async Task<PageList<ClientAccountDto>> Handle(
        GetCAWithPaginationQuery query,
        CancellationToken cancellationToken = default)
    {
        var clientAccounts = _readDbContext.ClientAccounts;
        
        clientAccounts.WhereIf(
            query.UserAccountTypeFilter != null,
            q => q.UserAccountType == query.UserAccountTypeFilter);
        
        return await clientAccounts.ToPagedList(
            query.PaginationParams.Page,
            query.PaginationParams.PageSize,
            cancellationToken);
    }
}