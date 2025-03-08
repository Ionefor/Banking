using Banking.BankAccounts.Domain.ValueObjects;
using Banking.BankAccounts.Domain.ValueObjects.Ids;
using Banking.SharedKernel;
using Banking.SharedKernel.Models.Abstractions;

namespace Banking.BankAccounts.Domain.Entities;

public class Wallet : SoftDeletableEntity<WalletId>
{
    private Wallet(WalletId id) : base(id)
    {
    }

    public Wallet(
        WalletId id,
        PaymentDetails paymentDetails,
        WalletType type,
        Currencies currency,
        Balance balance) : base(id)
    {
        PaymentDetails = paymentDetails;
        Type = type;
        Сurrency = currency;
        Balance = balance;
    }

    public PaymentDetails PaymentDetails { get; private set; }

    public WalletType Type { get; private set; }
    public Currencies Сurrency { get; private set; }

    public Balance Balance { get; private set; }

    internal new void Delete()
    {
        base.Delete();
    }

    internal new void Restore()
    {
        base.Restore();
    }

    internal void UpdateBalance(Balance balance)
    {
        Balance = balance;
    }
}