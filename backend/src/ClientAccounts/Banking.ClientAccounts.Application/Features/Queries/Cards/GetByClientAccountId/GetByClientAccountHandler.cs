using Banking.BankAccounts.Application.Abstractions;
using Banking.BankAccounts.Contracts.Dto;
using Banking.Core.Abstractions;
using Banking.Core.Extension;
using Banking.Core.Models;

namespace Banking.BankAccounts.Application.Features.Queries.Cards.GetByClientAccountId;

public class GetByClientAccountHandler(IReadDbContext readDbContext) :
    IQueryHandler<PageList<CardDto>, GetByClientAccountIdQuery>
{
    public async Task<PageList<CardDto>> Handle(
        GetByClientAccountIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var cards = readDbContext.Cards.
            Where(c => c.ClientAccountId == query.ClientAccountId);
        
        return await cards.ToPagedList(
            query.PaginationParams.Page,
            query.PaginationParams.PageSize,
            cancellationToken);
    }
}