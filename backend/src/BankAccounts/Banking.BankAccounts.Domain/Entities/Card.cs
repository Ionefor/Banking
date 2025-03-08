using Banking.BankAccounts.Domain.ValueObjects;
using Banking.BankAccounts.Domain.ValueObjects.Ids;
using Banking.SharedKernel.Models.Abstractions;

namespace Banking.BankAccounts.Domain.Entities;

public class Card : SoftDeletableEntity<CardId>
{
    private Card(CardId id) : base(id) {}

    public Card(
        CardId id,
        PaymentDetails paymentDetails,
        WalletId walletId,
        Ccv ccv,
        DateTime validThru) : base(id)
    {
        PaymentDetails = paymentDetails;
        WalletId = walletId;
        Ccv = ccv;
        ValidThru = validThru;
    }
    
    public PaymentDetails PaymentDetails { get; private set; }
    public WalletId WalletId { get; private set; }
    public Ccv Ccv { get; private set; }
    public DateTime ValidThru { get; private set; }
    
    internal new void Delete()
    {
        base.Delete();
    }
    
    internal new void Restore()
    {
        base.Restore();
    }
}