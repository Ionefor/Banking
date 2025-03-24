using Banking.Core.Abstractions;

namespace Banking.BankAccounts.Application.Queries.Cards.GetById;

public record GetCardByIdQuery(Guid CardId) : IQuery;
