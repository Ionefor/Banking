using Banking.Core.Abstractions;

namespace Banking.BankAccounts.Application.Features.Queries.Cards.GetById;

public record GetCardByIdQuery(Guid CardId) : IQuery;
