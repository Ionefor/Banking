using Banking.BankAccounts.Contracts.Dto;

namespace Banking.BankAccounts.Application.Abstractions;

public interface IReadDbContext
{
    IQueryable<ClientAccountDto> ClientAccounts { get; }
    
    IQueryable<AccountDto> Accounts { get; }
    
    IQueryable<CardDto> Cards { get; }
}