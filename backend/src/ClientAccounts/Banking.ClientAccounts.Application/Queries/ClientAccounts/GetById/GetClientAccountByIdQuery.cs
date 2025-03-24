using Banking.Core.Abstractions;

namespace Banking.BankAccounts.Application.Queries.ClientAccounts.GetById;

public record GetClientAccountByIdQuery(Guid Id) : IQuery;
