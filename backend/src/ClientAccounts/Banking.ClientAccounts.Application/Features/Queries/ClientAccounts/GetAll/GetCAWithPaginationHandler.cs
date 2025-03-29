using Banking.BankAccounts.Application.Abstractions;
using Banking.BankAccounts.Contracts.Dto;
using Banking.Core.Abstractions;
using Banking.Core.Extension;
using Banking.Core.Models;

namespace Banking.BankAccounts.Application.Features.Queries.ClientAccounts.GetAll;

public class GetCaWithPaginationHandler(IReadDbContext readDbContext) :
    IQueryHandler<PageList<ClientAccountDto>,
        GetCAWithPaginationQuery>
{
    public async Task<PageList<ClientAccountDto>> Handle(
        GetCAWithPaginationQuery query,
        CancellationToken cancellationToken = default)
    {
        var clientAccounts = readDbContext.ClientAccounts;
        
        clientAccounts.WhereIf(
            query.UserAccountTypeFilter != null,
            q => q.UserAccountType == query.UserAccountTypeFilter);
        
        return await clientAccounts.ToPagedList(
            query.PaginationParams.Page,
            query.PaginationParams.PageSize,
            cancellationToken);
    }
}