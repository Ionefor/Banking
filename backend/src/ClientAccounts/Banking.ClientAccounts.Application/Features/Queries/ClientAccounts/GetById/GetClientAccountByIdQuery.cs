using Banking.Core.Abstractions;

namespace Banking.BankAccounts.Application.Features.Queries.ClientAccounts.GetById;

public record GetClientAccountByIdQuery(Guid ClientAccountId) : IQuery;
