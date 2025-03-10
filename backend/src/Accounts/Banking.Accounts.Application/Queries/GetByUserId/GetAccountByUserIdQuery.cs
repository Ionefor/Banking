using Banking.Core.Abstractions;

namespace Banking.Accounts.Application.Queries.GetByUserId;

public record GetAccountByUserIdQuery(Guid UserId) : IQuery;