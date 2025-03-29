using Banking.BankAccounts.Application.Abstractions;
using Banking.BankAccounts.Contracts.Dto;
using Banking.Core.Abstractions;
using Banking.Core.Extension;
using Banking.Core.Models;

namespace Banking.BankAccounts.Application.Features.Queries.Cards.GetByAccountId;

public class GetByAccountIdHandler(IReadDbContext readDbContext) :
    IQueryHandler<PageList<CardDto>, GetByAccountIdQuery>
{
    public async Task<PageList<CardDto>> Handle(
        GetByAccountIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var cards = readDbContext.Cards.
            Where(c => c.AccountId == query.AccountId);
        
        return await cards.ToPagedList(
            query.PaginationParams.Page,
            query.PaginationParams.PageSize,
            cancellationToken);
    }
}