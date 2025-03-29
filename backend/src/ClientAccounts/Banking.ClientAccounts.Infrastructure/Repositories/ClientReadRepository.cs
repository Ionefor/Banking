using Banking.BankAccounts.Application.Abstractions;
using Banking.ClientAccounts.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Banking.ClientAccounts.Infrastructure.Repositories;

public class ClientReadRepository(ReadDbContext context) :
    IClientReadRepository
{
    public async Task<bool> ClientAccountExist(
        Guid clientAccountId,
        CancellationToken cancellationToken = default)
    {
      return await context.ClientAccounts.
          AnyAsync(c => c.Id == clientAccountId, cancellationToken);
    }
    
    public async Task<bool> BankAccountExist(
        Guid bankAccountId,
        CancellationToken cancellationToken = default)
    {
        return await context.Accounts.
            AnyAsync(c => c.Id == bankAccountId, cancellationToken);
    }
    
    public async Task<bool> CardExist(
        Guid cardId,
        CancellationToken cancellationToken = default)
    {
        return await context.Cards.
            AnyAsync(c => c.Id == cardId, cancellationToken);
    }

    public async Task<bool> CardExist(
        string paymentDetails,
        CancellationToken cancellationToken = default)
    {
        return await context.Cards.
            AnyAsync(c => c.PaymentDetails == paymentDetails, cancellationToken);
    }
}