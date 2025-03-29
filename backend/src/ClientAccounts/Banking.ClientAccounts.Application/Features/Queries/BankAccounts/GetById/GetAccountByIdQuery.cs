using Banking.Core.Abstractions;

namespace Banking.BankAccounts.Application.Features.Queries.BankAccounts.GetById;

public record GetAccountByIdQuery(Guid ClientAccountId, Guid AccountId) : IQuery;