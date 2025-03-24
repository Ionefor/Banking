using Banking.Core.Abstractions;

namespace Banking.BankAccounts.Application.Queries.Accounts.GetById;

public record GetAccountByIdQuery(Guid ClientAccountId, Guid AccountId) : IQuery;