using Banking.SharedKernel.Models.Abstractions;

namespace Banking.BankAccounts.Domain.ValueObjects.Ids;

public class WalletId(Guid id) : BaseId<WalletId>(id)
{
    public static implicit operator Guid(WalletId walletId) => walletId.Id;
    public static implicit operator WalletId(Guid id) => new(id);
}