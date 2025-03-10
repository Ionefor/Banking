using Banking.Core.Abstractions;

namespace Banking.Accounts.Application.Queries.GetByCompanyName;

public record GetByCompanyNameQuery(string CompanyName) : IQuery;
