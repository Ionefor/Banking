using Banking.ClientAccounts.Domain.ValueObjects;
using Banking.ClientAccounts.Domain.ValueObjects.Ids;
using Banking.SharedKernel;
using Banking.SharedKernel.Models.Abstractions;

namespace Banking.ClientAccounts.Domain.Entities;

public class BankAccount : SoftDeletableEntity<BankAccountId>
{
    private BankAccount(BankAccountId id) : base(id) {}
    
    public BankAccount(
        BankAccountId id,
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
}