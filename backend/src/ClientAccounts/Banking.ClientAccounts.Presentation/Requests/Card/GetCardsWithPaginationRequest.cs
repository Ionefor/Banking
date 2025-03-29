using Banking.Accounts.Contracts.Dto.Queries;
using Banking.BankAccounts.Application.Features.Queries.Cards.GetByAccountId;
using Banking.BankAccounts.Application.Features.Queries.Cards.GetByClientAccountId;

namespace Banking.ClientAccounts.Presentation.Requests.Card;

public record GetCardsWithPaginationRequest(PaginationParamsDto PaginationParams)
{
    public GetByAccountIdQuery ToAccountQuery(Guid id) =>
        new(id, PaginationParams);
    
    public GetByClientAccountIdQuery ToQuery(Guid id) =>
        new(id, PaginationParams);
}
