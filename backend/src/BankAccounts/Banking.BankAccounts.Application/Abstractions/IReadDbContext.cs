using Banking.BankAccounts.Contracts.Dto;

namespace Banking.BankAccounts.Application.Abstractions;

public interface IReadDbContext
{
    IQueryable<BankAccountDto> Accounts { get; }
    
    IQueryable<WalletDto> Wallets { get; }
    
    IQueryable<CardDto> Cards { get; }
}