using Banking.BankAccounts.Application.Abstractions;
using Banking.BankAccounts.Contracts.Dto;
using Banking.Core.Abstractions;
using Banking.Core.Extension;
using Banking.Core.Models;

namespace Banking.BankAccounts.Application.Queries.Cards.GetByAccountId;

public class GetByAccountIdHandler :
    IQueryHandler<PageList<CardDto>, GetByAccountIdQuery>
{
    private readonly IReadDbContext _readDbContext;

    public GetByAccountIdHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }
    public async Task<PageList<CardDto>> Handle(
        GetByAccountIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var cards = _readDbContext.Cards.
            Where(c => c.AccountId == query.AccountId);
        
        return await cards.ToPagedList(
            query.PaginationParams.Page,
            query.PaginationParams.PageSize,
            cancellationToken);
    }
}