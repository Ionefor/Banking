using Banking.Accounts.Contracts.Dto.Models;

namespace Banking.Accounts.Application.Abstractions;

public interface IReadDbContext
{
    IQueryable<IndividualAccountDto> IndividualAccounts { get; }
    
    IQueryable<CorporateAccountDto> CorporateAccounts { get; }
}