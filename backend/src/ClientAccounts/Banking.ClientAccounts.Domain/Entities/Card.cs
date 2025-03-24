using Banking.ClientAccounts.Domain.ValueObjects;
using Banking.ClientAccounts.Domain.ValueObjects.Ids;
using Banking.SharedKernel.Models.Abstractions;

namespace Banking.ClientAccounts.Domain.Entities;

public class Card : SoftDeletableEntity<CardId>
{
    private Card(CardId id) : base(id) {}

    public Card(
        CardId id,
        PaymentDetails paymentDetails,
        AccountId accountId,
        Ccv ccv,
        DateTime validThru) : base(id)
    {
        PaymentDetails = paymentDetails;
        AccountId = accountId;
        Ccv = ccv;
        ValidThru = validThru;
    }
    
    public PaymentDetails PaymentDetails { get; private set; }
    public AccountId AccountId { get; private set; }
    public Ccv Ccv { get; private set; }
    public bool IsMain { get; private set; }
    public DateTime ValidThru { get; private set; }
    
    internal void SetMainCard()
    {
        IsMain = false;
    }
    
}