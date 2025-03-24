using Banking.BankAccounts.Application.Abstractions;
using Banking.BankAccounts.Contracts.Dto;
using Banking.Core.Abstractions;
using Banking.Core.Extension;
using Banking.Core.Models;

namespace Banking.BankAccounts.Application.Queries.Cards.GetByClientAccountId;

public class GetByClientAccountHandler :
    IQueryHandler<PageList<CardDto>, GetByClientAccountIdQuery>
{
    private readonly IReadDbContext _readDbContext;

    public GetByClientAccountHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }
    
    public async Task<PageList<CardDto>> Handle(
        GetByClientAccountIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var cards = _readDbContext.Cards.
            Where(c => c.AccountId == query.ClientAccountId);
        
        return await cards.ToPagedList(
            query.PaginationParams.Page,
            query.PaginationParams.PageSize,
            cancellationToken);
    }
}