using Banking.BankAccounts.Domain.Entities;
using Banking.BankAccounts.Domain.ValueObjects;
using Banking.BankAccounts.Domain.ValueObjects.Ids;
using Banking.SharedKernel;
using Banking.SharedKernel.Models.Abstractions;
using Banking.SharedKernel.Models.Errors;
using CSharpFunctionalExtensions;

namespace Banking.BankAccounts.Domain.Aggregate;

public class BankAccount : SoftDeletableEntity<AccountId>
{
    private List<Wallet> _wallets = [];
    private List<Card> _cards = [];

    public BankAccount(
        AccountId id,
        Guid userAccountId,
        AccountType accountType)
        : base(id)
    {
        UserAccountId = userAccountId;
        AccountType = accountType;
    }
    public Guid UserAccountId { get; private set; }
    public AccountType AccountType { get; private set; }
    public IReadOnlyList<Wallet>? Wallets => _wallets;
    public IReadOnlyList<Card>? Cards => _cards;

    public new void Delete()
    {
        base.Delete();

        foreach (var wallet in _wallets)
            wallet.Delete();
        
        foreach (var card in _cards)
            card.Delete();
    }
    public new void Restore()
    {
        base.Restore();

        foreach (var wallet in _wallets)
            wallet.Restore();
        
        foreach (var card in _cards)
            card.Restore();
    }
    
    public UnitResult<Error> AddWallet(Wallet wallet)
    {
        if (_wallets.Contains(wallet))
        {
            return Errors.Extra.
                AlreadyExists(new ErrorParameters.Extra.ValueAlreadyExists(nameof(Wallet)));
        }
        
        _wallets.Add(wallet);

        return Result.Success<Error>();
    }
    
    public void SoftDeleteWallet(Wallet wallet)
    {
        var indexWallet = _wallets.
            FindIndex(w => w.Equals(wallet));
        
        _wallets[indexWallet].Delete();
    }
    
    public void RestoreWallet(Wallet wallet)
    {
        var indexWallet = _wallets.
            FindIndex(w => w.Equals(wallet));
        
        _wallets[indexWallet].Restore();
    }
    
    public void HardDeleteWallet(Wallet wallet)
    {
        _wallets.Remove(wallet);
    }
    
    public void UpdateWalletBalance(Wallet wallet, Balance balance)
    {
        var indexWallet = _wallets.
            FindIndex(w => w == wallet);
        
        _wallets[indexWallet].UpdateBalance(balance);
    }
    
    public UnitResult<Error> AddCard(Card card)
    {
        if (_cards.Contains(card))
        {
            return Errors.Extra.
                AlreadyExists(new ErrorParameters.Extra.ValueAlreadyExists(nameof(Card)));
        }
        
        _cards.Add(card);

        return Result.Success<Error>();
    }
    
    public void DeleteCard(Card card)
    {
        _cards.Remove(card);
    }
    
    // Account: create, update(wallets, cards), delete, softDelete, restore
    // Wallet : add, update(only balance), delete, softDelete, restore
    // Card : add, delete
    // Cards <- Account -> Wallets
    // Card -> Wallet
}